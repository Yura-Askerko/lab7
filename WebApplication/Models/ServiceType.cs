﻿using System;
using System.Collections.Generic;

namespace WebApplication.Models
{
    public partial class ServiceType
    {
        public ServiceType()
        {
            Services = new HashSet<Service>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Specificaion { get; set; }

        public virtual ICollection<Service> Services { get; set; }
    }
}
