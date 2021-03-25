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
    class SubdivisionService : Service
    {
        public SubdivisionService(DBConnection dBConnection, DataGridView dataGridView) : base(dBConnection, dataGridView)
        { }
        public List<Subdivision> getAllSubdivisions()
        {
            List<Subdivision> subdivisions = new List<Subdivision>();

            string sql = "Select * From subdivisions ORDER BY subdivision";
            DataTable subdivisionsTable = dBConnection.SelectQuery(sql);

            foreach (DataRow row in subdivisionsTable.Rows)
            {
                subdivisions.Add(new Subdivision((int)row.ItemArray[0], row.ItemArray[1].ToString()));
            }
            return subdivisions;
        }

        public void addSubdivision(string name)
        {
            string sql = String.Format("Insert into subdivisions (subdivision) VALUES ('{0}')", name);
            dBConnection.CUD(sql);
        }
        public void deleteSubdivision(Subdivision subdivision)
        {
            EmployeeService employeeService = new EmployeeService(dBConnection, dataGridView);
            BranchSubdivisionService branchSubdivisionService = new BranchSubdivisionService(dBConnection, dataGridView);
            WorkHourService workHourService = new WorkHourService(dBConnection, dataGridView);

            string sql = String.Format("select employees.id, employees.surname, " +
                " employees.name, employees.patronymic, employees.position," +
                " employees.payment_type , employees.branch_subdivision, " +
                " employees.fixed_salary, employees.hour_cost" +
                " from employees " +
                " inner join branches_subdivisions on branches_subdivisions.id = employees.branch_subdivision" +
                " inner join subdivisions on subdivisions.id = branches_subdivisions.subdivision" +
                " where branches_subdivisions.subdivision ='{0}'", subdivision.Id);
            List<Employee> employees = employeeService.getEmployees(sql);

            sql = String.Format("Select * from branches_subdivisions where branches_subdivisions.subdivision = '{0}'", subdivision.Id);

            List<BranchSubdivision> branchesSubdivisions = branchSubdivisionService.getBranchesSubdivisions(sql);

            foreach (Employee employee in employees)
            {
                sql = String.Format("Select * from work_hours where work_hours.employee = '{0}'", employee.Id);

                List<WorkHours> workHours = workHourService.getWorkHours(sql);

                foreach (WorkHours workHour in workHours)
                {
                    sql = String.Format("Delete from work_hours where id = '{0}'", workHour.Id);
                    dBConnection.CUD(sql);
                }

                sql = String.Format("Delete from employees where id = '{0}'", employee.Id);
                dBConnection.CUD(sql);
            }

            foreach (BranchSubdivision branchSubdivision in branchesSubdivisions)
            {
                sql = sql = String.Format("Delete from branches_subdivisions where id = '{0}'", branchSubdivision.Id);
                dBConnection.CUD(sql);
            }

            sql = String.Format("Delete From subdivisions Where id = '{0}'", subdivision.Id);
            dBConnection.CUD(sql);
        }
    }
}
