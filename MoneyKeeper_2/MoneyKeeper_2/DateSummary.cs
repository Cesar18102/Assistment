using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyKeeper_2
{
    public class DateSummary
    {
        public List<int> IDs_spend = new List<int>();
        public List<int> OrderIDs_spend = new List<int>();

        public List<int> IDs_earned = new List<int>();
        public List<int> OrderIDs_earned = new List<int>();

        public DateTime dateStart = new DateTime(2100, 1, 1);
        public DateTime dateFinish;

        public List<string> ProductNames_spend = new List<string>();
        public List<double> ProductCounts_spend = new List<double>();
        public List<double> MinProductPrices_spend = new List<double>();
        public List<double> MaxProductPrices_spend = new List<double>();

        public List<string> Persons_earned = new List<string>();
        public List<string> ProductNames_earned = new List<string>();
        public List<double> ProductCounts_earned = new List<double>();
        public List<double> MinProductPrices_earned = new List<double>();
        public List<double> MaxProductPrices_earned = new List<double>();

        public double TotalPrice_earned { get; private set; }
        public double TotalPrice_spend { get; private set; }

        public double Earned { get; private set; }
        public double Spend { get; private set; }
        
        public DateSummary(DateTime dateStart, DateTime dateFinish)
        {
            this.dateStart = dateStart;
            this.dateFinish = dateFinish;
        }

        public void AddAction(MoneyAction MA)
        {
            if (MA.Date < dateStart || MA.Date > dateFinish)
                return;

            if (MA.IsEarning)
                AddActionEarn(MA);
            else
                AddActionSpend(MA);
        }

        private void AddActionEarn(MoneyAction MA)
        {
            if (IDs_earned.IndexOf(MA.ID) == -1)
                IDs_earned.Add(MA.ID);

            if (OrderIDs_earned.IndexOf(MA.OrderID) == -1)
                OrderIDs_earned.Add(MA.OrderID);

            int productIndex = ProductNames_earned.IndexOf(MA.ProductName);
            if (productIndex == -1)
            {
                ProductNames_earned.Add(MA.ProductName);
                ProductCounts_earned.Add(MA.ProductCount);
                MinProductPrices_earned.Add(MA.ProductPrice);
                MaxProductPrices_earned.Add(MA.ProductPrice);
            }
            else
            {
                ProductCounts_earned[productIndex] += MA.ProductCount;

                if (MA.ProductPrice < MinProductPrices_earned[productIndex])
                    MinProductPrices_earned[productIndex] = MA.ProductPrice;

                if (MA.ProductPrice > MaxProductPrices_earned[productIndex])
                    MaxProductPrices_earned[productIndex] = MA.ProductPrice;
            }

            TotalPrice_earned += MA.TotalPrice;
            Earned += MA.Payed;

            if (Persons_earned.IndexOf(MA.Person) == -1)
                Persons_earned.Add(MA.Person);
        }

        private void AddActionSpend(MoneyAction MA)
        {
            if (IDs_spend.IndexOf(MA.ID) == -1)
                IDs_spend.Add(MA.ID);

            if (OrderIDs_spend.IndexOf(MA.OrderID) == -1)
                OrderIDs_spend.Add(MA.OrderID);

            int productIndex = ProductNames_spend.IndexOf(MA.ProductName);
            if (productIndex == -1)
            {
                ProductNames_spend.Add(MA.ProductName);
                ProductCounts_spend.Add(MA.ProductCount);
                MinProductPrices_spend.Add(MA.ProductPrice);
                MaxProductPrices_spend.Add(MA.ProductPrice);
            }
            else
            {
                ProductCounts_spend[productIndex] += MA.ProductCount;

                if (MA.ProductPrice < MinProductPrices_spend[productIndex])
                    MinProductPrices_spend[productIndex] = MA.ProductPrice;

                if (MA.ProductPrice > MaxProductPrices_spend[productIndex])
                    MaxProductPrices_spend[productIndex] = MA.ProductPrice;
            }

            TotalPrice_spend += MA.TotalPrice;
            Spend += MA.Payed;
        }
    }
}
