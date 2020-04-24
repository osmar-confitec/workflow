using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WorkFlowDominio.Entities;
using WorkFlowDominio.Interfaces;
using WorkFlowIdentity.Models;

namespace WorkFlowDominio.Services
{
    public class UsuarioService : ServicoBase<Usuario>, IUsuarioService
    {

        readonly IUsuarioRepository _usuarioRepository;
        readonly IAuthInserirUsuario _usuarioInserir;
        readonly IAuthDeletarUsuario _usuarioDeletar;

        public UsuarioService(IAuthDeletarUsuario usuarioDeletar, IAuthInserirUsuario usuarioInserir, IUsuarioRepository usuarioRepository) 
            : base(usuarioRepository)
        {

            _usuarioRepository = usuarioRepository;
            _usuarioInserir = usuarioInserir;
            _usuarioDeletar = usuarioDeletar;

        }

        public async Task DeletarUsuario(Guid guid)
        {
            await _usuarioDeletar.DeletarUsuario(guid);
        }

        public async Task<RegisterUserViewModel> IncluirUsuario(RegisterUserViewModel usuario)
        {
            return await _usuarioInserir.InserirUsuario(usuario);
        }
    }
}
