using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyKeeper_2
{
    public class ClientBase
    {
        private List<Client> clients;

        public int CountOfClients { get { return clients.Count; } }

        public Client this[int i] { get { return (i < 0 || i >= clients.Count) ? null : clients[i]; } }

        public ClientBase()
        {
            clients = new List<Client>();
        }

        public bool AddClient(Client client)
        {
            if (clients.IndexOf(client) != -1)
                return false;

            clients.Add(client);
            return true;
        }

        public bool RemoveClient(Client client)
        {
            if (clients.IndexOf(client) == -1)
                return false;

            clients.Remove(client);
            return true;
        }

        public override string ToString()
        {
            return String.Join("\n", clients);
        }
    }
}
