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
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void главнаяToolStripMenuItem_Click(object sender, EventArgs e)
        {

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
            
        }
    }
}
