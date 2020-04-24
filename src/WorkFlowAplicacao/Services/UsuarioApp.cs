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
using WorkFlowDominio.Interfaces;
using WorkFlowDominioShared.Bus;
using WorkFlowDominioShared.Interfaces;
using WorkFlowDominioShared.Notifications;
using WorkFlowIdentity.Models;

namespace WorkFlowAplicacao.Services
{
    public class UsuarioApp : BaseApp, IUsuarioApp
    {

        readonly IUsuarioService _usuarioService;
        readonly IUsuarioRepository _usuarioRepository;
        readonly IMediatorHandler _bus;
        readonly IMapper _mapper;
        readonly IAuthDeletarUsuario _authDeletarUsuario;
        readonly EventStoreContexto _eventStoreContexto;

        readonly IUser _user;


        public UsuarioApp(IMediatorHandler bus,
                            IUsuarioService usuarioService,
                            IUsuarioRepository usuarioRepository,
                            IAuthDeletarUsuario authDeletarUsuario,
                            ListNotificacoes<Notificacao> notificacoes,
                            EventStoreContexto eventStoreContexto,
                            IUser user,
                            IMapper mapper,
                            IUnitOfWork unitOfWork)
            : base(notificacoes, unitOfWork)
        {
            _usuarioService = usuarioService;
            _bus = bus;
            _user = user;
            _usuarioRepository = usuarioRepository;
            _mapper = mapper;
            _authDeletarUsuario = authDeletarUsuario;
            _eventStoreContexto = eventStoreContexto;
        }

        public async Task DeletarUsuario(Guid guid)
        {
            await _authDeletarUsuario.DeletarUsuario(guid);

            if (_notificacoes.Any())
                return;
            if (await _unitOfWork.CommitAsync())
            {

               var userAtualizado  = await   _usuarioRepository.ObterPorId(guid);

                /*store de notification*/
                await _bus.RaiseEvent(new EventStoreNotifications(storedEvent:
                                                                  new UsuarioStore(
                                                                        usuario: userAtualizado,
                                                                        _storedEventAction: WorkFlowDominioShared.Events.StoredEventActionEnum.Delete,
                                                                        userId: _user.GetUserId())));

            }
        }

        public async Task<RegisterUserViewModel> IncluirUsuario(RegisterUserViewModel usuario)
        {
            var ret = await _usuarioService.IncluirUsuario(usuario);
            if (_notificacoes.Any())
                return ret;

            if (await _unitOfWork.CommitAsync())
            {
                /*envio para o store após estar tudo commitado*/
                var usuarioret = _mapper.Map<Usuario>(ret);


                /*store de notification*/
                await _bus.RaiseEvent(new EventStoreNotifications(storedEvent:
                                                                  new UsuarioStore(
                                                                        usuario: usuarioret,
                                                                        _storedEventAction: WorkFlowDominioShared.Events.StoredEventActionEnum.Insert,
                                                                        userId: _user.GetUserId())));
            }
            return ret;
        }
    }
}
