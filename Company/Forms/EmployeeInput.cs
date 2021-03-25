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
    public partial class EmployeeInput : Form
    {
        string surname, name, patronymic, position;
        int paymentType, fixedSalary, hourCost, branchSubdivision;
        bool isUpdate;
        public EmployeeInput(List<BranchSubdivision> branchesSubdivisions, List<String> columnNames, List<Position> positions, bool isUpdate)
        {
            InitializeComponent();
            this.isUpdate = isUpdate;
            radioButton1.Checked = true;
           
            dataGridView1.RowCount = branchesSubdivisions.Count;
            dataGridView1.ColumnCount = columnNames.Count;
            for (int i = 0; i < dataGridView1.ColumnCount; i++)
            {
                dataGridView1.Columns[i].HeaderText = columnNames[i];
            }

            for (int i = 0; i < branchesSubdivisions.Count; i++)
            {
                dataGridView1[0, i].Value = branchesSubdivisions[i].Id;
                dataGridView1[1, i].Value = branchesSubdivisions[i].Branch.Name;
                dataGridView1[2, i].Value = branchesSubdivisions[i].Subdivision.GetSubdivision;
            }
            foreach (Position position in positions)
            {
                listBox1.Items.Add(position.GetPosition);
            }
        }
        public EmployeeInput(String surname, String name, String patronymic, int paymentType,
            int fixedSalary, int hourCost,  bool isUpdate)
        {
            InitializeComponent();
            this.isUpdate = isUpdate;
            dataGridView1.Visible = false;
            listBox1.Visible = false;
            label5.Visible = false;
            label6.Visible = false;
            this.surname = surname;
            this.patronymic = patronymic;
            this.paymentType = paymentType;
            this.fixedSalary = fixedSalary;
            this.hourCost = hourCost;
            textBox1.Text = surname;
            textBox2.Text = name;
            textBox3.Text = patronymic;
            if (fixedSalary == 0)
            {
                numericUpDown1.Value = hourCost;
            }
            else
            {
                numericUpDown1.Value = fixedSalary;
            }

            if (paymentType == 1)
            {
                radioButton1.Checked = true;
            }
            else
            {
                radioButton2.Checked = true;
            }
            this.Width -= 780;        
        }
        public string Surname { get => surname; set => surname = value; }
        public string GetName { get => name; set => name = value; }
        public string Patronymic { get => patronymic; set => patronymic = value; }
        public string Position { get => position; set => position = value; }
        public int PaymentType { get => paymentType; set => paymentType = value; }
        public int FixedSalary { get => fixedSalary; set => fixedSalary = value; }
        public int HourCost { get => hourCost; set => hourCost = value; }
        public int BranchSubdivision { get => branchSubdivision; set => branchSubdivision = value; }

        private void button1_Click(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                paymentType = 1;
                fixedSalary = (int)numericUpDown1.Value;
                hourCost = 0;
            }
            else if (radioButton2.Checked)
            {
                paymentType = 2;
                hourCost = (int)numericUpDown1.Value;
                fixedSalary = 0;
            }
            if (isUpdate)
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                if (textBox1.Text.Length > 0)
                {
                    surname = textBox1.Text;
                    if (textBox2.Text.Length > 0)
                    {
                        name = textBox2.Text;
                        if (textBox3.Text.Length > 0)
                        {
                            patronymic = textBox3.Text;
                            if (listBox1.SelectedItem != null)
                            {
                                position = listBox1.SelectedItem.ToString();

                                if (dataGridView1.CurrentRow != null)
                                {
                                    branchSubdivision = Convert.ToInt32(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[0].Value);
                                    this.DialogResult = DialogResult.OK;
                                    this.Close();
                                }
                                else
                                {
                                    MessageBox.Show("Выберете место работы!");
                                }
                            }
                            else
                            {
                                MessageBox.Show("Выберете должность");
                            }
                        }
                        else
                        {
                            MessageBox.Show("Введите отчество");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Введите имя");
                    }
                }
                else
                {
                    MessageBox.Show("Введите фамилию");
                }
            }          
        }
    }
}
