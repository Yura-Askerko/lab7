using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebApplication.Models;
using System.Data.Entity;
using WebApplication.Data;

namespace WebApplication.Services
{
    public partial class Services : System.Web.UI.Page
    {
        private readonly HotelContext _context = new HotelContext();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
                GetServices();
        }

        private void GetServices()
        {
            IEnumerable<Service> services = _context.Services.Include(s => s.ServiceType).Include(s => s.Employee).OrderBy(s => s.Id).ToList();
            ServicesGridView.DataSource = services;
            ServicesGridView.DataBind();
        }

        protected void ServicesGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            ServicesGridView.PageIndex = e.NewPageIndex;
            GetServices();
        }

        protected void AddServiceButton_Click(object sender, EventArgs e)
        {
            decimal cost = decimal.TryParse(ServiceCostTextBox.Text, out cost) == true ? cost : default;
            int serviceTypeId = int.Parse(ServiceTypesDropDownList.SelectedValue);
            int employeeId = int.Parse(EmployeesDropDownList.SelectedValue);

            if (CheckValues(cost))
            {
                Service service = new Service
                {
                    Cost = cost,
                    ServiceTypeId = serviceTypeId,
                    EmployeeId = employeeId
                };

                _context.Services.Add(service);
                _context.SaveChanges();

                ServiceCostTextBox.Text = string.Empty;
                ServiceTypesDropDownList.SelectedIndex = 0;
                EmployeesDropDownList.SelectedIndex = 0;

                AddStatusLabel.Text = "Service was successfully added.";

                ServicesGridView.PageIndex = ServicesGridView.PageCount;
                GetServices();
            }
        }

        protected void ServicesGridView_RowEditing(object sender, GridViewEditEventArgs e)
        {
            ServicesGridView.EditIndex = e.NewEditIndex;
            GetServices();
        }

        protected void ServicesGridView_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            ServicesGridView.EditIndex = -1;
            GetServices();
        }

        protected void ServicesGridView_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            decimal cost = decimal.TryParse((string)e.NewValues["Cost"], out cost) == true ? cost : default;
            int serviceTypeId = int.Parse((string)e.NewValues["ServiceTypeId"]);
            int employeeId = int.Parse((string)e.NewValues["EmployeeId"]);

            if (CheckValues(cost))
            {
                var row = ServicesGridView.Rows[e.RowIndex];
                int id = int.Parse(row.Cells[1].Text);

                Service service = _context.Services.FirstOrDefault(s => s.Id == id);

                service.Cost = cost;
                service.ServiceTypeId = serviceTypeId;
                service.EmployeeId = employeeId;

                _context.SaveChanges();

                AddStatusLabel.Text = "Service was successfully updated.";

                ServicesGridView.EditIndex = -1;
                GetServices();
            }
        }

        protected void ServicesGridView_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            var row = ServicesGridView.Rows[e.RowIndex];
            int id = int.Parse(row.Cells[1].Text);

            Service service = _context.Services.FirstOrDefault(s => s.Id == id);
            _context.Services.Remove(service);
            _context.SaveChanges();

            AddStatusLabel.Text = "Service was successfully updated.";
            GetServices();
        }

        private bool CheckValues(decimal cost)
        {
            if (cost == default)
            {
                AddStatusLabel.Text = "Incorrect 'Cost' data.";
                return false;
            }

            return true;
        }
    }
}