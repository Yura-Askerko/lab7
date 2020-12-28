using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebApplication.Models
{
    public partial class Employee
    {
        public Employee()
        {
            Services = new HashSet<Service>();
        }

        public int Id { get; set; }
        public string FullName { get; set; }
        public string Position { get; set; }

        public virtual ICollection<Service> Services { get; set; }
    }
}
