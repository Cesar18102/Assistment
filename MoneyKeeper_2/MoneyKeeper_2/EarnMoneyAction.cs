using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyKeeper_2
{
    public class EarnMoneyAction : MoneyAction
    {
        public EarnMoneyAction(string actionTypeDef, int id, int orderID, 
                               DateTime actionDate, string state, string productName, 
                               double productCount, double productPrice, double payed, 
                               string person, string comment)

                        : base(actionTypeDef, id, orderID, actionDate, 
                               state, productName, productCount, productPrice, 
                               payed, person, comment, true) { }
    }
}
