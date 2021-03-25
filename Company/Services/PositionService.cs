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
    class PositionService : Service
    {
        public PositionService(DBConnection dBConnection, DataGridView dataGridView) : base(dBConnection, dataGridView)
        { }
        public List<Position> getAllPositions()
        {
            List<Position> positions = new List<Position>();

            string sql = "Select * From positions ORDER BY id";
            DataTable positionTable = dBConnection.SelectQuery(sql);

            foreach (DataRow row in positionTable.Rows)
            {
                positions.Add(new Position((int)row.ItemArray[0], row.ItemArray[1].ToString()));
            }
            return positions;
        }
    }
}
