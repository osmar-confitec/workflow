using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using SlnNotificacoesApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WorkFlowApi.Controllers
{
    [ApiController]
    public abstract class BaseController : ControllerBase
    {

        protected ListNotificacoes<Notificacao> _notificacoes;

        protected BaseController(ListNotificacoes<Notificacao> notificacoes)
        {
            _notificacoes = notificacoes;


            if (_notificacoes == null)
                _notificacoes = new ListNotificacoes<Notificacao>();
        }


        public IReadOnlyCollection<Notificacao> notificacaos { get { return _notificacoes.AsReadOnly(); } }


        protected void AdicionarNotificacao(string msg)
        {
            _notificacoes.Add(new Erro(SlnNotificacoesApi.Enum.CriticidadeEnum.Media, null, msg, null, null));
        }

        protected void AdicionarNotificacao(Resources.Models.Resources<string> resources)
        {
            _notificacoes.Add(new Erro(resources));
        }


        protected ActionResult CustomResponse(ModelStateDictionary modelState)
        {
            if (!modelState.IsValid) NotificarErroModelInvalida(modelState);
            return CustomResponse();
        }

        protected void NotificarErroModelInvalida(ModelStateDictionary modelState)
        {
            var erros = modelState.Values.SelectMany(e => e.Errors);
            foreach (var erro in erros)
            {
                var errorMsg = erro.Exception == null ? erro.ErrorMessage : erro.Exception.Message;
                AdicionarNotificacao(errorMsg);
            }
        }


        protected ActionResult CustomResponse(Exception exception)
        {

            return BadRequest(new
            {
                success = false,
                errors = new List<Notificacao> { new Erro(new Resources.Models.Resources<string> { Mensagem = exception.Message }) }
            });
        }

        protected ActionResult CustomResponse(Func<ActionResult> acao)
        {
            try
            {
                return acao?.Invoke();
            }
            catch (Exception ex)
            {
                return CustomResponse(ex);
            }

        }

        protected async Task<ActionResult> CustomResponse(Func<Task<ActionResult>> acao)
        {
            try
            {
                return await acao?.Invoke();
            }
            catch (Exception ex)
            {
                return CustomResponse(ex);
            }

        }

        protected ActionResult CustomResponse(object result = null)
        {

            if (_notificacoes == null || (_notificacoes != null && !_notificacoes.Any(x => x is Erro || x is Alerta)))
                return Ok(new
                {
                    success = true,
                    data = result
                });


            return BadRequest(new
            {
                success = false,
                errors = notificacaos
            });
        }



    }


}
