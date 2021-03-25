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
        private Month month;
        public WorkHours(int id, int hoursCount, Employee employee, Month month)
        {
            this.id = id;
            this.hoursCount = hoursCount;
            this.employee = employee;
            this.month = month;
        }

        public int Id { get => id; set => id = value; }
        public int HoursCount { get => hoursCount; set => hoursCount = value; }
        internal Employee Employee { get => employee; set => employee = value; }
        internal Month Month { get => month; set => month = value; }

        public override string ToString()
        {
            return "В " + month.Name +  "Сотрудник: " + employee.getFIO() + " отработал: " 
                + hoursCount + (
                hoursCount % 100 > 10 
                && hoursCount % 100 < 20 
                || (hoursCount % 10 > 4 && hoursCount % 10 < 10) 
                || hoursCount % 10 == 0 ? ("часов") 
                : hoursCount % 10 == 1 ? ("Час") 
                : ("Часа")).ToString();
        }
    }
}
