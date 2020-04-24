using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Resources;
using SlnNotificacoesApi;
using WorkFlowApi.Controllers;
using WorkFlowApi.Models;
using WorkFlowAplicacao.Interfaces;
using WorkFlowDominio.Interfaces;
using WorkFlowIdentity.Models;

namespace WorkFlowApi.V1.Controllers
{
    [Authorize]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/auth")]
    public class AuthController : BaseController
    {

        /*
      readonly SignInManager<ApplicationUser> _signInManager;
      readonly UserManager<ApplicationUser> _userManager;
      */

        readonly AppSettings _appSettings;
        readonly SignInManager<ApplicationUser> _signInManager;
        readonly UserManager<ApplicationUser> _userManager;
        readonly ResourcesManageMemory _resourcesManageMemory;
        readonly IUsuarioApp _iusuarioApp;
        readonly IAuthEntrar _authEntrar;

        public AuthController(ListNotificacoes<Notificacao> notificacoes,
                              ResourcesManageMemory resourcesManageMemory,
                              IUsuarioApp iusuarioApp,
                              IAuthEntrar authEntrar,
                              SignInManager<ApplicationUser> signInManager,
                              UserManager<ApplicationUser> userManager,
                             IOptions<AppSettings> appSettings
            ) : base(notificacoes)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _authEntrar = authEntrar;
            _appSettings = appSettings.Value;
            _resourcesManageMemory = resourcesManageMemory;
            _iusuarioApp = iusuarioApp;

        }

        /*

        public AuthController(SignInManager<ApplicationUser> signInManager,
                             UserManager<ApplicationUser> userManager,
                             IOptions<AppSettings> appSettings)
        {

            _signInManager = signInManager;
            _userManager = userManager;
            _appSettings = appSettings.Value;

        }

            */


        [HttpPost]
        [Route("logout")]
       // [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            try
            {
                await _signInManager.SignOutAsync();
                return CustomResponse(new { });
            }
            catch (Exception ex)
            {
                return CustomResponse(ex);
                
            }
            
        }


        [HttpPost]
        [Route("nova-conta")]
        //[AllowAnonymous]
       // [ValidateAntiForgeryToken]
        public async Task<IActionResult> Registrar(RegisterUserViewModel registerUser)
        {
            try
            {
                if (!ModelState.IsValid) return CustomResponse(ModelState);
                //ApplicationUserWidoutEntity
                return await NovaConta(registerUser);
            }
            catch (Exception ex)
            {
                return CustomResponse(ex);
            }
        }

        [HttpDelete]
        [Route("deletar/{usuarioId:guid}")]
        public async Task<IActionResult> UsuarioDelete(Guid UsuarioId)
        {
            try
            {
                await _iusuarioApp.DeletarUsuario(UsuarioId);
                return CustomResponse(UsuarioId);
            }
            catch (Exception ex)
            {
                return CustomResponse(ex);
            }
        }

        [HttpPost]
        [Route("nova-conta-login")]
        //[AllowAnonymous]
       // [ValidateAntiForgeryToken]
        public async Task<IActionResult> RegistrarLogin(RegisterUserViewModel registerUser)
        {

            try
            {
                if (!ModelState.IsValid) return CustomResponse(ModelState);
                //ApplicationUserWidoutEntity
                return await NovaConta(registerUser, true);
            }
            catch (Exception ex)
            {
                return CustomResponse(ex);
            }

           
        }

        async Task<IActionResult> NovaConta(RegisterUserViewModel registerUser,
                                bool authenticar = false)
        {

            /*
            var user = new ApplicationUserWidoutEntity
            {
                UserName = registerUser.Email,
                Email = registerUser.Email,
                EmailConfirmed = true
            };
            */

            var user = new ApplicationUser
            {
                UserName = registerUser.Email,
                Email = registerUser.Email,
                EmailConfirmed = true
            };

            var result = await _userManager.CreateAsync(user, registerUser.Senha);
            if (result.Succeeded)
            {
                /**/
                try
                {
                    await _iusuarioApp.IncluirUsuario(registerUser);
                }
                catch (Exception)
                {
                    await _userManager.DeleteAsync(user);
                    throw;
                }
                
                if (_notificacoes.TemNotificacoes)
                {
                    await  _userManager.DeleteAsync(user);
                    return CustomResponse(registerUser);
                }


                if (authenticar)
                {
                    await _signInManager.SignInAsync(user, false);
                    return CustomResponse(await Util.GerarJwt(user.Email, _userManager, _appSettings));
                }

                return CustomResponse(registerUser);

            }
            foreach (var error in result.Errors)
            {
                AdicionarNotificacao(error.Description);
            }
            return CustomResponse(registerUser);
        }

        [HttpPost]
        [Route("entrar")]
        [AllowAnonymous]
       // [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginUserViewModel loginUser)
        {

            try
            {
                if (!ModelState.IsValid) return CustomResponse(ModelState);

                if (!await _authEntrar.IsSatisfiedBy(loginUser))
                {
                    return CustomResponse(loginUser);
                };

                var result = await _signInManager.PasswordSignInAsync(loginUser.Email, loginUser.Senha, false, true);

                if (result.Succeeded)
                {
                    return CustomResponse(await Util.GerarJwt(loginUser.Email, _userManager, _appSettings));
                }
                if (result.IsLockedOut)
                {
                    var msg = _resourcesManageMemory.ObterResources(new Resources.Models.Resources<string>
                    {
                        Modulos = Resources.Enuns.ModulosEnum.Usuario,
                        ResourceValue = Resources.Enuns.ResourceValueEnum.UsuarioBloqueadoTentativasInvalidas
                    }, null).FirstOrDefault()?.Mensagem;
                    AdicionarNotificacao(msg);

                    return CustomResponse(loginUser);
                }
                var msgIncorretos = _resourcesManageMemory.ObterResources(new Resources.Models.Resources<string>
                {
                    Modulos = Resources.Enuns.ModulosEnum.Usuario,
                    ResourceValue = Resources.Enuns.ResourceValueEnum.UsuarioSenhaIncorretos
                }, null).FirstOrDefault()?.Mensagem;

                AdicionarNotificacao(msgIncorretos);
                return CustomResponse(loginUser);
            }
            catch (Exception ex)
            {
                return CustomResponse(ex);
            }
        }

    }
}