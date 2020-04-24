using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using WorkFlowDominio.Entities;

namespace WorkFlowData.Mappings
{
   public class UsuarioModuloAcaoMapping : IEntityTypeConfiguration<UsuarioModuloAcao>
    {
        public void Configure(EntityTypeBuilder<UsuarioModuloAcao> builder)
        {

            builder.Ignore(x => x.StateEntityBase);


            builder.HasKey(x => new { x.Id, x.IdUsuario, x.IdModuloAcao });

            builder.ToTable("TbUsuarioModuloAcao");


            builder.Property(x => x.IdUsuario)
                      .HasColumnName("IdUsuario")
                      .IsRequired()
                ;


            /*many to many*/
            builder.HasOne(x => x.Usuario)
                .WithMany(x => x.UsuarioModulosAcoes)
                .HasForeignKey(f => f.IdUsuario);

            builder.HasOne(x => x.ModuloAcao)
               .WithMany(x => x.UsuarioModulosAcoes)
               .HasForeignKey(f => f.IdModuloAcao);



        }
    }
}
