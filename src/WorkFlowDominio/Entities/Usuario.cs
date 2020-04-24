using System;
using System.Collections.Generic;
using System.Text;

namespace WorkFlowDominio.Entities
{
    public class Usuario : EntityBase
    {

        public string Nome { get; set; }

        public string SobreNome { get; set; }

        public string CPF { get; set; }

        public DateTime DataNascimento { get; set; }

        public IEnumerable<UsuarioModuloAcao> UsuarioModulosAcoes { get; set; }


        public byte[] SenhaHash { get; set; }

        public byte[] SenhaSalt { get; set; }

        public string Email { get; set; }

        

        public Usuario()
        {
            UsuarioModulosAcoes = new List<UsuarioModuloAcao>();

        }

    }
}
