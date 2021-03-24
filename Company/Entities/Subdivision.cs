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

        public Subdivision(int id, string subdivision)
        {
            this.id = id;
            this.subdivision = subdivision;
        }

        public int Id { get => id; set => id = value; }
        public string GetSubdivision { get => subdivision; set => subdivision = value; }

        public override string ToString()
        {
            return "Id: " + id + " Подразделение: " + subdivision; ;
        }
    }
}
