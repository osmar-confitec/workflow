//using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore.Design;
//using Microsoft.Extensions.Configuration;
//using System;
//using System.Collections.Generic;
//using System.IO;
//using System.Text;

//namespace WorkFlowData.EventSourcing.Context
//{

//    public class DesignTimeDbContextFactoryEventStoreContexto : IDesignTimeDbContextFactory<EventStoreContexto>
//    {
//        public EventStoreContexto CreateDbContext(string[] args)
//        {
//            IConfigurationRoot configuration = new ConfigurationBuilder()
//                .SetBasePath(Directory.GetCurrentDirectory())

//                .AddJsonFile("appsettings.json")
//                .Build();
//            var builder = new DbContextOptionsBuilder<EventStoreContexto>();
//            var connectionString = configuration.GetConnectionString("DefaultConnection");
//            builder.UseSqlServer(connectionString);
//            return new EventStoreContexto(builder.Options);
//        }
//    }
//}
