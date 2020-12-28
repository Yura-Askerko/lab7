using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebApplication.Models
{
    public partial class Service
    {
        public int Id { get; set; }
        public decimal Cost { get; set; }
        public int ServiceTypeId { get; set; }
        public int EmployeeId { get; set; }

        public virtual Employee Employee { get; set; }
        public virtual ServiceType ServiceType { get; set; }
    }
}
