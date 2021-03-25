using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Entities
{
    class Employee
    {
        private int id;
        private string surname;
        private string name;
        private string patronymic;
        private BranchSubdivision branchSubdivision;
        private Position position;
        private PaymentType paymentType;
        private int fixedSalary;
        private int hourCost;
        private int salary;

        public Employee(int id, string surname, string name, string patronymic,
            BranchSubdivision branchSubdivision, Position position,
            PaymentType paymentType, int fixedSalary, int hourCost)
        {
            this.id = id;
            this.surname = surname;
            this.name = name;
            this.patronymic = patronymic;
            this.branchSubdivision = branchSubdivision;
            this.position = position;
            this.paymentType = paymentType;
            this.fixedSalary = fixedSalary;
            this.hourCost = hourCost;
            salary = 0;
        }
        public string getFIO()
        {
            return surname + " " + name + " " + patronymic;
        }
        public int Id { get => id; set => id = value; }
        public string Surname { get => surname; set => surname = value; }
        public string Name { get => name; set => name = value; }
        public string Patronymic { get => patronymic; set => patronymic = value; }
        public int FixedSalary { get => fixedSalary; set => fixedSalary = value; }
        public int HourCost { get => hourCost; set => hourCost = value; }

        internal Position Position { get => position; set => position = value; }
        internal PaymentType PaymentType { get => paymentType; set => paymentType = value; }
        internal BranchSubdivision BranchSubdivision { get => branchSubdivision; set => branchSubdivision = value; }
        public int Salary { get => salary; set => salary = value; }

        public override string ToString()
        {
            return "Id: " + id + " ФИО: " + getFIO() + " Должность: "
                + position.GetPosition + " Тип оплаты: " + paymentType.GetPaymentType
                + (fixedSalary > 0 ? (" Фиксированная запрлата: " + fixedSalary) : (""))
                + (hourCost > 0 ? (" Стоимость часа работы: " + hourCost) : (""))
                + " " + branchSubdivision.ToString()
                + " Зарплата в текущем месяце: " + salary;
        }
    }
}
