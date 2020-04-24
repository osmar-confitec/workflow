using Specifications.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WorkFlowDominio.Entities;
using WorkFlowViewModel;

namespace WorkFlowDominio.Interfaces
{

    #region " Camadas "


    public interface IClienteRepository : IRepository<Cliente>
    {



    }



    public interface IClienteService : IBaseService<Cliente>
    {

        Task<ClienteIncluirViewModel> IncluirCliente(ClienteIncluirViewModel cliente);

        Task<Cliente> AtualizarCliente(ClienteAlterarViewModel cliente);

        Task DeletarCliente(Guid guid);
    }

    #endregion


    #region " Especificacoes "
    public interface IClienteDadosInserirValidos : ISpecification<ClienteIncluirViewModel>
    {


    }

    public interface IClienteDadosAtualizarValidos : ISpecification<ClienteAlterarViewModel>
    {





    }

    public interface IClienteDeletar : ISpecification<Cliente>
    {



    }

    public interface IClienteInserir : ISpecification<Cliente>
    {

        Task<Cliente> Inserir(ClienteIncluirViewModel clienteIncluirViewModel);

    }

    public interface IClienteAtualizar : ISpecification<Cliente>
    {



    }

    public interface IClienteTemDadosSerasa : ISpecification<Cliente>
    {



    }
    #endregion



}
