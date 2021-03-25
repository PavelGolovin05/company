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

            string sql = "Select * From branсhes ORDER BY id";
            DataTable branchesTable = dBConnection.SelectQuery(sql);

            foreach (DataRow row in branchesTable.Rows)
            {
                City searchCity = cities.Where(x => x.Id == (int)row.ItemArray[1]).First();
                branches.Add(new Branch((int)row.ItemArray[0], row.ItemArray[2].ToString(), searchCity));
            }
            return branches;
        }
    }
}
