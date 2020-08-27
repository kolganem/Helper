using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MyDiplom.Models;

namespace MyDiplom.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<Device> Devices { get; set; }
        public DbSet<Failure> Failures { get; set; }


        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }
    }
}
