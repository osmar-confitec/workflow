using MetodosComunsApi;
using Resources.Models;
using SlnNotificacoesApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkFlowAplicacao.Interfaces;
using WorkFlowDominio.Entities;
using WorkFlowDominio.Interfaces;
using WorkFlowDominioShared.Interfaces;
using WorkFlowViewModel;

namespace WorkFlowAplicacao.Services
{
    public abstract class BaseApp : IBaseApp
    {

        protected ListNotificacoes<Notificacao> _notificacoes;

        protected IUnitOfWork _unitOfWork;

        protected BaseApp(ListNotificacoes<Notificacao> notificacoes, IUnitOfWork unitOfWork)
        {
            _notificacoes = notificacoes;
            _unitOfWork = unitOfWork;

        }


        public async Task<bool> CommitarAlteracoes()
        {

            if (_notificacoes.Any(x => x is Erro))
                return false;

            if (!await _unitOfWork.CommitAsync())
            {
                _notificacoes.Add(new Erro(Resources.ResourcesManage.ObterResources(new Resources.Models.Resources<string> { ResourceValue = Resources.Enuns.ResourceValueEnum.Falha }).FirstOrDefault()));
                return false;
            }
            return true;
        }

        public void Dispose()
        {

            GC.SuppressFinalize(this);
        }

        public async Task<ClassePaginacao<TEntity2>> ObterListaPaginadaViewModel<TEntity1, TEntity2>(
                IBaseService<TEntity1> baseService,
                Func<TEntity1, TEntity2> conversao,
                PaginacaoViewModel paginacaoViewModel) where TEntity2 : class where TEntity1 : EntityBase
        {

            var dadosPaginacao = new DadosPaginacao<TEntity1>();

            MetodosComunsApi.MetodosComuns.CopyT1T2DiferentClass(paginacaoViewModel, dadosPaginacao.DataPaginacao);
            MetodosComunsApi.MetodosComuns.CopyT1T2DiferentClass(paginacaoViewModel, dadosPaginacao);

            var usuPag = (await baseService.ObterListaPaginada(dadosPaginacao));
            var ListAdd = usuPag.Lista.Select(x => conversao(x));

            var ListRet = new ClassePaginacao<TEntity2>(ListAdd);
            ListRet.TotalRegistros = usuPag.TotalRegistros;
            return ListRet;
        }
    }
}
