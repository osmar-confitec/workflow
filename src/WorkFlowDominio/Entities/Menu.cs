using System;
using System.Collections.Generic;
using System.Text;

namespace WorkFlowDominio.Entities
{
    public class Menu : EntityBase
    {
        public string DescricaoMenu { get; set; }

        public Menu MenuPai { get; set; }

        public Guid? IdMenuPai { get; set; }

        public int Ordem { get; set; }

        public Modulo? Modulo { get; set; }

        public bool Emodulo { get; private set; }

        public Menu()
        {
            Emodulo = Modulo.HasValue;
        }
    }
}
