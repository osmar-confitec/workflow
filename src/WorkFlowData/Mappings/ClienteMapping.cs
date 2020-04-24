using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using WorkFlowDominio.Entities;

namespace WorkFlowData.Mappings
{
   public class ClienteMapping : IEntityTypeConfiguration<Cliente>
    {

        public void Configure(EntityTypeBuilder<Cliente> builder)
        {


            builder.Ignore(x => x.StateEntityBase);

            builder.ToTable("TbClientes");

            builder.Property(x => x.Nome)
                    .HasColumnName("Nome")
                    .HasColumnType("varchar(200)")
                    .IsRequired()
                    ;

            builder.Property(x => x.Nome)
                    .HasColumnName("SobreNome")
                    .HasColumnType("varchar(200)")
                    .IsRequired()
                    ;

            builder.Property(x => x.DataNascimento)
                 .HasColumnName("DataNascimento")
                 .HasColumnType("datetime")
                 .IsRequired()
                 ;


            builder.Property(x => x.Idade)
              .HasColumnName("Idade")
              .HasColumnType("int")
              .IsRequired()
              ;
        }

    }

}
