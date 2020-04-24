using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using WorkFlowIdentity.Models;

namespace WorkFlowIdentity.Context
{
    public class ApplicationDbContextIdentity : IdentityDbContext<ApplicationUser>
    {

        private readonly Microsoft.AspNetCore.Hosting.IWebHostEnvironment _env;

        public ApplicationDbContextIdentity(Microsoft.AspNetCore.Hosting.IWebHostEnvironment env)/*,
                                                     DbContextOptions<ApplicationDbContextIdentity> options):this(options)*/
        {
            _env = env;

        }


    //public ApplicationDbContextIdentity(DbContextOptions<ApplicationDbContextIdentity> options)
    //: base(options)
    //{

    //}

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var config = new ConfigurationBuilder()
                 .SetBasePath(_env == null ? Directory.GetCurrentDirectory() : _env.ContentRootPath)
                 .AddJsonFile("appsettings.json")
                 .Build();

            optionsBuilder.UseSqlServer(config.GetConnectionString("DefaultConnection"));
        }
    }
}
