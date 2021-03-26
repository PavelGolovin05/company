using Company.DB;
using Company.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Company.Services
{
    class EmployeeService : Service
    {
        public EmployeeService(DBConnection dBConnection, DataGridView dataGridView) : base(dBConnection, dataGridView)
        { }
        public List<Employee> getAllEmployees()
        {
            string sql = "Select * From employees ORDER BY id";

            return getEmployees(sql);
        }

        public List<String> getColumnNames()
        {
            List<String> columnNames = new List<string>();
            string sql = "Select * From employees limit 1";
            DataTable employeesTable = dBConnection.SelectQuery(sql);
            foreach (DataColumn column in employeesTable.Columns)
            {
                columnNames.Add(column.ColumnName.ToString());
            }
            columnNames.Add("salary");
            return columnNames;
        }

        public List<Employee> getChosenBranchEmployees(string branch)
        {
            string sql = String.Format("select employees.id, employees.surname, employees.name, employees.patronymic, employees.position,\n"
                + "employees.payment_type , employees.branch_subdivision, employees.fixed_salary, employees.hour_cost\n"
                + "from employees\n"
                + "inner join branches_subdivisions on branches_subdivisions.id = employees.branch_subdivision\n"
                + "inner join branсhes on branсhes.id = branches_subdivisions.branch\n"
                + "where branсhes.name = '{0}'", branch);

            return getEmployees(sql);
        }

        public List<Employee> getChosenSubdivisionEmployees(string subdivision)
        {
            string sql = String.Format("select employees.id, employees.surname, employees.name, employees.patronymic, employees.position,\n"
                + "employees.payment_type , employees.branch_subdivision, employees.fixed_salary, employees.hour_cost\n"
                + "from employees\n"
                + "inner join branches_subdivisions on branches_subdivisions.id = employees.branch_subdivision\n"
                + "inner join subdivisions on subdivisions.id = branches_subdivisions.subdivision\n"
                + "where subdivisions.subdivision = '{0}'", subdivision);

            return getEmployees(sql);
        }

        public List<Employee> getChosenBranchSubdivisionEmployees(string branch, string subdivision)
        {
            string sql = String.Format("select employees.id, employees.surname, employees.name, employees.patronymic, employees.position,\n"
                + "employees.payment_type , employees.branch_subdivision, employees.fixed_salary, employees.hour_cost\n"
                + "from employees\n"
                + "inner join branches_subdivisions on branches_subdivisions.id = employees.branch_subdivision\n"
                + "inner join branсhes on branсhes.id = branches_subdivisions.branch\n"
                + "inner join subdivisions on subdivisions.id = branches_subdivisions.subdivision\n"
                + "where branсhes.name = '{0}' AND subdivisions.subdivision = '{1}'", branch, subdivision);

            return getEmployees(sql);
        }
        public List<Employee> getEmployees(string sql)
        {
            PaymentTypeService paymentTypeService = new PaymentTypeService(dBConnection, dataGridView);
            PositionService positionService = new PositionService(dBConnection, dataGridView);
            BranchSubdivisionService branchSubdivisionService = new BranchSubdivisionService(dBConnection, dataGridView);

            List<PaymentType> paymentTypes = paymentTypeService.getAllPaymentTypes();
            List<Position> positions = positionService.getAllPositions();
            List<BranchSubdivision> branchesSubdivisions = branchSubdivisionService.getAllBranchesSubdivisions();
            List<Employee> employees = new List<Employee>();

            DataTable employeesTable = dBConnection.SelectQuery(sql);

            foreach (DataRow row in employeesTable.Rows)
            {
                int id = (int)row.ItemArray[0];
                string surname = row.ItemArray[1].ToString();
                string name = row.ItemArray[2].ToString();
                string patronymic = row.ItemArray[3].ToString();
                int fixedSalary = row.ItemArray[7].ToString() == "" ? 0 : (int)row.ItemArray[7];
                int hoursCost = row.ItemArray[8].ToString() == "" ? 0 : (int)row.ItemArray[8];
                PaymentType searchPaymentType = paymentTypes.Where(x => x.Id == (int)row.ItemArray[5]).First();
                Position searchPosition = positions.Where(x => x.Id == (int)row.ItemArray[4]).First();
                BranchSubdivision searchBranchSubdivision = branchesSubdivisions.Where(x => x.Id == (int)row.ItemArray[6]).First();

                employees.Add(new Employee(id, surname, name, patronymic,
                    searchBranchSubdivision, searchPosition,
                    searchPaymentType, fixedSalary, hoursCost));
            }
            return employees;
        }

        public List<Employee> CalculateSalary(List<Employee> employees)
        {
            WorkHourService workHourService = new WorkHourService(dBConnection, dataGridView);
            MonthService monthService = new MonthService(dBConnection, dataGridView);

            foreach (Employee employee in employees)
            {
                string sql2 = String.Format("Select * from work_hours where employee = {0}" +
                    " and month = MONTH(CURRENT_DATE())", employee.Id);

                WorkHours workHours = workHourService.getWorkHours(sql2).FirstOrDefault();

                if (workHours != null)
                {
                    sql2 = "Select * from month_work_hours where id = MONTH(CURRENT_DATE())";

                    Month month = monthService.getMonths(sql2).First();
                    switch (employee.PaymentType.Id)
                    {
                        case 1:
                            employee.Salary = employee.FixedSalary / month.WorkHours * workHours.HoursCount;
                            break;
                        case 2:
                            employee.Salary = workHours.HoursCount * employee.HourCost;
                            break;
                    }
                }
                else
                {
                    continue;
                }
            }
            return employees;
        }

        public void addEmployee(string surname, string name, string patronymic,
            int position, int paymentType, int branchSubdivision, int fixedSalary, int hourCost)
        {
            string sql = String.Format("Insert into employees (surname, name, patronymic," +
                "position, payment_type, branch_subdivision, fixed_salary, hour_cost) " +
                " Values ('{0}','{1}','{2}','{3}',{4},{5},{6},{7})", surname, name, patronymic,
                position, paymentType, branchSubdivision, fixedSalary, hourCost);
            dBConnection.CUD(sql);
        }

        public void deleteEmployeeById(Employee employee)
        {
            string sql = String.Format("Delete from work_hours where employee = {0}", employee.Id);

            dBConnection.CUD(sql);

            sql = String.Format("Delete from employees where id = '{0}'", employee.Id);
            deleteEmployee(sql);
        }

        public void deleteEmployee(string sql)
        {
            dBConnection.CUD(sql);
        }

        public void updateEmployee(Employee employee)
        {
            string sql = String.Format("Update employees Set payment_type = {0}, fixed_salary = {1}, hour_cost = {2}" +
                " where id = {3}",
                employee.PaymentType.Id, employee.FixedSalary, employee.HourCost, employee.Id);
            dBConnection.CUD(sql);
        }
    }
}
