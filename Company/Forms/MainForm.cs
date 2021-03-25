using Company.DB;
using Company.Entities;
using Company.Forms;
using Company.Services;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Company
{
    public partial class MainForm : Form
    {
        DBConnection dBConnection = new DBConnection("localhost", "root", "company", "1111", "utf8");
        List<Branch> branches;
        List<BranchSubdivision> branchesSubdivisions;
        List<City> cities;
        List<Employee> employees;
        List<Month> months;
        List<PaymentType> paymentTypes;
        List<Position> positions;
        List<Subdivision> subdivisions;
        List<WorkHours> workHours;
        BranchService branchService;
        BranchSubdivisionService branchSubdivisionService;
        CityService cityService;
        EmployeeService employeeService;
        MonthService monthService;
        PaymentTypeService paymentTypeService;
        PositionService positionService;
        SubdivisionService subdivisionService;
        WorkHourService workHourService;
        public MainForm()
        {
            InitializeComponent();    
            dBConnection.OpenConnection();
            branchService = new BranchService(dBConnection, dataGridView1);
            branchSubdivisionService = new BranchSubdivisionService(dBConnection, dataGridView1);
            cityService = new CityService(dBConnection, dataGridView1);
            employeeService = new EmployeeService(dBConnection, dataGridView1);
            monthService = new MonthService(dBConnection, dataGridView1);
            paymentTypeService = new PaymentTypeService(dBConnection, dataGridView1);
            positionService = new PositionService(dBConnection, dataGridView1);
            subdivisionService = new SubdivisionService(dBConnection, dataGridView1);
            workHourService = new WorkHourService(dBConnection, dataGridView1);
            initialData();
        }

        public void initialData()
        {   
            branchesSubdivisions = branchSubdivisionService.getAllBranchesSubdivisions();
            cities = cityService.getAllCities();
            employees = employeeService.getAllEmployees();
            employees = employeeService.CalculateSalary(employees);
            months = monthService.getAllMonths();
            paymentTypes = paymentTypeService.getAllPaymentTypes();
            positions = positionService.getAllPositions();
            workHours = workHourService.getAllWorkHours();
            label3.Text = "Сотрудники";

            fillListBoxes();
            fillDatatGridEmployees();
        }
        public void fillDatatGridEmployees()
        {
            dataGridView1.Rows.Clear();

            dataGridView1.ColumnCount = employeeService.getColumnNames().Count;
            for (int i = 0; i < dataGridView1.ColumnCount; i++)
            {
                dataGridView1.Columns[i].HeaderText = employeeService.getColumnNames()[i];
            }
            dataGridView1.RowCount = employees.Count + 1;

            for (int i = 0; i < employees.Count; i++)
            {
                dataGridView1[0, i].Value = employees[i].Id;
                dataGridView1[1, i].Value = employees[i].Surname;
                dataGridView1[2, i].Value = employees[i].Name;
                dataGridView1[3, i].Value = employees[i].Patronymic;
                dataGridView1[4, i].Value = employees[i].Position.GetPosition;
                dataGridView1[5, i].Value = employees[i].PaymentType.GetPaymentType;
                dataGridView1[6, i].Value = employees[i].BranchSubdivision.Branch.Name +
                        " " + employees[i].BranchSubdivision.Subdivision.GetSubdivision;
                dataGridView1[7, i].Value = employees[i].FixedSalary;
                dataGridView1[8, i].Value = employees[i].HourCost;
                dataGridView1[9, i].Value = employees[i].Salary;
            }
        }

        public void fillListBoxes()
        {
            branches = branchService.getAllBranches();
            subdivisions = subdivisionService.getAllSubdivisions();

            listBox1.Items.Clear();
            listBox2.Items.Clear();

            foreach (Branch branch in branches)
            {
                listBox1.Items.Add(branch.Name);
            }

            foreach (Subdivision subdivision in subdivisions)
            {
                listBox2.Items.Add(subdivision.GetSubdivision);
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox2.SelectedItem != null)
            {
                employees = employeeService.getChosenBranchSubdivisionEmployees(listBox1.SelectedItem.ToString(), listBox2.SelectedItem.ToString());
            }
            else
            {
                employees = employeeService.getChosenBranchEmployees(listBox1.SelectedItem.ToString());
            }

            fillDatatGridEmployees();
        }

        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem != null)
            {
                employees = employeeService.getChosenBranchSubdivisionEmployees(listBox1.SelectedItem.ToString(), listBox2.SelectedItem.ToString());
            }
            else
            {
                employees = employeeService.getChosenSubdivisionEmployees(listBox2.SelectedItem.ToString());
            }

            fillDatatGridEmployees();
        }

        private void главнаяToolStripMenuItem_Click(object sender, EventArgs e)
        {
            initialData();
        }

        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Вы точно хотите выйти?", "Выход", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                dBConnection.CloseConnetion();
                this.Close();
            }
        }

        //Филиал
        private void добавитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BranchSubdivisionInput input = new BranchSubdivisionInput(true, cities);
            if (input.ShowDialog() == DialogResult.OK)
            {
                City city = cities.Where(x => x.GetCity == input.City).First();
                Branch branch = branches.Where(x => x.Name == input.GetName).FirstOrDefault();
                if(branch == null)
                {
                    branchService.addBranch(input.GetName, city);
                    fillListBoxes();
                }
                else
                {
                    MessageBox.Show("Такой филиал уже существует!");
                }
            }
        }
        //Филиал
        private void удалитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem != null)
            {
                DialogResult dialogResult = MessageBox.Show("Вы точно хотите удалить филиал?", "Удаление", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    Branch branch = branches.Where(x => x.Name == listBox1.SelectedItem.ToString()).First();
                    branchService.deleteBranch(branch);
                    initialData();
                }
            }
            else
            {
                MessageBox.Show("Сначала выберете филиал!");
            }
        }
        //Подразделение
        private void добавитьToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            BranchSubdivisionInput input = new BranchSubdivisionInput(false, cities);
            if (input.ShowDialog() == DialogResult.OK)
            {
                Subdivision subdivision = subdivisions.Where(x => x.GetSubdivision == input.GetName).FirstOrDefault();
                if (subdivision == null)
                {
                    subdivisionService.addSubdivision(input.GetName);
                    initialData();
                }
                else
                {
                    MessageBox.Show("Такой филиал уже существует!");
                }
            }
        }
        //Подразделение
        private void удалитьToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (listBox2.SelectedItem != null)
            {
                DialogResult dialogResult = MessageBox.Show("Вы точно хотите удалить подразделение?", "Удаление", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    Subdivision subdivision = subdivisions.Where(x => x.GetSubdivision == listBox2.SelectedItem.ToString()).First();
                    subdivisionService.deleteSubdivision(subdivision);
                    initialData();
                }
            }
            else
            {
                MessageBox.Show("Сначала выберете подразделение!");
            }
        }
        //Сотрудник
        private void добавитьToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            EmployeeInput employeeInput = new EmployeeInput(branchesSubdivisions, branchSubdivisionService.getColumnNames(),
                positions, false);
            if (employeeInput.ShowDialog() == DialogResult.OK)
            {
                Position position = positions.Where(x => x.GetPosition == employeeInput.Position).First();
                PaymentType paymentType = paymentTypes.Where(x => x.Id == employeeInput.PaymentType).First();
                BranchSubdivision branchSubdivision = branchesSubdivisions.Where(x=>x.Id == employeeInput.BranchSubdivision).First();

                employeeService.addEmployee(employeeInput.Surname, employeeInput.GetName, employeeInput.Patronymic,
                    position.Id, paymentType.Id, branchSubdivision.Id, employeeInput.FixedSalary, employeeInput.HourCost);
                initialData();
            }
        }

        private void удалитьToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow != null)
            {
                DialogResult dialogResult = MessageBox.Show("Вы точно хотите удалить сотрудника?", "Удаление", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    int id = Convert.ToInt32(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[0].Value);
                    Employee employee = employees.Where(x => x.Id == id).First();
                    employeeService.deleteEmployeeById(employee);
                    initialData();
                }
            }
            else
            {
                MessageBox.Show("Сначала выберете сотрудника!");
            }
        }

        private void изменитьОплатуToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
            if (dataGridView1.CurrentRow != null)
            {
                int id = Convert.ToInt32(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[0].Value);
                Employee employee = employees.Where(x => x.Id == id).First();

                EmployeeInput employeeInput = new EmployeeInput(employee.Surname, employee.Name, employee.Patronymic,
                   employee.PaymentType.Id, employee.FixedSalary, employee.HourCost, true);
                if (employeeInput.ShowDialog() == DialogResult.OK)
                {
                    PaymentType paymentType = paymentTypes.Where(x => x.Id == employeeInput.PaymentType).First();
                    employee.PaymentType = paymentType;
                    employee.FixedSalary = employeeInput.FixedSalary;
                    employee.HourCost = employeeInput.HourCost;
                    employeeService.updateEmployee(employee);
                    initialData();
                }
            }
        }

        private void добавитьЧасыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow != null)
            {
                int id = Convert.ToInt32(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[0].Value);
                Employee employee = employees.Where(x => x.Id == id).First();

                WorkHoursInput workHours = new WorkHoursInput(months);
                if (workHours.ShowDialog() == DialogResult.OK)
                {
                    workHourService.addWorkHours(employee.Id, workHours.HoursCount, workHours.Month);
                }
;            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem != null && listBox2.SelectedItem != null)
            {
                Branch branch = branches.Where(x => x.Name == listBox1.SelectedItem.ToString()).First();
                Subdivision subdivision = subdivisions.Where(x => x.GetSubdivision == listBox2.SelectedItem.ToString()).First();
                BranchSubdivision branchSubdivision = branchesSubdivisions.Where(x => x.Branch.Id == branch.Id
                                                                           && x.Subdivision.Id == subdivision.Id).FirstOrDefault(); ;
                if (branchSubdivision == null)
                {
                    branchSubdivisionService.AddBranchSubdivision(branch, subdivision);
                    MessageBox.Show("Место работы успешно добавлено!");
                }
                else
                {
                    MessageBox.Show("Такое место работы уже есть!");
                }
            }
        }
    }
}
