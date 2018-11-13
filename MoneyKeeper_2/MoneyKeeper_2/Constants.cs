using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace MoneyKeeper_2
{
    public static class Constants
    {
        public static float SCALE_X = Screen.PrimaryScreen.Bounds.Width / 1920.0f;
        public static float SCALE_Y = Screen.PrimaryScreen.Bounds.Height / 1080.0f;

        public const string ACTION_TYPE_DEF_EARNING = "Доход";
        public const string ACTION_TYPE_DEF_SPENDING = "Расход";
        public const string ACTION_TYPE_DEF_DEBT_GOT = "Возврат долга";
        public const string ACTION_TYPE_DEF_SALARY = "Зарплата";

        public const string STATE_SALDO = "Должен";
        public const string STATE_DEBT_GOT = "Не должен";
        public const string STATE_OWN_DEBT = "Вы должны";

        public const string STATE_PAYED = "Оплачено";
        public const string STATE_DEBT = "Отсрочка";

        public const string COLUMN_NAME_ACTION_NUMBER = "ActionNumber";
        public const string COLUMN_NAME_ACTION_ID = "ActionID";
        public const string COLUMN_NAME_ACTION_ORDER_ID = "OrderID";
        public const string COLUMN_NAME_ACTION_TYPE = "ActionType";
        public const string COLUMN_NAME_ACTION_DATE = "ActionDate";
        public const string COLUMN_NAME_ACTION_STATE = "State";
        public const string COLUMN_NAME_ACTION_PRODUCT_NAME = "ProductName";
        public const string COLUMN_NAME_ACTION_PRODUCT_COUNT = "ProductCount";
        public const string COLUMN_NAME_ACTION_PRODUCT_PRICE = "ProductPrice";
        public const string COLUMN_NAME_ACTION_TOTAL_PRICE = "TotalPrice";
        public const string COLUMN_NAME_ACTION_EARNED_MONEY = "GotMoney";
        public const string COLUMN_NAME_ACTION_SPENT_MONEY = "SpentMoney";
        public const string COLUMN_NAME_ACTION_DEBT = "Withdraw";
        public const string COLUMN_NAME_ACTION_CLIENT = "Person";
        public const string COLUMN_NAME_ACTION_COMMENT = "Comment";

        public const string COLUMN_NAME_ACTION_NUMBER_PRINT = "ActionNumberPrint";
        public const string COLUMN_NAME_ACTION_ID_PRINT = "ActionIDPrint";
        public const string COLUMN_NAME_ACTION_ORDER_ID_PRINT = "ActionOrderIDPrint";
        public const string COLUMN_NAME_ACTION_TYPE_PRINT = "ActionTypePrint";
        public const string COLUMN_NAME_ACTION_DATE_PRINT = "ActionDatePrint";
        public const string COLUMN_NAME_ACTION_STATE_PRINT = "StatePrint";
        public const string COLUMN_NAME_ACTION_PRODUCT_NAME_PRINT = "ProductNamePrint";
        public const string COLUMN_NAME_ACTION_PRODUCT_COUNT_PRINT = "ProductCountPrint";
        public const string COLUMN_NAME_ACTION_PRODUCT_PRICE_PRINT = "ProductPricePrint";
        public const string COLUMN_NAME_ACTION_TOTAL_PRICE_PRINT = "TotalPricePrint";
        public const string COLUMN_NAME_ACTION_EARNED_MONEY_PRINT = "GotMoneyPrint";
        public const string COLUMN_NAME_ACTION_SPENT_MONEY_PRINT = "SpentMoneyPrint";
        public const string COLUMN_NAME_ACTION_DEBT_PRINT = "WithdrawPrint";
        public const string COLUMN_NAME_ACTION_CLIENT_PRINT = "PersonPrint";
        public const string COLUMN_NAME_ACTION_COMMENT_PRINT = "CommentPrint";

        public const string DEBT = "Долг";
        public const string TOTAL = "Итого";

        public static string ORDERS_INFO_PATH = Environment.CurrentDirectory + "/Data/OrdersInfo.txt";
        public static string NAMES_INFO_PATH = Environment.CurrentDirectory + "/Data/NominationsInfo.txt";
        public static string CLIENTS_INFO_PATH = Environment.CurrentDirectory + "/Data/ClientsInfo.txt";

        public static Color DEBT_COLOR = Color.Red;
        public static Color NO_DEBT_COLOR = Color.Green;
        public static Color MINUS_DEBT_COLOR = Color.Blue;
        public static Color SELECT_CELL_COLOR = Color.LightGray;
        public static Color NORMAL_CELL_COLOR = Color.White;

        public static void ScaleForm(Form F, float SCALE_X, float SCALE_Y)
        {
            F.Scale(new SizeF(SCALE_X, SCALE_Y));
            foreach (Control c in F.Controls)
                c.Font = new Font(FontFamily.GenericSansSerif, c.Font.Size * SCALE_X);
        }
    }
}
