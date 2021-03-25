using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Entities
{
    class Subdivision
    {
        private int id;
        private string subdivision;
        private List<Branch> branches;

        public Subdivision(int id, string subdivision)
        {
            this.id = id;
            this.subdivision = subdivision;
        }

        public int Id { get => id; set => id = value; }
        public string GetSubdivision { get => subdivision; set => subdivision = value; }
        internal List<Branch> Branches { get => branches; set => branches = value; }

        public override string ToString()
        {
            string text = "Id: " + id + " Подразделение: " + subdivision + "Филиалы подразделения:\n";
            foreach(Branch branch in branches)
            {
                text += branch.ToString() + "\n";
            }
            return text;
        }
    }
}
