//using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore.Design;
//using Microsoft.Extensions.Configuration;
//using System;
//using System.Collections.Generic;
//using System.IO;
//using System.Text;

//namespace WorkFlowData.Context
//{

//    public class DesignTimeDbContextFactoryContexto : IDesignTimeDbContextFactory<Contexto>
//    {
//        public Contexto CreateDbContext(string[] args)
//        {
//            IConfigurationRoot configuration = new ConfigurationBuilder()
//                .SetBasePath(Directory.GetCurrentDirectory())

//                .AddJsonFile("appsettings.json")
//                .Build();
//            var builder = new DbContextOptionsBuilder<Contexto>();
//            var connectionString = configuration.GetConnectionString("DefaultConnection");
//            builder.UseSqlServer(connectionString);
//            return new Contexto(builder.Options);
//        }
//    }
//}
