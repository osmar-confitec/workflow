using System;
using System.Collections.Generic;
using System.Text;

namespace WorkFlowDominio.Entities
{
    public enum Modulo

    {

        Usuario = 1,
        Permissoes = 2

    }

    public enum Acao
    {

        Incluir = 1,
        Alterar = 2,
        Deletar = 3,
        Consultar = 4

    }

    public class ModuloAcao : EntityBase
    {
        public Modulo Modulo { get; set; }

        public Acao Acao { get; set; }

        public IEnumerable<UsuarioModuloAcao> UsuarioModulosAcoes { get; set; }

        public ModuloAcao()
        {
            UsuarioModulosAcoes = new List<UsuarioModuloAcao>();
        }

    }
}
