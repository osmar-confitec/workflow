using MetodosComunsApi;

using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using WorkFlowDominio.Entities;
using WorkFlowDominio.Interfaces;

namespace WorkFlowDominio.Services
{
    public abstract class ServicoBase<TEntity> : IBaseService<TEntity> where TEntity : EntityBase
    {
        protected readonly IRepository<TEntity> repositorio;

        public ServicoBase(IRepository<TEntity> iRepositorio)
        {

            repositorio = iRepositorio;

        }

        public async Task Adicionar(TEntity entity)
        {
            await repositorio.Adicionar(entity);
        }

        public async Task AdicionarNoSave(TEntity entity)
        {
            await repositorio.AdicionarNoSave(entity);
        }

        public async Task Atualizar(TEntity entity)
        {
            await repositorio.Atualizar(entity);
        }

        public void AtualizarNoSave(TEntity entity)
        {
            repositorio.AtualizarNoSave(entity);
        }

        public async Task<IEnumerable<TEntity>> Buscar(Expression<Func<TEntity, bool>> predicate)
        {
            return await repositorio.Buscar(predicate);
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public async Task<ClassePaginacao<TEntity>> ObterListaPaginada<TEntity2>(DadosPaginacao<TEntity2> dadosPaginacao) where TEntity2 : class
        {
            var dadosPaginar = new DadosPaginacao<TEntity>();
            MetodosComuns.CopyT1T2DiferentClass(dadosPaginacao, dadosPaginar);

            if (dadosPaginar.Pagina <= 0) dadosPaginar.Pagina = 1;

            if (string.IsNullOrEmpty(dadosPaginar.Ordenacao))
            {
                dadosPaginar.Ordenacao = dadosPaginar.DataPaginacao.GetType().GetProperties()[0].Name;
            }


            var qtdreg = MetodosComuns.ObterJson("qtdRegistrosPorPagina", "appsettings.json").Value;

            if (dadosPaginar.QuantidadeRegistrosPagina <= 0)
                dadosPaginar.QuantidadeRegistrosPagina = Convert.ToInt32(qtdreg);

            /**/

            return await repositorio.ObterListaPaginada(dadosPaginar);
        }

        public async Task<TEntity> ObterPorId(Guid id)
        {
            return await repositorio.ObterPorId(id);
        }

        public async Task<List<TEntity>> ObterTodos()
        {
            return await repositorio.ObterTodos();
        }

        public async Task Remover(Guid id)
        {
            await repositorio.Remover(id);
        }

        public void RemoverNoSave(Guid id)
        {
            repositorio.RemoverNoSave(id);
        }

        public async Task<int> SaveChanges()
        {
            return await repositorio.SaveChanges();
        }

    }
}
