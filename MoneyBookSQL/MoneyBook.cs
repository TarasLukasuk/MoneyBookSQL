using System;

namespace MoneyBookSQL
{
    internal class MoneyBook
    {
        public MoneyBook()
        {

        }

        public MoneyBook(string nameOperation, string operation, int price, string description)
        {
            NameOperation = nameOperation;
            Operation = operation;
            Price = price;
            Description = description;
        }

        public MoneyBook(int ID, string nameOperation, string operation, int price, string description)
        {
            this.ID = ID;
            NameOperation = nameOperation;
            Operation = operation;
            Price = price;
            Description = description;
        }

        public int ID { get; set; }
        public string DateTimes { get; set; } = DateTime.Now.ToString();
        public string NameOperation { get; set; }
        public string Operation { get; set; }
        public int Price { get; set; }
        public string Description { get; set; }
    }
}
