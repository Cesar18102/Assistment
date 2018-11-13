using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyKeeper_2
{
    public class OrderTable
    {
        private List<Order> orders;

        public int CountOfActions { get; private set; }
        public int CountOfOrders { get { return orders.Count; } }

        private int maxOrderID = -1;
        private int maxActionID = -1;

        public int MaxOrderID { get { return maxOrderID; } private set { maxOrderID = value > maxOrderID ? value : maxOrderID; } }
        public int MaxActionID { get { return maxActionID; } private set { maxActionID = value > maxActionID ? value : maxActionID; } }

        public Order this[int i]
        {
            get { return (i < 0 || i >= CountOfOrders) ? null : orders[i]; }
        }

        public OrderTable()
        {
            orders = new List<Order>();
            CountOfActions = 0;
        }

        public void AddOrder(Order order)
        {
            orders.Add(order);

            MaxOrderID = order.OrderID;
            MaxActionID = order.MaxActionID;

            CountOfActions += order.CountOfActions;
        }

        public bool RemoveOrder(int orderID)
        {
            for (int i = 0; i < CountOfOrders; i++ )
                if (orders[i].OrderID == orderID)
                {
                    orders.RemoveAt(i);
                    return true;
                }

            return false;
        }

        public bool RemoveAction(int ID)
        {
            for (int i = 0; i < CountOfOrders; i++)
                for (int j = 0; j < orders[i].CountOfActions; j++ )
                    if (orders[i][j].ID == ID)
                    {
                        orders[i].RemoveAction(ID);
                        CountOfActions--;

                        if (orders[i].CountOfActions == 0)
                            orders.RemoveAt(i);

                        return true;
                    }

            return false;
        }

        public List<MoneyAction> GetListOfSelectedActions(params ActionFits[] actionPredicates)
        {
            List<MoneyAction> SelectedActions = new List<MoneyAction>();
            foreach (Order order in orders)
                for (int i = 0; i < order.CountOfActions; i++)
                {
                    bool ToAdd = true;
                    foreach (ActionFits ActionPredicate in actionPredicates)
                        if (!ActionPredicate(order[i]))
                        {
                            ToAdd = false;
                            break;
                        }

                    if (ToAdd)
                        SelectedActions.Add(order[i]);
                }
            return SelectedActions;
        }

        public delegate bool ActionFits(MoneyAction MA);

        public override string ToString()
        {
            return String.Join("\n", orders);
        }
    }
}