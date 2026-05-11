using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NextHouse.Domain.Entities.Account;
using NextHouse.Domain.Entities.Location;
using NextHouse.Domain.Entities.Property;
using NextHouse.Domain.Entities.Request;
using NextHouse.Persistence.Entities;
using System;
using System.Collections.Generic;
using System.Reflection.Metadata;
using System.Text;
using static System.Collections.Specialized.BitVector32;

namespace NextHouse.Persistence
{
    public class DataContext : IdentityUserContext<ApplicationUser>
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        public DbSet<Property> Properties { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<RolePermission> RolePermissions { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<PropertyImage> PropertyImages { get; set; }

        public DbSet<PropertyRequest> PropertyRequests { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(DataContext).Assembly);

            base.OnModelCreating(modelBuilder);
        }
    }
}
