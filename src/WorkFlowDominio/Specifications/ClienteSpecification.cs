using AutoMapper;
using FluentValidation;
using Resources;
using Resources.Models;
using Resources.Util;
using SlnNotificacoesApi;
using Specifications.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WorkFlowDominio.Entities;
using WorkFlowDominio.Events;
using WorkFlowDominio.Interfaces;
using WorkFlowDominioShared.Bus;
using WorkFlowIdentity.Models;
using WorkFlowViewModel;

namespace WorkFlowDominio.Specifications
{
    public class ClienteDadosInserirValidos : CompositeSpecificationNotifications<ClienteIncluirViewModel>, IClienteDadosInserirValidos
    {

        readonly ResourcesManageMemory _resourcesManageMemory;

        public ClienteDadosInserirValidos(IUser user, ListNotificacoes<Notificacao> notificacoes,
                                                      ResourcesManageMemory resourcesManageMemory)
            : base(user, notificacoes)
        {

            _resourcesManageMemory = resourcesManageMemory;
            AdicionarListaResources(_resourcesManageMemory.ObterResources(null, new List<Resources.Models.Resources<string>>() {
                new Resources<string>{
                    ResourceValue = Resources.Enuns.ResourceValueEnum.CPFInvalido
                },
                new Resources<string>{
                    ResourceValue = Resources.Enuns.ResourceValueEnum.DescricaoNomeRequerido
                },
                new Resources<string>{
                    ResourceValue = Resources.Enuns.ResourceValueEnum.SobreNomeRequerido
                },
                 new Resources<string>{
                    ResourceValue = Resources.Enuns.ResourceValueEnum.Maiorde12
                }
              }));

            RuleFor(x => x.CPF).Custom(FluentValidationCommons.ECpf(Res.ObterResourceMessageClass(null, Resources.Enuns.ResourceValueEnum.CPFInvalido)));

            RuleFor(x => x.Nome).Custom(FluentValidationCommons.Nome(Res.ObterResourceMessageClass(null, Resources.Enuns.ResourceValueEnum.DescricaoNomeRequerido)));

            RuleFor(x => x.SobreNome).Custom(FluentValidationCommons.SobreNome(Res.ObterResourceMessageClass(null, Resources.Enuns.ResourceValueEnum.SobreNomeRequerido)));

            RuleFor(x => x.DataNascimento).Custom(FluentValidationCommons.Maior12(Res.ObterResourceMessageClass(null, Resources.Enuns.ResourceValueEnum.Maiorde12)));
        }

        public override IEnumerable<Resources<string>> ObterRes() => null;
    }

    public class ClienteDadosAtualizarValidos : CompositeSpecificationNotifications<ClienteAlterarViewModel>, IClienteDadosAtualizarValidos
    {
        public ClienteDadosAtualizarValidos(IUser user, ListNotificacoes<Notificacao> notificacoes) : base(user, notificacoes)
        {



        }

        public override IEnumerable<Resources<string>> ObterRes() => null;
    }

    public class ClienteDeletar : CompositeSpecificationNotifications<Cliente>, IClienteDeletar
    {
        public ClienteDeletar(IUser user, ListNotificacoes<Notificacao> notificacoes) : base(user, notificacoes)
        {



        }

        public override Task<bool> IsSatisfiedBy(Cliente candidate)
        {
            return base.IsSatisfiedBy(candidate);
        }

        public override IEnumerable<Resources<string>> ObterRes() => null;
    }


    public class ClienteInserir : CompositeSpecificationNotifications<Cliente>, IClienteInserir
    {

        readonly IClienteDadosInserirValidos _clienteDadosInserirValidos;
        readonly IClienteRepository _clienteRepository;
        readonly IMapper _mapper;
        readonly IClienteTemDadosSerasa _clienteTemDadosSerasa;


        public ClienteInserir(IClienteTemDadosSerasa clienteTemDadosSerasa,
            IClienteRepository clienteRepository,
            IMapper mapper,
            IUser user,
            ListNotificacoes<Notificacao> notificacoes,
            IClienteDadosInserirValidos clienteDadosInserirValidos)
            : base(user, notificacoes)
        {
            _mapper = mapper;
            _clienteRepository = clienteRepository;
            _clienteTemDadosSerasa = clienteTemDadosSerasa;
            _clienteDadosInserirValidos = clienteDadosInserirValidos;

        }


        public override async Task<bool> IsSatisfiedBy(Cliente candidate)
        {
            //
            var dadosSerasa = Not(_clienteTemDadosSerasa);

            //verificação das regras de negócio de dados serasa
            var resultSerasa = await dadosSerasa.IsSatisfiedBy(candidate);

            /*somatoria das regras desse módulo mais associados*/
            var Satisfied = await base.IsSatisfiedBy(candidate)
                                                             && resultSerasa;
            //não satisfez retorna 
            if (!Satisfied)
                return Satisfied;

            await _clienteRepository.AdicionarNoSave(candidate);
            return true;

        }
        public override IEnumerable<Resources<string>> ObterRes() => null;

        public async Task<Cliente> Inserir(ClienteIncluirViewModel clienteIncluirViewModel)
        {
            if (!await _clienteDadosInserirValidos.IsSatisfiedBy(clienteIncluirViewModel))
            {
                return null;
            }
            var cliente = _mapper.Map<Cliente>(clienteIncluirViewModel);
            if (await IsSatisfiedBy(cliente))
                return cliente;
            return null;
        }
    }

    public class ClienteAtualizar : CompositeSpecificationNotifications<Cliente>, IClienteAtualizar
    {
        public ClienteAtualizar(IUser user, ListNotificacoes<Notificacao> notificacoes) : base(user, notificacoes)
        {



        }

        public override Task<bool> IsSatisfiedBy(Cliente candidate)
        {
            return base.IsSatisfiedBy(candidate);
        }

        public override IEnumerable<Resources<string>> ObterRes() => null;
    }

    public class ClienteTemDadosSerasa : CompositeSpecificationNotifications<Cliente>, IClienteTemDadosSerasa
    {
        public ClienteTemDadosSerasa(IUser user, ListNotificacoes<Notificacao> notificacoes)

            : base(user, notificacoes)
        {


        }

        public override async Task<bool> IsSatisfiedBy(Cliente candidate)
        {
            var Satisfied = await base.IsSatisfiedBy(candidate);

            if (!Satisfied)
                return Satisfied;


            return candidate == null;
        }

        public override IEnumerable<Resources<string>> ObterRes() => null;

    }

}
