﻿using Company.DB;
using System;
using System.Collections.Generic;
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
    }
}
