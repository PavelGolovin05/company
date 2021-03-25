using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Entities
{
    public class Position
    {
        private int id;
        private string position;

        public Position(int id, string position)
        {
            this.id = id;
            this.position = position;
        }
        public int Id { get => id; set => id = value; }
        public string GetPosition { get => position; set => position = value; }

        public override string ToString()
        {
            return "Id: " + id + " Должность: " + position;
        }
    }
}
