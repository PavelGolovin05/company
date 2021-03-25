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
    public partial class BranchSubdivisionInput : Form
    {
        private string name;
        private string city;
        private bool isBranch;
        public BranchSubdivisionInput(bool isBranch, List<City> cities)
        {
            InitializeComponent();

            this.isBranch = isBranch;
            if(!isBranch)
            {
                label2.Visible = false;
                listBox1.Visible = false;
                this.Width -= 140;
            }
            else
            {
                foreach (City city in cities)
                {
                    listBox1.Items.Add(city.GetCity);
                }
            }
        }

        public string GetName { get => name; set => name = value; }
        public string City { get => city; set => city = value; }

        private void button1_Click(object sender, EventArgs e)
        {
            bool correct = false;
            if (textBox1.Text.Length > 0)
            {
                name = textBox1.Text;
                if (isBranch)
                {
                    if (listBox1.SelectedItem != null) 
                    {
                        correct = true;
                        city = listBox1.SelectedItem.ToString();
                    }
                    else
                    {
                        MessageBox.Show("Выберете город из списка!");
                    }
                }
                else
                {
                    correct = true;
                }
                if (correct)
                {
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
            }
            else
            {
                MessageBox.Show("Поле не заполнено!");
            }
        }
    }
}
