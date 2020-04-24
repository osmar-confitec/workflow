using Dapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkFlowDominio.Entities;
using WorkFlowDominio.Interfaces;

namespace WorkFlowData.Repository
{
    public class UsuarioRepository : RepositoyBase<Usuario>, IUsuarioRepository
    {
        public UsuarioRepository(WorkFlowData.Context.Contexto db) : base(db)
        {



        }

        public async Task<Usuario> ObterUsuarioCPF(string CPF)
        {
            var sql = @"SELECT * FROM TbUsuarios E " +
                     "WHERE E.CPF = @uid";

            // var usuarios = await Db.Database.GetDbConnection().QueryAsync<Usuario>(sql, new { uid = CPF });
            var usuarios = await Db.Database.GetDbConnection().QueryAsync<Usuario>(sql, new { uid = CPF });

            return usuarios.FirstOrDefault();
        }

        public async Task<Usuario> ObterUsuarioEmail(string Email)
        {
            var sql = @"SELECT * FROM TbUsuarios E " +
                     "WHERE E.Email = @uid";

            var clientes = await Db.Database.GetDbConnection().QueryAsync<Usuario>(sql, new { uid = Email });

            return clientes.SingleOrDefault();
        }

        protected override IQueryable<Usuario> ObterConsultaListaPaginada(Usuario usuario)
        {
                            throw new NotImplementedException();
        }
    }
}
