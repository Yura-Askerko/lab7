using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebApplication.Data;
using WebApplication.Models;

namespace WebApplication.ServiceTypes
{
    public partial class ServiceTypes : System.Web.UI.Page
    {
        private readonly HotelContext _context = new HotelContext();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
                GetServiceTypes();
        }
        private void GetServiceTypes()
        {
            IEnumerable<ServiceType> serviceTypes = _context.ServiceTypes.ToList();
            ServiceTypesGridView.DataSource = serviceTypes;
            ServiceTypesGridView.DataBind();
        }

        protected void ServiceTypesGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            ServiceTypesGridView.PageIndex = e.NewPageIndex;
            GetServiceTypes();
        }

        protected void AddServiceTypeButton_Click(object sender, EventArgs e)
        {
            string name = ServiceTypeNameTextBox.Text;
            string specification = ServiceTypeSpecificaionTextBox.Text;

            if (CheckValues(name, specification))
            {
                ServiceType serviceType = new ServiceType
                {
                    Name = name,
                    Specificaion = specification
                };

                _context.ServiceTypes.Add(serviceType);
                _context.SaveChanges();

                ServiceTypeNameTextBox.Text = string.Empty;
                ServiceTypeSpecificaionTextBox.Text = string.Empty;

                AddStatusLabel.Text = "Service type was successfully added.";

                ServiceTypesGridView.PageIndex = ServiceTypesGridView.PageCount;
                GetServiceTypes();
            }
        }

        protected void ServiceTypesGridView_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            ServiceTypesGridView.EditIndex = -1;
            GetServiceTypes();
        }

        protected void ServiceTypesGridView_RowEditing(object sender, GridViewEditEventArgs e)
        {
            ServiceTypesGridView.EditIndex = e.NewEditIndex;
            GetServiceTypes();
        }

        protected void ServiceTypesGridView_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            string name = (string)e.NewValues["Name"];
            string specification = (string)e.NewValues["Specificaion"];

            if (CheckValues(name, specification))
            {
                var row = ServiceTypesGridView.Rows[e.RowIndex];
                int id = int.Parse(row.Cells[1].Text);

                ServiceType serviceType = _context.ServiceTypes.FirstOrDefault(s => s.Id == id);

                serviceType.Name = name;
                serviceType.Specificaion = specification;

                _context.SaveChanges();

                AddStatusLabel.Text = "Service type was successfully updated.";

                ServiceTypesGridView.EditIndex = -1;
                GetServiceTypes();
            }
        }

        protected void ServiceTypesGridView_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            var row = ServiceTypesGridView.Rows[e.RowIndex];
            int id = int.Parse(row.Cells[1].Text);

            ServiceType serviceType = _context.ServiceTypes.FirstOrDefault(s => s.Id == id);
            _context.ServiceTypes.Remove(serviceType);
            _context.SaveChanges();

            AddStatusLabel.Text = "Service type was successfully deleted.";
            GetServiceTypes();
        }

        private bool CheckValues(string name, string specification)
        {
            if (string.IsNullOrEmpty(name))
            {
                AddStatusLabel.Text = "Incorrect 'Name' data.";
                return false;
            }

            if (string.IsNullOrEmpty(specification))
            {
                AddStatusLabel.Text = "Incorrect 'Specification' data.";
                return false;
            }

            return true;
        }
    }
}