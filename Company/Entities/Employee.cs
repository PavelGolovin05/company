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
        private Branch branch;
        private Position position;
        private PaymentType paymentType;
        private int fixedSalary;
        private int hourCost;

        public Employee(int id, string surname, string name, string patronymic, Branch branch,
            Position position, PaymentType paymentType, int fixedSalary, int hourCost)
        {
            this.id = id;
            this.surname = surname;
            this.name = name;
            this.patronymic = patronymic;
            this.branch = branch;
            this.position = position;
            this.paymentType = paymentType;
            this.fixedSalary = fixedSalary;
            this.hourCost = hourCost;
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
        internal Branch Branch { get => branch; set => branch = value; }
        internal Position Position { get => position; set => position = value; }
        internal PaymentType PaymentType { get => paymentType; set => paymentType = value; }

        public override string ToString()
        {
            return "Id: " + id + " ФИО: " + getFIO() + " Должность: "
                + position.GetPosition + " Тип оплаты: " + paymentType.GetPaymentType
                + (fixedSalary > 0 ? (" Фиксированная запрлата: " + fixedSalary) : (""))
                + (hourCost > 0 ? (" Стоимость часа работы: " + hourCost) : (""));
        }
    }
}
