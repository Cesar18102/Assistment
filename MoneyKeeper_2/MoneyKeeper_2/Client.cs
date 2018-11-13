using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyKeeper_2
{
    public class Client
    {
        public string Name { get; private set; }

        public Client(string name)
        {
            this.Name = name;
        }

        public override string ToString()
        {
            return Name;
        }

        public override bool Equals(object obj)
        {
            if(!obj.GetType().Equals(typeof(Client)))
                return false;

            return (obj as Client).Name == Name;
        }
    }
}
