using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;

namespace MoneyBookSQL
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private readonly SQL SQL = new SQL();
        private readonly XML XML = new XML();
        private int result = 0;
        private int index = 0;
        private bool permissionRecordData;

        private void CheckingTextEmptiness()
        {
            if (NameOperation.Text == string.Empty || Price.Text == string.Empty || Description.Text == string.Empty)
            {
                MessageBox.Show("Not all fields are filled");
                permissionRecordData = false;
            }
            else
            {
                permissionRecordData = true;
            }
        }

        private void IsTextAllowed(TextBox textBox)
        {
            if (Regex.IsMatch(textBox.Text,"[^0-9]"))
            {
                textBox.Text = textBox.Text.Remove(textBox.Text.Length - 1);
                textBox.SelectionStart = textBox.Text.Length;
            }
        }

        private void Calculations(string operation, int price)
        {
            if (operation=="+")
            {
                result += XML.GetTotalPrice() + price;
                TotalPrice.Text = result.ToString();
            }

            if (operation=="-")
            {
                result -= price - XML.GetTotalPrice();
                TotalPrice.Text = result.ToString();
            }
        }

        private void Minus_Click(object sender, RoutedEventArgs e)
        {
            DataRecording("-");
        }

        private void Plus_Click(object sender, RoutedEventArgs e)
        {
            DataRecording("+");
        }

        private void DataRecording(string operation)
        {
            CheckingTextEmptiness();

            if (permissionRecordData)
            {
                int nextID = SQL.NumberLastItemList;
                nextID++;

                int.TryParse(Price.Text, out int rezalt);

                MoneyBook moneyBook = new MoneyBook(nextID, NameOperation.Text, operation, rezalt, Description.Text);

                SQL.AddElement(moneyBook);

                Table.ItemsSource = SQL.ShowData();

                ID.Content = SQL.NumberLastItemList;

                Calculations(operation, rezalt);

                NameOperation.Text = string.Empty;
                Price.Text = string.Empty;
                Description.Text = string.Empty;
            }
        }

        private void Price_TextChanged(object sender, TextChangedEventArgs e)
        {
            IsTextAllowed(Price);
        }

        private void TotalPrice_TextChanged(object sender, TextChangedEventArgs e)
        {
            IsTextAllowed(TotalPrice);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            SQL.LoadData();

            Table.ItemsSource = SQL.ShowData();

            ID.Content = SQL.NumberLastItemList;

            Table.ScrollIntoView(Table.Items[Table.Items.Count - 1]);

            TotalPrice.Text = XML.GetTotalPrice().ToString();
        }

        private void Table_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            index = Table.SelectedIndex;
        }

        private void DeleteRow_Click(object sender, RoutedEventArgs e)
        {
            Table.ItemsSource = SQL.ShowData();

            int numberID = SQL.NumberLastItemList;
            numberID--;

            ID.Content = numberID.ToString();

            TotalPrice.Text = SQL.DeleteItem(index, TotalPrice).ToString();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            XML.SavePrice(TotalPrice.Text);
            SQL.SaveData();
        }
    }
}
