using AutoMapper;
using SlnNotificacoesApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkFlowAplicacao.Interfaces;
using WorkFlowData.EventSourcing.Context;
using WorkFlowDominio.Entities;
using WorkFlowDominio.Entities.StoreEvents;
using WorkFlowDominio.Events;
using WorkFlowDominio.Interfaces;
using WorkFlowDominioShared.Bus;
using WorkFlowDominioShared.Interfaces;
using WorkFlowDominioShared.Notifications;
using WorkFlowIdentity.Models;
using WorkFlowViewModel;

namespace WorkFlowAplicacao.Services
{
    public class ClienteApp : BaseApp, IClienteApp
    {
        readonly IClienteService _clienteService;
        readonly IMediatorHandler _bus;
        readonly IMapper _mapper;
        readonly EventStoreContexto _eventStoreContexto;

        readonly IUser _user;


        public ClienteApp(
                            IMediatorHandler bus,
                            IClienteService clienteService,
                            ListNotificacoes<Notificacao> notificacoes,
                            EventStoreContexto eventStoreContexto,
                            IUser user,
                            IMapper mapper,
                            IUnitOfWork unitOfWork)
            : base(notificacoes, unitOfWork)
        {
           _clienteService = clienteService;
            _bus = bus;
           _user = user;
           _mapper = mapper;
            _eventStoreContexto = eventStoreContexto;

        }

        public Task<ClienteAlterarViewModel> AlterarCliente(ClienteAlterarViewModel cliente)
        {
            throw new NotImplementedException();
        }

        public Task DeletarCliente(Guid guid)
        {
            throw new NotImplementedException();
        }

        public async Task<ClienteIncluirViewModel> IncluirCliente(ClienteIncluirViewModel cliente)
        {
            var ret = await _clienteService.IncluirCliente(cliente);
            if (_notificacoes.Any())
                return ret;

            if (await _unitOfWork.CommitAsync())
            {
                /*envio do evento de cliente inserido*/
                await _bus.RaiseEvent(new ClienteInseridoEvent());

                /*envio para o store após estar tudo commitado*/
                var clienteret = _mapper.Map<Cliente>(ret);
              

                /*store de notification*/
                await _bus.RaiseEvent(new EventStoreNotifications(storedEvent:
                                                                  new ClienteStore(
                                                                        cliente: clienteret,
                                                                        _storedEventAction: WorkFlowDominioShared.Events.StoredEventActionEnum.Insert,
                                                                        userId: _user.GetUserId())));
                                                                        

            }

            return ret;
        }
    }
}
