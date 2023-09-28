using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Data.SQLite;
using System.Linq;
using System.Windows.Controls;

namespace MoneyBookSQL
{
    internal class SQL
    {
        public List<MoneyBook> Data = new List<MoneyBook>();
        private readonly AppContextSQL AppContextSQL = new AppContextSQL();

        public int NumberLastItemList => Data.Count;

        public void LoadData()
        {
            try
            {
                Data = AppContextSQL.MoneyBooks.ToList();
            }
            catch (System.Exception)
            {
                CreateTable();
            }
        }

        public void SaveData()
        {
            foreach (MoneyBook item in Data)
            {
                AppContextSQL.MoneyBooks.AddOrUpdate(item);
                AppContextSQL.SaveChangesAsync();
            }
        }

        public BindingList<MoneyBook> ShowData()
        {
            return new BindingList<MoneyBook>(Data);
        }

        public void AddElement(MoneyBook moneyBook)
        {
            Data.Add(moneyBook);
        }

        public void CreateTable()
        {
            using (SQLiteConnection connection = new SQLiteConnection("Data Source=.\\DB.db"))
            {
                connection.Open();

                SQLiteCommand command = new SQLiteCommand(connection)
                {
                    CommandText = "CREATE TABLE MoneyBooks (ID INTEGER,DateTimes TEXT, NameOperation TEXT, Operation TEXT, Price INTEGER DEFAULT 0, Description TEXT, PRIMARY KEY(ID AUTOINCREMENT))"
                };
                command.ExecuteNonQuery();
                connection.Close();
            }
        }

        public void DropTable()
        {
            using (SQLiteConnection connection = new SQLiteConnection("Data Source=.\\DB.db"))
            {
                connection.Open();

                SQLiteCommand command = new SQLiteCommand(connection)
                {
                    CommandText = " DROP Table MoneyBooks"
                };
                command.ExecuteNonQuery();
                connection.Close();
            }
        }

        private void UpdateID()
        {
            DropTable();
            CreateTable();
            OverwriteID();
        }

        public int DeleteItem(int elementNumber, TextBox textBox)
        {
            string operation = Data[elementNumber].Operation;
            int price = Data[elementNumber].Price;
            int.TryParse(textBox.Text, out int totalPrice);
            int result = 0;

            switch (operation)
            {
                case "+":
                    result = totalPrice - price;
                    break;
                case "-":
                    result = totalPrice + price;
                    break;
            }

            Data.RemoveAt(elementNumber);

            UpdateID();

            return result;
        }

        private void OverwriteID()
        {
            int newID = 0;
            foreach (MoneyBook item in Data)
            {
                newID++;
                item.ID = newID;
            }
        }
    }
}
