using MetodosComunsApi;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using WorkFlowDominio.Entities;
using WorkFlowDominio.Interfaces;

namespace WorkFlowData.Repository
{
    public abstract class RepositoyBase<TEntity> : IRepository<TEntity> where TEntity : EntityBase, new()
    {

        protected readonly DbContext Db;
        protected readonly DbSet<TEntity> DbSet; 
        
        
        protected RepositoyBase(DbContext db)
        {
            Db = db;
            DbSet = db.Set<TEntity>();
        }

        protected abstract IQueryable<TEntity> ObterConsultaListaPaginada(TEntity entity);

        public async Task<ClassePaginacao<TEntity>> ObterListaPaginada(DadosPaginacao<TEntity> dadosPaginacao)
        {

            IQueryable<TEntity> query = ObterConsultaListaPaginada(dadosPaginacao.DataPaginacao);

            var total = query.Count();

            var list = await query
                         .OrderByAscDesc(dadosPaginacao.Ordenacao, dadosPaginacao.Direcao)
                         .Skip((dadosPaginacao.Pagina - 1) * dadosPaginacao.QuantidadeRegistrosPagina)
                         .Take(dadosPaginacao.QuantidadeRegistrosPagina).ToListAsync();

            var paginada = new ClassePaginacao<TEntity>(list);
            paginada.TotalRegistros = total;
            return paginada;
        }

        public async Task<IEnumerable<TEntity>> Buscar(Expression<Func<TEntity, bool>> predicate)
        {
            return await DbSet.AsNoTracking().Where(predicate).ToListAsync();
        }

        public virtual async Task<TEntity> ObterPorId(Guid id)
        {
            return await DbSet.FindAsync(id);
        }

        public virtual async Task<List<TEntity>> ObterTodos()
        {
            return await DbSet.AsNoTracking().ToListAsync();
        }

        public virtual async Task Adicionar(TEntity entity)
        {
            DbSet.Add(entity);
            await SaveChanges();
        }

        public virtual async Task Atualizar(TEntity entity)
        {
            DbSet.Update(entity);
            await SaveChanges();
        }

        public virtual async Task Remover(Guid id)
        {
            DbSet.Remove(new TEntity { Id = id });
            await SaveChanges();
        }

        public async Task<int> SaveChanges()
        {
            return await Db.SaveChangesAsync();
        }

        public void Dispose()
        {
            Db?.Dispose();
        }

        public virtual async Task AdicionarNoSave(TEntity entity)
        {
            await DbSet.AddAsync(entity);
        }

        public virtual void RemoverNoSave(Guid id)
        {
            DbSet.Remove(DbSet.Find(id));
        }


        public virtual void AtualizarNoSave(TEntity obj)
        {
            DbSet.Update(obj);
        }
    }
}
