using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using WorkFlowDominio.Entities;

namespace WorkFlowData.Mappings
{
    public class ModuloAcaoMapping : IEntityTypeConfiguration<ModuloAcao>
    {
        public void Configure(EntityTypeBuilder<ModuloAcao> builder)
        {
            builder.Ignore(x => x.StateEntityBase);

            builder.ToTable("TbModuloAcao");

            builder.Property(x => x.Acao)
                          .HasColumnName("Acao")
                          .IsRequired()
                        ;

            builder.Property(x => x.Modulo)
                         .HasColumnName("Modulo")
                         .IsRequired()
                       ;
        }


    }
}
