

using MetodosComunsApi;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WorkFlowDominio.Entities;
using WorkFlowDominio.Interfaces;

using WorkFlowViewModel;

namespace WorkFlowAplicacao.Interfaces
{
    public interface IBaseApp : IDisposable
    {

        Task<bool> CommitarAlteracoes();


        Task<ClassePaginacao<TEntity2>> ObterListaPaginadaViewModel<TEntity1, TEntity2>(
                          IBaseService<TEntity1> baseService,
                          Func<TEntity1, TEntity2> conversao,
                          PaginacaoViewModel paginacaoViewModel) where TEntity2 : class where TEntity1 : EntityBase;


       
    }
}
