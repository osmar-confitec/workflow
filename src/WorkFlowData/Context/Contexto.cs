using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using WorkFlowData.Mappings;
using WorkFlowDominio.Entities;

namespace WorkFlowData.Context
{
   public class Contexto : DbContext
    {

        readonly Microsoft.AspNetCore.Hosting.IWebHostEnvironment _env;

        public DbSet<Cliente> Clientes { get; set; }

        public DbSet<Usuario> Usuarios { get; set; }

        public DbSet<Menu> Menus { get; set; }

        public DbSet<ModuloAcao> ModuloAcoes { get; set; }

        public DbSet<UsuarioModuloAcao> UsuarioModuloAcoes { get; set; }
        //

        public Contexto(Microsoft.AspNetCore.Hosting.IWebHostEnvironment env)/*,
                                           DbContextOptions<Contexto> options) : this(options)*/
        {
            _env = env;

        }

        //public Contexto(DbContextOptions<Contexto> options)
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
            modelBuilder.ApplyConfiguration(new ClienteMapping());
            modelBuilder.ApplyConfiguration(new UsuarioMapping());
            modelBuilder.ApplyConfiguration(new MenuMapping());
            modelBuilder.ApplyConfiguration(new ModuloAcaoMapping());
            modelBuilder.ApplyConfiguration(new UsuarioModuloAcaoMapping());

            base.OnModelCreating(modelBuilder);
        }

    }
}
