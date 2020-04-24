using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using WorkFlowDominio.Entities;

namespace WorkFlowData.Mappings
{
    public class UsuarioMapping : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            //Ignora e define campos de Storevent

            builder.Ignore(x => x.StateEntityBase);

            
  

            builder.ToTable("TbUsuarios");


            builder.Property(x => x.Nome)
                   .HasColumnName("Nome")
                   .HasColumnType("varchar(150)")
                   .IsRequired()
                 ;


            builder.Property(x => x.SobreNome)
                      .HasColumnName("SobreNome")
                      .HasColumnType("varchar(150)")
                      .IsRequired()
                ;



            builder.Property(x => x.Email)
                      .HasColumnName("Email")
                      .HasColumnType("varchar(50)")
                      .IsRequired()
                ;


            builder.Property(x => x.SenhaHash)
                           .HasColumnName("SenhaHash")
                           .IsRequired()
                       ;
            builder.Property(x => x.SenhaSalt)
                       .HasColumnName("SenhaSalt")
                       .IsRequired()
                   ;



            builder.Property(x => x.DataNascimento)
                            .HasColumnName("DataNascimento")
                            .IsRequired()
                             ;

            builder.Property(x => x.CPF)
                       .HasColumnName("CPF")
                       .HasColumnType("varchar(11)")
                       .IsRequired()
            ;


        }
    }
}
