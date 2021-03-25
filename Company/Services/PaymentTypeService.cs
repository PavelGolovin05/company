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
    class PaymentTypeService : Service
    {
        public PaymentTypeService(DBConnection dBConnection, DataGridView dataGridView) : base(dBConnection, dataGridView)
        { }
        public List<PaymentType> getAllPaymentTypes()
        {
            List<PaymentType> paymentTypes = new List<PaymentType>();

            string sql = "Select * From payment_types ORDER BY id";
            DataTable paymentTypesTable = dBConnection.SelectQuery(sql);

            foreach (DataRow row in paymentTypesTable.Rows)
            {
                paymentTypes.Add(new PaymentType((int)row.ItemArray[0], row.ItemArray[1].ToString()));
            }
            return paymentTypes;
        }
    }
}
