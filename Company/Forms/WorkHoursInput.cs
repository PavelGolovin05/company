using Company.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Company.Forms
{
    public partial class WorkHoursInput : Form
    {
        private int hoursCount, month;
        public WorkHoursInput(List<Month> months)
        {
            InitializeComponent();

            foreach (Month month in months)
            {
                listBox1.Items.Add(month.ToString());
            }
        }
        public int HoursCount { get => hoursCount; set => hoursCount = value; }
        public int Month { get => month; set => month = value; }

        private void button1_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex > -1)
            {
                hoursCount = (int) numericUpDown1.Value;
                month = listBox1.SelectedIndex + 1;
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show("Выберете месяц!");
            }
        }
    }
}
