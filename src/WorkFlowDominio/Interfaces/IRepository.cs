﻿using MetodosComunsApi;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using WorkFlowDominio.Entities;

namespace WorkFlowDominio.Interfaces
{
    public interface IRepository<TEntity> : IDisposable where TEntity : EntityBase
    {
        Task Adicionar(TEntity entity);
        Task AdicionarNoSave(TEntity entity);
        void AtualizarNoSave(TEntity entity);
        Task<TEntity> ObterPorId(Guid id);
        Task<List<TEntity>> ObterTodos();
        Task Atualizar(TEntity entity);
        Task Remover(Guid id);
        void RemoverNoSave(Guid id);
        Task<IEnumerable<TEntity>> Buscar(Expression<Func<TEntity, bool>> predicate);
        Task<int> SaveChanges();
        Task<ClassePaginacao<TEntity>> ObterListaPaginada(DadosPaginacao<TEntity> dadosPaginacao);
    }
}
