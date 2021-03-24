using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Entities
{
    class City
    {
        private int id;
        private string city;

        public City(int id, string city)
        {
            this.id = id;
            this.city = city;
        }

        public int Id { get => id; set => id = value; }
        public string GetCity { get => city; set => city = value; }

        public override string ToString()
        {
            return "Id: " + id + " Город: " + city;
        }
    }
}
