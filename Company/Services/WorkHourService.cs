﻿using Company.DB;
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
    class WorkHourService : Service
    {
        public WorkHourService(DBConnection dBConnection, DataGridView dataGridView) : base(dBConnection, dataGridView)
        { }
        public List<WorkHours> getAllWorkHours()
        {
            string sql = "Select * From work_hours ORDER BY id";
            return getWorkHours(sql);
        }

        public List<WorkHours> getWorkHours(string sql)
        {
            MonthService monthService = new MonthService(dBConnection, dataGridView);
            EmployeeService employeeService = new EmployeeService(dBConnection, dataGridView);

            List<Month> months = monthService.getAllMonths();
            List<Employee> employees = employeeService.getAllEmployees();

            List<WorkHours> workHours = new List<WorkHours>();
            DataTable workHoursTable = dBConnection.SelectQuery(sql);

            foreach (DataRow row in workHoursTable.Rows)
            {
                Month searchMonth = months.Where(x => x.Id == (int)row.ItemArray[3]).First();
                Employee searchEmployee = employees.Where(x => x.Id == (int)row.ItemArray[1]).First();

                workHours.Add(new WorkHours((int)row.ItemArray[0], (int)row.ItemArray[2], searchEmployee, searchMonth));
            }
            return workHours;
        }

        public void addWorkHours(int employee, int hoursCount, int month)
        {
            string sql = String.Format("Insert into work_hours (employee, hours_count, month)" +
                " Values({0}, {1}, {2})", employee, hoursCount, month);
            dBConnection.CUD(sql);
        }
    }
}
