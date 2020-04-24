using System;
using System.Collections.Generic;
using System.Text;

namespace WorkFlowDominio.Entities
{
  public  class Fornecedor : EntityBase
    {

        public string CNPJ { get; set; }

        public string NomeFantasia { get; set; }

        public string RazaoSocial { get; set; }

    }
}
