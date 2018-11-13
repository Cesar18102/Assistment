using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyKeeper_2
{
    public class Salary : MoneyAction
    {
        public Salary(int ID, int orderID, DateTime ActionDate, 
                      double salary, string person, string comment)

            : base(Constants.ACTION_TYPE_DEF_SPENDING, ID, orderID, 
                   ActionDate, "", Constants.ACTION_TYPE_DEF_SALARY, 
                   1, salary, salary, person, comment, false)
        {

        }
    }
}
