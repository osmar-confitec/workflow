using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using WorkFlowDominio.Entities.StoreEvents;
using WorkFlowData.Extensions;

namespace WorkFlowData.EventSourcing.Mappings
{
   public class ClienteStoreMapping : IEntityTypeConfiguration<ClienteStore>
    {

        public void Configure(EntityTypeBuilder<ClienteStore> builder)
        {

            /*configuração padrão para stored events*/
            builder.BuilderStoredEventConfigure();
            builder.ToTable("TbClientesStorage");

           
              
        }

    }
}
