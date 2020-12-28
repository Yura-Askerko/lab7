using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebApplication.Data;
using WebApplication.Models;

namespace WebApplication.Employees
{
    public partial class Employees : System.Web.UI.Page
    {
        private readonly HotelContext _context = new HotelContext();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
                GetEmployees();
        }

        private void GetEmployees()
        {
            IEnumerable<Employee> employees = _context.Employees.ToList();
            EmployeesGridView.DataSource = employees;
            EmployeesGridView.DataBind();
        }

        protected void EmployeesGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            EmployeesGridView.PageIndex = e.NewPageIndex;
            GetEmployees();
        }

        protected void AddEmployeeButton_Click(object sender, EventArgs e)
        {
            string fullName = EmployeeFullNameTextBox.Text;
            string position = EmployeePositionTextBox.Text;

            if (CheckValues(fullName, position))
            {
                Employee employee = new Employee
                {
                    FullName = fullName,
                    Position = position
                };

                _context.Employees.Add(employee);
                _context.SaveChanges();

                EmployeeFullNameTextBox.Text = string.Empty;
                EmployeePositionTextBox.Text = string.Empty;

                AddStatusLabel.Text = "Employee was successfully added.";

                EmployeesGridView.PageIndex = EmployeesGridView.PageCount;
                GetEmployees();
            }
        }

        protected void EmployeesGridView_RowEditing(object sender, GridViewEditEventArgs e)
        {
            EmployeesGridView.EditIndex = e.NewEditIndex;
            GetEmployees();
        }

        protected void EmployeesGridView_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            EmployeesGridView.EditIndex = -1;
            GetEmployees();
        }

        protected void EmployeesGridView_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            string fullName = (string)e.NewValues["FullName"];
            string position = (string)e.NewValues["Position"];

            if (CheckValues(fullName, position))
            {
                var row = EmployeesGridView.Rows[e.RowIndex];
                int id = int.Parse(row.Cells[1].Text);

                Employee employee = _context.Employees.FirstOrDefault(emp => emp.Id == id);

                employee.FullName = fullName;
                employee.Position = position;

                _context.SaveChanges();

                AddStatusLabel.Text = "Employee was successfully updated.";

                EmployeesGridView.EditIndex = -1;
                GetEmployees();
            }
        }

        protected void EmployeesGridView_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            var row = EmployeesGridView.Rows[e.RowIndex];
            int id = int.Parse(row.Cells[1].Text);

            Employee employee = _context.Employees.FirstOrDefault(emp => emp.Id == id);
            _context.Employees.Remove(employee);
            _context.SaveChanges();

            AddStatusLabel.Text = "Employee was successfully deleted.";
            GetEmployees();
        }

        private bool CheckValues(string fullName, string position)
        {
            if (string.IsNullOrEmpty(fullName))
            {
                AddStatusLabel.Text = "Incorrect 'Full Name' data.";
                return false;
            }

            if (string.IsNullOrEmpty(position))
            {
                AddStatusLabel.Text = "Incorrect 'Position' data.";
                return false;
            }

            return true;
        }
    }
}