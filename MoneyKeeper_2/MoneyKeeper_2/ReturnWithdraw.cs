using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyKeeper_2
{
    public class ReturnWithdraw : MoneyAction
    {
        public ReturnWithdraw(int ID, int orderID, DateTime ActionDate, 
                              double price, string person, string comment)

            : base(Constants.ACTION_TYPE_DEF_EARNING, ID, orderID, 
                   ActionDate, Constants.STATE_PAYED, Constants.DEBT, 
                   1, 0, price, person, comment, true)
        {

        }
    }
}
