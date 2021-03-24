using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Entities
{
    class WorkHours
    {
        private int id;
        private int hoursCount;
        private Employee employee;
        public WorkHours(int id, int hoursCount, Employee employee)
        {
            this.id = id;
            this.hoursCount = hoursCount;
            this.employee = employee;
        }

        public int Id { get => id; set => id = value; }
        public int HoursCount { get => hoursCount; set => hoursCount = value; }
        internal Employee Employee { get => employee; set => employee = value; }

        public override string ToString()
        {
            return "Сотрудник: " + employee.getFIO() + " отработал: " + (
                hoursCount % 100 > 10 
                && hoursCount % 100 < 20 
                || (hoursCount % 10 > 4 && hoursCount % 10 < 10) 
                || hoursCount % 10 == 0 ? ("часов") 
                : hoursCount % 10 == 1 ? ("Час") 
                : ("Часа")).ToString();
        }
    }
}
