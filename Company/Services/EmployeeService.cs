﻿using Company.DB;
using System;
using System.Collections.Generic;
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
    }
}
