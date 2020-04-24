using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using WorkFlowDominio.Entities;

namespace WorkFlowData.Mappings
{
    public class MenuMapping : IEntityTypeConfiguration<Menu>
    {
        public void Configure(EntityTypeBuilder<Menu> builder)
        {
            builder.Ignore(x => x.StateEntityBase);

            builder.ToTable("TbMenu");

            builder.Property(x => x.DescricaoMenu)
                          .HasColumnName("DescricaoMenu")
                          .HasColumnType("varchar(200)")
                          .IsRequired()
                        ;

            builder.Property(x => x.Emodulo)
                         .HasColumnName("Emodulo")
                         .IsRequired()
                       ;

            builder.Property(x => x.Ordem)
                        .HasColumnName("Ordem")
                        .IsRequired()
                      ;

            builder.Property(x => x.IdMenuPai)
                       .HasColumnName("IdMenuPai")
                       .IsRequired(false)
                     ;

            builder.Property(x => x.Modulo)
                       .HasColumnName("Modulo")
                     ;

            builder.HasOne(x => x.MenuPai)
                   .WithOne()
                   .IsRequired(false)
                   .HasForeignKey<Menu>(x => x.IdMenuPai);

        }


    }
}
