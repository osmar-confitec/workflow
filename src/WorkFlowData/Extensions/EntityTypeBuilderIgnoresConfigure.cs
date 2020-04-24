using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using WorkFlowDominioShared.Events;

namespace WorkFlowData.Extensions
{
   public static  class EntityTypeBuilderIgnoresConfigure
    {

        public static void BuilderStoredEventConfigure<T>(this EntityTypeBuilder<T> builder) where T : StoredEvent
        {

            builder.HasKey(x => x.Id);

            builder.Property<Guid>("Id")
                      .ValueGeneratedOnAdd();

            builder.Property(x => x.Data)
                  .HasColumnName("Data")
                  .HasColumnType("varchar(max)")
                  .IsRequired()
                  ;

            builder.Property(x => x.MessageType)
                   .HasColumnName("MessageType")
                   .HasColumnType("varchar(300)")
                   .IsRequired()
                 ;

            builder.Property(x => x.AggregatedId)
                .HasColumnName("AggregatedId")
                .IsRequired()
              ;

            builder.Property(x => x.StoredEventAction)
                 .HasColumnName("StoredEventAction")
                 .HasColumnType("int")
                 .IsRequired()
               ;

            builder.Property(x => x.Timestamp)
                .HasColumnName("Timestamp")
                .HasColumnType("datetime")
                .IsRequired()
              ;

            builder.Property(x => x.UserId)
            .HasColumnName("User")
            .IsRequired()
        ;
        }

    }
}
