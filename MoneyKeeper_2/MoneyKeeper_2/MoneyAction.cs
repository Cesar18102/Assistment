using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyKeeper_2
{
    public class MoneyAction
    {
        public int ID { get; private set; }
        public int OrderID { get; private set; }
        public string ActionTypeDefinition { get; private set; }
        public bool IsEarning { get; private set; }

        private DateTime date;
        public DateTime Date { get { return new DateTime(date.Year, date.Month, date.Day); } }

        public string State { get; private set; }
        public string ProductName { get; private set; }
        public double ProductCount { get; private set; }
        public double ProductPrice { get; private set; }
        public double Earned { get; private set; }
        public double Spent { get; private set; }
        public string Person { get; private set; }
        public string Comment { get; private set; }

        public double TotalPrice { get { return ProductCount * ProductPrice; } }
        public double Payed { get; private set; }

        public MoneyAction(string actionTypeDefinition, int id, int orderID, 
                           DateTime actionDate, string state, string productName, 
                           double productCount, double productPrice, double payed, 
                           string person, string comment, bool IsEarning)
        {
            this.ActionTypeDefinition = actionTypeDefinition;
            this.ID = id;
            this.OrderID = orderID;
            this.date = actionDate;
            this.State = state;
            this.ProductName = productName;
            this.ProductCount = productCount;
            this.ProductPrice = productPrice;
            this.Earned = IsEarning? payed : 0;
            this.Spent = IsEarning? 0 : payed;
            this.Payed = payed;
            this.Person = person;
            this.Comment = comment;
            this.IsEarning = IsEarning;
        }

        public override string ToString()
        {
            return ActionTypeDefinition + ";" + ID + ";" + OrderID+ ";" +
                   date.Year + ";" + date.Month + ";" + date.Day + ";" + 
                   State + ";" + ProductName + ";" + ProductCount + ";" + 
                   ProductPrice + ";" + Payed + ";" + Person + ";" + Comment;
        }
    }
}
