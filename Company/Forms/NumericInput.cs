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
    public partial class NumericInput : Form
    {
        private int value;
        public NumericInput()
        {
            InitializeComponent();
        }

        public int Value { get => value; set => this.value = value; }

        private void button1_Click(object sender, EventArgs e)
        {
            value = (int) numericUpDown1.Value;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
