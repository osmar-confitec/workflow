using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SlnNotificacoesApi;
using WorkFlowApi.Controllers;
using WorkFlowAplicacao.Interfaces;
using WorkFlowViewModel;

namespace WorkFlowApi.V1.Controllers
{

    [Authorize]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/cliente")]
    public class ClienteController : BaseController
    {

        readonly IClienteApp _clienteapp;

        public ClienteController(IClienteApp clienteapp,ListNotificacoes<Notificacao> notificacoes) : base(notificacoes)
        {
            _clienteapp = clienteapp;

        }

        [HttpPost]
        [Route("incluircliente")]
        [AllowAnonymous]
        public async Task<ActionResult<ClienteIncluirViewModel>> IncluirCliente(ClienteIncluirViewModel clienteIncluirViewModel)
        {

            try
            {
                if (!ModelState.IsValid) return CustomResponse(ModelState);
                await _clienteapp.IncluirCliente(clienteIncluirViewModel);
                return CustomResponse(clienteIncluirViewModel);
            }
            catch (Exception ex)
            {
                return CustomResponse(ex);
            }

        }


        [HttpPut]
        [Route("alterarcliente")]
        [AllowAnonymous]
        public async Task<ActionResult<ClienteAlterarViewModel>> AlterarCliente(ClienteAlterarViewModel clienteIncluirViewModel)
        {
            try
            {
                if (!ModelState.IsValid) return CustomResponse(ModelState);
                await _clienteapp.AlterarCliente(clienteIncluirViewModel);
                return CustomResponse(clienteIncluirViewModel);
            }
            catch (Exception ex)
            {
                return CustomResponse(ex);
            }
        }


        [HttpDelete]
        [Route("deletarcliente/{clienteid:guid}")]
        [AllowAnonymous]
        public async Task<ActionResult> DeletarCliente(Guid guid)
        {
            try
            {
                if (!ModelState.IsValid) return CustomResponse(ModelState);
                await _clienteapp.DeletarCliente(guid);
                return CustomResponse(new { });
            }
            catch (Exception ex)
            {
                return CustomResponse(ex);
            }
        }

    }
}