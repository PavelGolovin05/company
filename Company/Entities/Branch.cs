using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Entities
{
    class Branch
    {
        private int id;
        private string name;
        private City city;
        private Subdivision subdivision;

        public Branch(int id, string name, City city, Subdivision subdivision)
        {
            this.id = id;
            this.name = name;
            this.city = city;
            this.subdivision = subdivision;
        }

        public int Id { get => id; set => id = value; }
        public string Name { get => name; set => name = value; }
        internal City City { get => city; set => city = value; }
        internal Subdivision Subdivision { get => subdivision; set => subdivision = value; }

        public override string ToString()
        {
            return "Id: " + id + " Название филиала: " + name + " Город: " + city.GetCity + " Название подразделения: " + subdivision.GetSubdivision;
        }
    }
}
