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

            string sql = "Select * From subdivisions ORDER BY id";
            DataTable subdivisionsTable = dBConnection.SelectQuery(sql);

            foreach (DataRow row in subdivisionsTable.Rows)
            {
                subdivisions.Add(new Subdivision((int)row.ItemArray[0], row.ItemArray[1].ToString()));
            }
            return subdivisions;
        }
    }
}
