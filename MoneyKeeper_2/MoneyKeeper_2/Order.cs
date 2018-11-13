using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyKeeper_2
{
    public class Order
    {
        private List<MoneyAction> moneyActions;

        public int OrderID { get; private set; }
        public int CountOfActions { get { return moneyActions.Count; } }

        private int maxActionID = -1;
        public int MaxActionID { get { return maxActionID; } private set { maxActionID = value > maxActionID? value : maxActionID; } }

        public MoneyAction this[int i]
        {
            get { return (i < 0 || i >= CountOfActions) ? null : moneyActions[i]; }
        }

        public Order(int orderID)
        {
            this.OrderID = orderID;
            moneyActions = new List<MoneyAction>();
        }

        public void AddAction(MoneyAction action)
        {
            if (action.OrderID != OrderID)
                return;

            moneyActions.Add(action);
            MaxActionID = action.ID;
        }

        public void RemoveAction(int ID)
        {
            int index = getIndexOfActionByID(ID);

            if (index == -1)
                return;

            moneyActions.RemoveAt(index);
        }

        public int getIndexOfActionByID(int ID)
        {
            for (int i = 0; i < CountOfActions; i++)
                if (moneyActions[i].ID == ID)
                    return i;

            return -1;
        }

        public override string ToString()
        {
            return OrderID + " " + CountOfActions + "\n" + String.Join("\n", moneyActions);
        }
    }
}
