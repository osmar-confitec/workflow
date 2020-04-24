using MetodosComunsApi;
using Specifications.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WorkFlowDominio.Entities;
using WorkFlowIdentity.Models;

namespace WorkFlowDominio.Interfaces
{

    #region "  Camadas "   


    public interface IUsuarioRepository : IRepository<Usuario>
    {
        Task<Usuario> ObterUsuarioCPF(string CPF);
        Task<Usuario> ObterUsuarioEmail(string Email);


    }


    public interface IUsuarioService : IBaseService<Usuario>
    {

        Task<RegisterUserViewModel> IncluirUsuario(RegisterUserViewModel usuario);

        Task DeletarUsuario(Guid guid);
    }


    #endregion 


    #region " Especificações "


    public interface IAuthDeletarUsuario : ISpecification<Usuario>
    {
        Task DeletarUsuario(Guid UsuarioId);

    }


    


    public interface IAuthInserirUsuarioDadosValidos : ISpecification<RegisterUserViewModel>
    {



    }

    public interface IAuthInserirUsuario : ISpecification<Usuario>
    {
        Task<RegisterUserViewModel> InserirUsuario(RegisterUserViewModel registerUserViewModel);
    }

    public interface IAuthEntrarDadosValidos : ISpecification<LoginUserViewModel>
    {


    }

    public interface IAuthEntrar : ISpecification<LoginUserViewModel>{}

    #endregion

}
