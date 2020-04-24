using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WorkFlowViewModel;

namespace WorkFlowAplicacao.Interfaces
{
    public interface IClienteApp : IBaseApp
    {

        Task<ClienteIncluirViewModel> IncluirCliente(ClienteIncluirViewModel cliente);

        Task<ClienteAlterarViewModel> AlterarCliente(ClienteAlterarViewModel cliente);

        Task DeletarCliente(Guid guid);

    }
}
