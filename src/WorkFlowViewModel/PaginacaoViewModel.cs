using System;
using System.Collections.Generic;
using System.Text;

namespace WorkFlowViewModel
{
    public abstract class PaginacaoViewModel
    {
        public int Pagina { get; set; }
        public string Ordenacao { get; set; }
        public bool Direcao { get; set; }
        public int QuantidadeRegistrosPagina { get; set; }
    }
}
