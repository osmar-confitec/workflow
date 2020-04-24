using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WorkFlowIdentity.Models;

namespace WorkFlowAplicacao.Interfaces
{
    public interface IUsuarioApp : IBaseApp
    {
        Task<RegisterUserViewModel> IncluirUsuario(RegisterUserViewModel usuario);

        Task DeletarUsuario(Guid guid);
    }
}
