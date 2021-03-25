using Company.DB;
using System;
using System.Collections.Generic;
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
    }
}
