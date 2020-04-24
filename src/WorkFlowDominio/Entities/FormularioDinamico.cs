using System;
using System.Collections.Generic;
using System.Text;

namespace WorkFlowDominio.Entities
{

    public class FormularioDinamicoColuna : EntityBase
    { 
    
    }

    public class FormularioDinamico : EntityBase
    {

        public IEnumerable<FormularioDinamicoColuna> FormularioDinamicoColunas { get; set; }

    }
}
