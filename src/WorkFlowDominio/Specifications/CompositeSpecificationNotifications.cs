using SlnNotificacoesApi;
using Specifications.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkFlowDominio.Entities;
using WorkFlowIdentity.Models;

namespace WorkFlowDominio.Specifications
{
    public abstract class CompositeSpecificationNotifications<T> : CompositeSpecification<T>
    {
        protected readonly IUser _user;

        protected ListNotificacoes<Notificacao> _notificacoes = new ListNotificacoes<Notificacao>();

        protected virtual void AtualizarDadosEntitty(EntityBase model)
        {
            switch (model.StateEntityBase)
            {
                case StateEntityBaseEnum.Inclusao:
                    model.DataCadastro = DateTime.Now;
                    model.IdUsuarioCadastro = _user.GetUserId() == Guid.Empty ? Guid.NewGuid() : _user.GetUserId();
                    model.Ativo = true;
                    break;
                case StateEntityBaseEnum.Alteracao:
                    model.DataAtualizacao = DateTime.Now;
                    model.Ativo = true;
                    model.IdUsuarioAlteracao = _user.GetUserId() == Guid.Empty ? Guid.NewGuid() : _user.GetUserId();
                    break;
                case StateEntityBaseEnum.Delecao:
                    model.DataDelecao = DateTime.Now;
                    model.IdUsuarioDelecao = _user.GetUserId() == Guid.Empty ? Guid.NewGuid() : _user.GetUserId();
                    model.Ativo = false;
                    break;
                default:
                    break;
            }
        }

        public override void AdicionarNotificacao(Notificacao erro)
        {
            
            _notificacoes.Add(erro);
        }

        public override void AdicionarNotificacoes(IReadOnlyList<Notificacao> notificacaos)
        {
            _notificacoes.AddRange(notificacaos);
        }

        protected CompositeSpecificationNotifications(IUser user, ListNotificacoes<Notificacao> notificacoes) : base()
        {
            _notificacoes = notificacoes;
            _user = user;
        }

        public override async Task<bool> IsSatisfiedBy(T candidate)
        {

            var Satisfied = await base.IsSatisfiedBy(candidate);

            if (Notificacoes.Any())
            {
                _notificacoes.AddRange(Notificacoes);
                return Satisfied;
            }

            if (candidate is EntityBase)
                AtualizarDadosEntitty(candidate as EntityBase);

            return Satisfied;
        }

    }
}
