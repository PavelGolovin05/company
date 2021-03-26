using Company.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Company.Services
{
    class Service
    {
        protected DBConnection dBConnection;
        public Service(DBConnection dBConnection)
        {
            this.dBConnection = dBConnection;
        }
    }
}
