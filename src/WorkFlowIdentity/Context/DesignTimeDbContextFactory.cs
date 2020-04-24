//using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore.Design;
//using Microsoft.Extensions.Configuration;
//using System;
//using System.Collections.Generic;
//using System.IO;
//using System.Text;

//namespace WorkFlowIdentity.Context
//{
//    public class DesignTimeDbContextFactoryIdentity : IDesignTimeDbContextFactory<ApplicationDbContextIdentity>
//    {
//        public ApplicationDbContextIdentity CreateDbContext(string[] args)
//        {
//            IConfigurationRoot configuration = new ConfigurationBuilder()
//                .SetBasePath(Directory.GetCurrentDirectory())
//                .AddJsonFile("appsettings.json")
//                .Build();
//            var builder = new DbContextOptionsBuilder<ApplicationDbContextIdentity>();
//            var connectionString = configuration.GetConnectionString("DefaultConnection");
//            builder.UseSqlServer(connectionString);
//            return new ApplicationDbContextIdentity(builder.Options);
//        }
//    }
//}
