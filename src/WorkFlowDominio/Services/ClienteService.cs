using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WorkFlowDominio.Entities;
using WorkFlowDominio.Interfaces;
using WorkFlowViewModel;

namespace WorkFlowDominio.Services
{
    public class ClienteService : ServicoBase<Cliente>, IClienteService
    {

        readonly IClienteRepository _clienteRepository;
        readonly IClienteInserir _clienteInserir;

        public ClienteService(IClienteInserir clienteInserir, IClienteRepository clienteRepository) 
                        : base(clienteRepository)
        {
            _clienteRepository = clienteRepository;
            _clienteInserir = clienteInserir;
        }



        public Task<Cliente> AtualizarCliente(ClienteAlterarViewModel cliente)
        {
            throw new NotImplementedException();
        }

        public Task DeletarCliente(Guid guid)
        {
            throw new NotImplementedException();
        }

        public async Task<ClienteIncluirViewModel> IncluirCliente(ClienteIncluirViewModel cliente)
        {
           var clienteret =  await _clienteInserir.Inserir(cliente);
            cliente.Id = new Guid(clienteret.Id.ToString());
            return cliente;
        }
    }
}
