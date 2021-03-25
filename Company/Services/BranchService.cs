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
    class BranchService : Service
    {
        public BranchService(DBConnection dBConnection, DataGridView dataGridView) : base(dBConnection, dataGridView)
        { }

        public List<Branch> getAllBranches()
        {
            CityService cityService = new CityService(dBConnection, dataGridView);

            List<City> cities = cityService.getAllCities();
            List<Branch> branches = new List<Branch>();

            string sql = "Select * From branсhes ORDER BY name";
            DataTable branchesTable = dBConnection.SelectQuery(sql);

            foreach (DataRow row in branchesTable.Rows)
            {
                City searchCity = cities.Where(x => x.Id == (int)row.ItemArray[1]).First();
                branches.Add(new Branch((int)row.ItemArray[0], row.ItemArray[2].ToString(), searchCity));
            }
            return branches;
        }

        public void addBranch(string name, City city)
        {
            string sql = String.Format("Insert into branсhes (name, city) VALUES ('{0}', '{1}')", name, city.Id);
            dBConnection.CUD(sql);
        }
        //Каскадное удаление
        public void deleteBranch(Branch branch)
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
                " inner join branсhes on branсhes.id = branches_subdivisions.branch" +
                " where branches_subdivisions.branch ='{0}'", branch.Id);
            List<Employee> employees = employeeService.getEmployees(sql);

            sql = String.Format("Select * from branches_subdivisions where branches_subdivisions.branch = '{0}'", branch.Id);

            List<BranchSubdivision> branchesSubdivisions = branchSubdivisionService.getBranchesSubdivisions(sql);

            foreach (Employee employee in employees)
            {
                sql = String.Format("Select * from work_hours where work_hours.employee = '{0}'", employee.Id);

                List<WorkHours> workHours = workHourService.getWorkHours(sql);

                foreach(WorkHours workHour in workHours)
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

            sql = String.Format("Delete From branсhes Where id = '{0}'", branch.Id);
            dBConnection.CUD(sql);
        }
    }
}
