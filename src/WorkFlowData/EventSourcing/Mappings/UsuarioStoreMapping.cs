using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using WorkFlowData.Extensions;
using WorkFlowDominio.Entities.StoreEvents;

namespace WorkFlowData.EventSourcing.Mappings
{
   public class UsuarioStoreMapping : IEntityTypeConfiguration<UsuarioStore>
    {

        public void Configure(EntityTypeBuilder<UsuarioStore> builder)
        {

            /*configuração padrão para stored events*/
            builder.BuilderStoredEventConfigure();
            builder.ToTable("TbUsuariosStorage");



        }

    }
}
