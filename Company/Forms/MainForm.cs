using Company.DB;
using Company.Entities;
using Company.Services;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
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

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void главнаяToolStripMenuItem_Click(object sender, EventArgs e)
        {
            initialData();
        }

        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Вы точно хотите выйти?","Выход", MessageBoxButtons.YesNo);
            if(dialogResult == DialogResult.Yes)
            {
                dBConnection.CloseConnetion();
                this.Close();
            }
        }

        public void initialData()
        {   
            branches = branchService.getAllBranches();
            branchesSubdivisions = branchSubdivisionService.getAllBranchesSubdivisions();
            cities = cityService.getAllCities();
            employees = employeeService.getAllEmployees();
            months = monthService.getAllMonths();
            paymentTypes = paymentTypeService.getAllPaymentTypes();
            positions = positionService.getAllPositions();
            subdivisions = subdivisionService.getAllSubdivisions();
            workHours = workHourService.getAllWorkHours();

            listBox1.Items.Clear();
            listBox2.Items.Clear();
            label3.Text = "Сотрудники";

            foreach(Branch branch in branches)
            {
                listBox1.Items.Add(branch.Name);
            }

            foreach (Subdivision subdivision in subdivisions)
            {
                listBox2.Items.Add(subdivision.GetSubdivision);
            }

            dataGridView1.ColumnCount = employeeService.getColumnNames().Count;
            for (int i = 0; i < dataGridView1.ColumnCount; i++)
            {
                dataGridView1.Columns[i].HeaderText = employeeService.getColumnNames()[i];
            }
            dataGridView1.RowCount = employees.Count;

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
            }
        }
    }
}
