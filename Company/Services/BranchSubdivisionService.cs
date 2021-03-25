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
        public BranchSubdivisionService(DBConnection dBConnection, DataGridView dataGridView) : base(dBConnection, dataGridView)
        { }
        public List<BranchSubdivision> getAllBranchesSubdivisions()
        {
            BranchService branchService = new BranchService(dBConnection, dataGridView);
            SubdivisionService subdivisionService = new SubdivisionService(dBConnection, dataGridView);

            List<Branch> branches = branchService.getAllBranches();
            List<Subdivision> subdivisions = subdivisionService.getAllSubdivisions();
            List<BranchSubdivision> branchesSubdivisions = new List<BranchSubdivision>();

            string sql = "Select * From branches_subdivisions ORDER BY id";
            DataTable branchesSubdivisionsTable = dBConnection.SelectQuery(sql);

            foreach (DataRow row in branchesSubdivisionsTable.Rows)
            {
                Branch searchBranch = branches.Where(x => x.Id == (int)row.ItemArray[1]).First();
                Subdivision searchSubdivision = subdivisions.Where(x => x.Id == (int)row.ItemArray[2]).First();
                branchesSubdivisions.Add(new BranchSubdivision((int)row.ItemArray[0], searchBranch, searchSubdivision));
            }
            return branchesSubdivisions;
        }
    }
}
