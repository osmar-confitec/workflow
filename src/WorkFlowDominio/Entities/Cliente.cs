using System;
using System.Collections.Generic;
using System.Text;

namespace WorkFlowDominio.Entities
{
    public class Cliente: EntityBase
    {

        public string CPF { get; set; }

        public string Nome { get; set; }

        public string SobreNome { get; set; }

        public DateTime DataNascimento { get; set; }

        public int Idade { get; set; }

    }
}
