using Dapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkFlowData.Context;
using WorkFlowDominio.Entities;
using WorkFlowDominio.Interfaces;

namespace WorkFlowData.Repository
{
    public class ClienteRepository : RepositoyBase<Cliente>, IClienteRepository
    {
        protected override IQueryable<Cliente> ObterConsultaListaPaginada(Cliente entity)
        {
            throw new NotImplementedException();
        }


        public override async Task<List<Cliente>> ObterTodos()
        {

            var sql = "   SELECT * FROM Clientes  ";
            var ret = await Db.Database.GetDbConnection().QueryAsync<Cliente>(sql);
            return ret.ToList();
        }

        public override async Task<Cliente> ObterPorId(Guid id)
        {
            var sql = @"SELECT * FROM Clientes E " +
                      "WHERE E.Id = @uid";

            var endereco = await Db.Database.GetDbConnection().QueryAsync<Cliente>(sql, new { uid = id });

            return endereco.SingleOrDefault();
        }

        public async Task<Cliente> ObterClientePorCPF(string cpf)
        {
            var sql = @"SELECT * FROM TbClientes E " +
                      "WHERE E.CPF = @uid";

            var clientes = await Db.Database.GetDbConnection().QueryAsync<Cliente>(sql, new { uid = cpf });

            return clientes.SingleOrDefault();
        }

        public ClienteRepository(Contexto db) : base(db)
        {


        }
    }
}
