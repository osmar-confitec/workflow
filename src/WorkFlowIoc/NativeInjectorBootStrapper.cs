

using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Resources;
using SlnNotificacoesApi;
using WorkFlowAplicacao.Interfaces;
using WorkFlowAplicacao.Services;
using WorkFlowBus;
using WorkFlowData.Context;
using WorkFlowData.EventSourcing;
using WorkFlowData.EventSourcing.Context;
using WorkFlowData.Repository;
using WorkFlowData.UoW;
using WorkFlowDominio.EventHandlers;
using WorkFlowDominio.Events;
using WorkFlowDominio.Interfaces;
using WorkFlowDominio.Services;
using WorkFlowDominio.Specifications;
using WorkFlowDominioShared.Bus;
using WorkFlowDominioShared.Events;
using WorkFlowDominioShared.Interfaces;
using WorkFlowDominioShared.Notifications;
using WorkFlowIdentity.Authorization;
using WorkFlowIdentity.Models;

namespace WorkFlowIoc
{
    public class NativeInjectorBootStrapper
    {
        public static void RegisterServices(IServiceCollection services)
        {
            // ASP.NET HttpContext dependency
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            // Domain Bus (Mediator)
            services.AddScoped<IMediatorHandler, InMemoryBusWorkFlow>();

            // ASP.NET Authorization Polices
            services.AddSingleton<IAuthorizationHandler, ClaimsRequirementHandler>();

            services.AddScoped<ListNotificacoes<Notificacao>>();



            // Application
      
            services.AddScoped<IUsuarioApp, UsuarioApp>();

            //Services
            services.AddScoped<IClienteService, ClienteService>();
            services.AddScoped<IUsuarioService, UsuarioService>();


            // Domain - Events
            services.AddScoped<INotificationHandler<EventStoreNotifications>, EventStoreNotificationsHandler>();
            services.AddScoped<IEventStoreSave, EventStoreSql>();
            //EventStoreSql : IEventStoreSave

            RegisterServicesCliente(services);


            // Infra - Data
            services.AddScoped<IUnitOfWork, UnitOfWorkData>();
            services.AddScoped<Contexto>();


            // Clientes 
            services.AddScoped<IClienteRepository, ClienteRepository>();


            //Usuario
            services.AddScoped<IUsuarioRepository, UsuarioRepository>();


            //Specification

            //Cliente
            services.AddScoped<IClienteDadosInserirValidos, ClienteDadosInserirValidos>();
            services.AddScoped<IClienteDadosAtualizarValidos, ClienteDadosAtualizarValidos>();
            services.AddScoped<IClienteDeletar, ClienteDeletar>();
            services.AddScoped<IClienteInserir, ClienteInserir>();
            services.AddScoped<IClienteAtualizar, ClienteAtualizar>();
            services.AddScoped<IClienteTemDadosSerasa, ClienteTemDadosSerasa>();

            //Usuario
            services.AddScoped<IAuthInserirUsuarioDadosValidos, AuthInserirUsuarioDadosValidos>();
            services.AddScoped<IAuthInserirUsuario, AuthInserirUsuario>();
            services.AddScoped<IAuthDeletarUsuario, AuthDeletarUsuario>();
            services.AddScoped<IAuthEntrar, AuthEntrar>();
            //IAuthEntrar


            // Infra - Data EventSourcing
            services.AddScoped<EventStoreContexto>();

            /*
             */
            // 
            // Infra - Identity Services
            services.AddTransient<IEmailSender, AuthEmailMessageSender>();
            services.AddTransient<ISmsSender, AuthSMSMessageSender>();


            // Infra - Identity
            services.AddScoped<IUser, AspNetUser>();
            services.AddSingleton<ResourcesManageMemory>();

            //ResourcesManageMemory

        }

         static void RegisterServicesCliente(IServiceCollection services)
        {

            services.AddScoped<IClienteApp, ClienteApp>();

            // Cliente
            services.AddScoped<INotificationHandler<ClienteAlteradoEvent>, ClienteEventHandler>();
            services.AddScoped<INotificationHandler<ClienteInseridoEvent>, ClienteEventHandler>();
            services.AddScoped<INotificationHandler<ClienteRemovidoEvent>, ClienteEventHandler>();
        }
    }
}
