using System;
using System.Collections.Generic;
using System.Text;

namespace WorkFlowDominio.Entities
{
    public class UsuarioModuloAcao : EntityBase
    {
        public Guid IdUsuario { get; set; }

        public Usuario Usuario { get; set; }

        public Guid IdModuloAcao { get; set; }

        public ModuloAcao ModuloAcao { get; set; }

        public UsuarioModuloAcao()
        {

        }

    }
}
