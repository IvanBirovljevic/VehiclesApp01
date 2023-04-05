using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Project.Service.Interfaces;
using Project.Service.Models;

namespace Project.MVC.Data
{
    public class ProjectMVCContext : DbContext
    {
        public ProjectMVCContext (DbContextOptions<ProjectMVCContext> options)
            : base(options)
        {
        }

        public DbSet<Project.Service.Models.VehicleMake> VehicleMake { get; set; } = default!;

        public DbSet<Project.Service.Models.VehicleModel>? VehicleModel { get; set; }

        public async override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await base.SaveChangesAsync(cancellationToken);
        }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=Project.MVC.Data;Trusted_Connection=True;MultipleActiveResultSets=true");
            optionsBuilder.UseLazyLoadingProxies();
        }
    }
}

