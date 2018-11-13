using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyKeeper_2
{
    public class Nominations
    {
        private List<string> nominations;

        public int CountOfNominations { get { return nominations.Count; } }

        public string this[int i] { get { return (i < 0 || i >= CountOfNominations) ? null : nominations[i]; } }

        public Nominations()
        {
            nominations = new List<string>();
        }

        public bool AddNomination(string nomination)
        {
            if (nominations.Contains(nomination))
                return false;

            nominations.Add(nomination);

            return true;
        }

        public bool RemoveNomination(string nomination)
        {
            if (!nominations.Contains(nomination))
                return false;

            nominations.Remove(nomination);

            return true;
        }

        public override string ToString()
        {
            return String.Join("\n", nominations);
        }
    }
}
