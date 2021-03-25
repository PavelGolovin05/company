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
            PaymentTypeService paymentTypeService = new PaymentTypeService(dBConnection, dataGridView);
            PositionService positionService = new PositionService(dBConnection, dataGridView);
            BranchSubdivisionService branchSubdivisionService = new BranchSubdivisionService(dBConnection, dataGridView);

            List<PaymentType> paymentTypes = paymentTypeService.getAllPaymentTypes();
            List<Position> positions = positionService.getAllPositions();
            List<BranchSubdivision> branchesSubdivisions = branchSubdivisionService.getAllBranchesSubdivisions();
            List<Employee> employees = new List<Employee>();

            string sql = "Select * From employees ORDER BY id";
            DataTable employeesTable = dBConnection.SelectQuery(sql);

            foreach (DataRow row in employeesTable.Rows)
            {
                PaymentType searchPaymentType = paymentTypes.Where(x => x.Id == (int)row.ItemArray[5]).First();
                Position searchPosition = positions.Where(x => x.Id == (int)row.ItemArray[4]).First();
                BranchSubdivision searchBranchSubdivision = branchesSubdivisions.Where(x => x.Id == (int)row.ItemArray[6]).First();

                employees.Add(new Employee((int)row.ItemArray[0], row.ItemArray[1].ToString(),
                    row.ItemArray[2].ToString(), row.ItemArray[3].ToString(), searchBranchSubdivision,
                    searchPosition, searchPaymentType,
                    row.ItemArray[7].ToString() == "" ? 0 : (int)row.ItemArray[7],
                    row.ItemArray[8].ToString() == "" ? 0 : (int)row.ItemArray[8]));
            }
            return employees;
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
            return columnNames;
        }
    }
}
