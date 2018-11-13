using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization.Json;

namespace MoneyKeeper_2
{
    public class SpendMoneyAction : MoneyAction
    {
        public SpendMoneyAction(string actionTypeDef, int id, int orderID, 
                                DateTime actionDate, string productName, double productCount, 
                                double productPrice, string comment)

                         : base(actionTypeDef, id, orderID, actionDate, 
                                "", productName, productCount, productPrice,
                                productCount * productPrice, "", comment, false) { }
    }
}
