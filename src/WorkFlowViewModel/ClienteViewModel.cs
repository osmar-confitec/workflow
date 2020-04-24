using System;
using System.Collections.Generic;
using System.Text;

namespace WorkFlowViewModel
{

    public class ClienteIncluirViewModel : ClienteBaseViewModel
    {




    }

    public class ClienteAlterarViewModel : ClienteBaseViewModel
    {



    }

    public abstract class ClienteBaseViewModel
    {

        public Guid Id { get; set; }

        public string CPF { get; set; }

        public string Nome { get; set; }

        public string SobreNome { get; set; }

        public DateTime DataNascimento { get; set; }
    }
}
