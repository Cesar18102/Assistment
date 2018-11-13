using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyKeeper_2
{
    public class ProductSummary
    {
        public bool IsEaring { get; private set; }

        public string ActionTypeDefinition { get; private set; }
        public List<int> IDs = new List<int>();
        public List<int> OrderIDs = new List<int>();

        public DateTime dateStart = new DateTime(2100, 1, 1);
        public DateTime dateFinish;

        public List<string> Persons = new List<string>();
        public string ProductName { get; private set; }
        public double ProductCount { get; private set; }
        public double MinProductPrice { get; private set; }
        public double MaxProductPrice { get; private set; }

        public double TotalPrice { get; private set; }
        public double Earned { get; private set; }
        public double Spent { get; private set; }

        public ProductSummary(string productName)
        {
            this.ProductName = productName;
            MinProductPrice = -1;
            MaxProductPrice = -1;
        }

        public void AddAction(MoneyAction MA)
        {
            if (MA.ProductName != this.ProductName)
                return;

            ActionTypeDefinition = MA.ActionTypeDefinition;
            IsEaring = MA.IsEarning;

            if (IDs.IndexOf(MA.ID) == -1)
                IDs.Add(MA.ID);

            if (OrderIDs.IndexOf(MA.OrderID) == -1)
                OrderIDs.Add(MA.OrderID);

            if (dateStart > MA.Date)
                dateStart = MA.Date;

            if (dateFinish < MA.Date)
                dateFinish = MA.Date;

            ProductCount += MA.ProductCount;

            if (MinProductPrice == -1 && MaxProductPrice == -1)
            {
                MinProductPrice = MA.ProductPrice;
                MaxProductPrice = MA.ProductPrice;
            }
            else
            {
                if (MA.ProductPrice < MinProductPrice)
                    MinProductPrice = MA.ProductPrice;

                if (MA.ProductPrice > MaxProductPrice)
                    MaxProductPrice = MA.ProductPrice;
            }

            TotalPrice += MA.TotalPrice;
            Earned += MA.Earned;
            Spent += MA.Spent;

            if (Persons.IndexOf(MA.Person) == -1)
                Persons.Add(MA.Person);
        }
    }
}
