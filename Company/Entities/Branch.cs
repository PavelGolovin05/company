using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Entities
{
    public class Branch
    {
        private int id;
        private string name;
        private City city;
        private List<Subdivision> subdivisions;

        public Branch(int id, string name, City city)
        {
            this.id = id;
            this.name = name;
            this.city = city;
        }

        public int Id { get => id; set => id = value; }
        public string Name { get => name; set => name = value; }
        internal City City { get => city; set => city = value; }
        internal List<Subdivision> Subdivisions { get => subdivisions; set => subdivisions = value; }

        public override string ToString()
        {
            string text = "Id: " + id + " Название филиала: " + name + " Город: " + city.GetCity + "Подразделения филиала:\n";
            foreach(Subdivision subdivision in subdivisions)
            {
                text += subdivision.ToString() + "\n";
            }
            return text;
        }
    }
}
