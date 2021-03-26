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
    class BranchSubdivisionService : Service
    {
        public BranchSubdivisionService(DBConnection dBConnection) : base(dBConnection)
        { }
        public List<BranchSubdivision> getAllBranchesSubdivisions()
        {
            string sql = "Select * From branches_subdivisions ORDER BY id";
            return getBranchesSubdivisions(sql);
        }
        public List<String> getColumnNames()
        {
            List<String> columnNames = new List<string>();
            string sql = "Select * From branches_subdivisions limit 1";
            DataTable employeesTable = dBConnection.SelectQuery(sql);
            foreach (DataColumn column in employeesTable.Columns)
            {
                columnNames.Add(column.ColumnName.ToString());
            }
            return columnNames;
        }
        public List<BranchSubdivision> getBranchesSubdivisions(string sql)
        {
            BranchService branchService = new BranchService(dBConnection);
            SubdivisionService subdivisionService = new SubdivisionService(dBConnection);

            List<Branch> branches = branchService.getAllBranches();
            List<Subdivision> subdivisions = subdivisionService.getAllSubdivisions();
            List<BranchSubdivision> branchesSubdivisions = new List<BranchSubdivision>();

            DataTable branchesSubdivisionsTable = dBConnection.SelectQuery(sql);

            foreach (DataRow row in branchesSubdivisionsTable.Rows)
            {
                Branch searchBranch = branches.Where(x => x.Id == (int)row.ItemArray[1]).First();
                Subdivision searchSubdivision = subdivisions.Where(x => x.Id == (int)row.ItemArray[2]).First();
                branchesSubdivisions.Add(new BranchSubdivision((int)row.ItemArray[0], searchBranch, searchSubdivision));
            }
            return branchesSubdivisions;
        }

        public void AddBranchSubdivision(Branch branch, Subdivision subdivision)
        {
            string sql = String.Format("insert into branches_subdivisions (branch, subdivision)" +
                "Values({0}, {1})", branch.Id, subdivision.Id);
            dBConnection.CUD(sql);
        }
    }
}
