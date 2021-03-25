using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Entities
{
    public class Month
    {
        private int id;
        private int workHours;
        private string name;

        public Month(int id, int workHours, string name)
        {
            this.id = id;
            this.workHours = workHours;
            this.name = name;
        }

        public int Id { get => id; set => id = value; }
        public int WorkHours { get => workHours; set => workHours = value; }
        public string Name { get => name; set => name = value; }

        public override string ToString()
        {
            return "В " + name + ":" + workHours + (
                workHours % 100 > 10
                && workHours % 100 < 20
                || (workHours % 10 > 4 && workHours % 10 < 10)
                || workHours % 10 == 0 ? ("рабочих часов")
                : workHours % 10 == 1 ? ("рабоий час")
                : ("рабочих часа")).ToString();
        }
    }
}
