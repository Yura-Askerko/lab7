using System;
using WebApplication.Models;
using System.Data.Entity;

namespace WebApplication.Data
{
    public partial class HotelContext : DbContext
    {
        public HotelContext() 
            : base("name=SqlConnection")
        {
            Database.CreateIfNotExists();
        }

        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<Service> Services { get; set; }
        public virtual DbSet<ServiceType> ServiceTypes { get; set; }
    }
}
