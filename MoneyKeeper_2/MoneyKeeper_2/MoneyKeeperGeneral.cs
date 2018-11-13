using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace MoneyKeeper_2
{
    public class MoneyKeeperGeneral
    {
        public OrderTable MoneyActions;
        public Nominations ProductNames;
        public ClientBase ClientNames;

        public MoneyKeeperGeneral()
        {
            MoneyActions = new OrderTable();
            ProductNames = new Nominations();
            ClientNames = new ClientBase();
        }

        public void Load(string fileOrdersPath, string fileNominationsPath, string fileClientsPath)
        {
            if (!File.Exists(fileNominationsPath))
            {
                Stream str = File.Create(fileNominationsPath);
                str.Close();
            }

            StreamReader strNom = new StreamReader(fileNominationsPath);

            while (!strNom.EndOfStream)
                ProductNames.AddNomination(strNom.ReadLine());

            strNom.Close();//Product names

            if (!File.Exists(fileClientsPath))
            {
                Stream str = File.Create(fileClientsPath);
                str.Close();
            }

            StreamReader strCli = new StreamReader(fileClientsPath);

            while (!strCli.EndOfStream)
                ClientNames.AddClient(new Client(strCli.ReadLine()));

            strCli.Close();//Client names

            if (!File.Exists(fileOrdersPath))
            {
                Stream str = File.Create(fileOrdersPath);
                str.Close();
            }

            StreamReader strOrd = new StreamReader(fileOrdersPath);
            
            while(!strOrd.EndOfStream)
            {
                string info = strOrd.ReadLine();
                int orderID = Convert.ToInt32(info.Substring(0, info.IndexOf(' ')));
                int countOfActions = Convert.ToInt32(info.Substring(info.IndexOf(' ') + 1, info.Length - info.IndexOf(' ') - 1));
                Order order = new Order(orderID);

                for (int i = 0; i < countOfActions && !strOrd.EndOfStream; i++)
                {
                    string[] actionData = strOrd.ReadLine().Split(';');
                    MoneyAction action = new MoneyAction(actionData[0], Convert.ToInt32(actionData[1]), Convert.ToInt32(actionData[2]),
                                                         new DateTime(Convert.ToInt32(actionData[3]), Convert.ToInt32(actionData[4]), Convert.ToInt32(actionData[5])),
                                                         actionData[6], actionData[7], Convert.ToDouble(actionData[8]), Convert.ToDouble(actionData[9]),
                                                         Convert.ToDouble(actionData[10]), actionData[11], actionData[12], actionData[0] == Constants.ACTION_TYPE_DEF_EARNING);

                    order.AddAction(action);
                }

                MoneyActions.AddOrder(order);
            }

            strOrd.Close();//orders
        }

        public void Save(string fileOrdersPath, string fileNominationsPath, string fileClientsPath)
        {
            if (!File.Exists(fileNominationsPath))
            {
                Stream str = File.Create(fileNominationsPath);
                str.Close();
            }

            StreamWriter strwOrd = new StreamWriter(fileOrdersPath);
            strwOrd.Write(MoneyActions.ToString());
            strwOrd.Close();//product names

            if (!File.Exists(fileOrdersPath))
            {
                Stream str = File.Create(fileOrdersPath);
                str.Close();
            }

            StreamWriter strwNom = new StreamWriter(fileNominationsPath);
            strwNom.Write(ProductNames.ToString());
            strwNom.Close();//orders

            if (!File.Exists(fileClientsPath))
            {
                Stream str = File.Create(fileClientsPath);
                str.Close();
            }

            StreamWriter strwCli = new StreamWriter(fileClientsPath);
            strwCli.Write(ClientNames.ToString());
            strwCli.Close();//client names
        }
    }
}
