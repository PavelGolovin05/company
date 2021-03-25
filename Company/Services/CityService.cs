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
    class CityService : Service
    {
        public CityService(DBConnection dBConnection, DataGridView dataGridView) : base(dBConnection, dataGridView)
        { }
        public List<City> getAllCities()
        {
            List<City> cities = new List<City>();

            string sql = "Select * From cities ORDER BY id";
            DataTable citiesTable = dBConnection.SelectQuery(sql);

            foreach (DataRow row in citiesTable.Rows)
            {
                cities.Add(new City((int)row.ItemArray[0], row.ItemArray[1].ToString()));
            }
            return cities;
        }
    }
}
