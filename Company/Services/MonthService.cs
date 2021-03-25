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
    class MonthService : Service
    {
        public MonthService(DBConnection dBConnection, DataGridView dataGridView) : base(dBConnection, dataGridView)
        { }
        public List<Month> getAllMonths()
        {
            List<Month> months = new List<Month>();

            string sql = "Select * From month_work_hours ORDER BY id";
            DataTable monthsTable = dBConnection.SelectQuery(sql);

            foreach (DataRow row in monthsTable.Rows)
            {
                months.Add(new Month((int)row.ItemArray[0], (int) row.ItemArray[1], row.ItemArray[2].ToString()));
            }
            return months;
        }
    }
}
