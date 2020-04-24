using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using WorkFlowData.EventSourcing.Mappings;
using WorkFlowDominio.Entities.StoreEvents;
using WorkFlowDominioShared.Events;

namespace WorkFlowData.EventSourcing.Context
{
    public class EventStoreContexto : DbContext
    {

        readonly Microsoft.AspNetCore.Hosting.IWebHostEnvironment _env;

        
        public DbSet<ClienteStore> ClienteStores { get; set; }

        public EventStoreContexto(Microsoft.AspNetCore.Hosting.IWebHostEnvironment env)/*,
                                           DbContextOptions<EventStoreContexto> options) : this(options)*/
        {
            _env = env;

        }

        //public EventStoreContexto(DbContextOptions<EventStoreContexto> options)
        // : base(options)
        //{


        //}
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // get the configuration from the app settings
            var config = new ConfigurationBuilder()
                .SetBasePath(_env == null ? Directory.GetCurrentDirectory() : _env.ContentRootPath)
                //.SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            // define the database to use
            optionsBuilder.UseSqlServer(config.GetConnectionString("DefaultConnection"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ClienteStoreMapping());
            modelBuilder.ApplyConfiguration(new UsuarioStoreMapping());
            // UsuarioStoreMapping

            base.OnModelCreating(modelBuilder);
        }

    }
}
