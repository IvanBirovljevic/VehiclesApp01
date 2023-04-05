﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project.Service.Interfaces;

namespace Project.Service.Models
{    
    public class VehicleMake : IVehicleMake
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public string? Abrv { get; set; }

        public virtual ICollection<VehicleModel>? VehicleModels { get; set; }

    }
}
