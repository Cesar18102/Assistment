using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyKeeper_2
{
    public class PersonSummary
    {
        public bool IsEarning { get; private set; }

        public string ActionTypeDefinition { get; private set; }
        public List<int> IDs = new List<int>();
        public List<int> OrderIDs = new List<int>();

        public DateTime dateStart = new DateTime(2100, 1, 1);
        public DateTime dateFinish;

        public string State;
        public string Person { get; private set; }

        public List<string> ProductNames = new List<string>();
        public List<double> ProductCounts = new List<double>();
        public List<double> MinProductPrices = new List<double>();
        public List<double> MaxProductPrices = new List<double>();

        public double TotalPrice { get; private set; }
        public double Earned { get; private set; }
        public double Spent { get { return 0; } }

        public PersonSummary(string person)
        {
            this.Person = person;
        }

        public void AddAction(MoneyAction MA)
        {
            if(MA.Person != this.Person)
                return;

            ActionTypeDefinition = MA.ActionTypeDefinition;
            IsEarning = MA.IsEarning;

            if (IDs.IndexOf(MA.ID) == -1)
                IDs.Add(MA.ID);

            if (OrderIDs.IndexOf(MA.OrderID) == -1)
                OrderIDs.Add(MA.OrderID);

            if (dateStart > MA.Date)
                dateStart = MA.Date;

            if (dateFinish < MA.Date)
                dateFinish = MA.Date;

            int productIndex = ProductNames.IndexOf(MA.ProductName);
            if (productIndex == -1)
            {
                ProductNames.Add(MA.ProductName);
                ProductCounts.Add(MA.ProductCount);
                MinProductPrices.Add(MA.ProductPrice);
                MaxProductPrices.Add(MA.ProductPrice);
            }
            else
            {
                ProductCounts[productIndex] += MA.ProductCount;

                if(MA.ProductPrice < MinProductPrices[productIndex])
                    MinProductPrices[productIndex] = MA.ProductPrice;
                if(MA.ProductPrice > MaxProductPrices[productIndex])
                    MaxProductPrices[productIndex] = MA.ProductPrice;
            }

            TotalPrice += MA.TotalPrice;
            Earned += MA.Earned;

            State = TotalPrice < Earned ? Constants.STATE_OWN_DEBT : (TotalPrice > Earned ? Constants.STATE_SALDO : Constants.STATE_DEBT_GOT);
        }
    }
}
