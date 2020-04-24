using AutoMapper;
using FluentValidation;
using MetodosComunsApi;
using Resources;
using Resources.Models;
using Resources.Util;
using SlnNotificacoesApi;
using Specifications.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkFlowDominio.Entities;
using WorkFlowDominio.Interfaces;
using WorkFlowIdentity.Models;

namespace WorkFlowDominio.Specifications
{


    public class AuthEntrar : CompositeSpecificationNotifications<LoginUserViewModel>, IAuthEntrar
    {

        ResourcesManageMemory _resourcesManageMemory;
        IUsuarioRepository _usuarioRepository;

        public AuthEntrar(IUsuarioRepository usuarioRepository,
                            IUser user,
                            ListNotificacoes<Notificacao> notificacoes,
                            ResourcesManageMemory resourcesManageMemory)
                        : base(user, notificacoes)
        {
            _usuarioRepository = usuarioRepository;
            _resourcesManageMemory = resourcesManageMemory;
            ObterResources();

            RuleFor(x => x.Email).EmailAddress().WithMessage(Res.ObterResourceMessage(null, Resources.Enuns.ResourceValueEnum.EmailInvalido));

            RuleFor(x=> x.Senha).NotEmpty().WithMessage(Res.ObterResourceMessage(null, Resources.Enuns.ResourceValueEnum.SenhaNaoPodeServazia));


        }

        void ObterResources()
        {
            AdicionarListaResources(_resourcesManageMemory.ObterResources(null, new List<Resources.Models.Resources<string>>() {
                 new Resources<string>{
                    ResourceValue = Resources.Enuns.ResourceValueEnum.EmailInvalido
                },
                new Resources<string>{
                    Modulos = Resources.Enuns.ModulosEnum.Usuario,
                    ResourceValue = Resources.Enuns.ResourceValueEnum.UsuarioNaoEncontrado
                },
                  new Resources<string>{
                    Modulos = Resources.Enuns.ModulosEnum.Usuario,
                    ResourceValue = Resources.Enuns.ResourceValueEnum.UsuarioInativo
                },
                 new Resources<string>{
                    
                    ResourceValue = Resources.Enuns.ResourceValueEnum.SenhaNaoPodeServazia
                },
                new Resources<string>{
                    Modulos = Resources.Enuns.ModulosEnum.Usuario,
                    ResourceValue = Resources.Enuns.ResourceValueEnum.UsuarioSenhaIncorretos
                }
                  //
            }));
        }

        public async override Task<bool> IsSatisfiedBy(LoginUserViewModel candidate)
        {
            
            /*somatoria das regras desse módulo mais associados*/
            var Satisfied = await base.IsSatisfiedBy(candidate);
            //não satisfez retorna 
            if (!Satisfied)
                return Satisfied;


            var UsuarioObtido =   await _usuarioRepository.ObterUsuarioEmail(candidate.Email);
            if (UsuarioObtido == null)
                AdicionarNotificacao(new Erro(Res.ObterResourceMessageClass(Resources.Enuns.ModulosEnum.Usuario, Resources.Enuns.ResourceValueEnum.UsuarioNaoEncontrado)));

            if (UsuarioObtido != null && !UsuarioObtido.Ativo)
                AdicionarNotificacao(new Erro(Res.ObterResourceMessageClass(Resources.Enuns.ModulosEnum.Usuario, Resources.Enuns.ResourceValueEnum.UsuarioInativo)));

            if (UsuarioObtido != null &&  !MetodosComunsApi.MetodosComuns.VerificarSenhaHash(candidate.Senha, UsuarioObtido.SenhaHash, UsuarioObtido.SenhaSalt))
                AdicionarNotificacao(new Erro(Res.ObterResourceMessageClass(Resources.Enuns.ModulosEnum.Usuario, Resources.Enuns.ResourceValueEnum.UsuarioSenhaIncorretos)));

            /*adicionar e não salvar*/
            if (_notificacoes.TemErros)
                return false;

            return true;
        }

        public override IEnumerable<Resources<string>> ObterRes() => null;
    }

    public class AuthDeletarUsuario : CompositeSpecificationNotifications<Usuario>, IAuthDeletarUsuario
    {

        readonly IUsuarioRepository _usuarioRepository;

        public AuthDeletarUsuario(IUsuarioRepository usuarioRepository,IUser user, ListNotificacoes<Notificacao> notificacoes) 
                    : base(user, notificacoes)
        {

            _usuarioRepository = usuarioRepository;
        }

        public async Task DeletarUsuario(Guid UsuarioId)
        {
            var user =  await _usuarioRepository.ObterPorId(UsuarioId);
            await IsSatisfiedBy(user);
        }

        public async override Task<bool> IsSatisfiedBy(Usuario candidate)
        {
            candidate.StateEntityBase = StateEntityBaseEnum.Delecao;
            /*somatoria das regras desse módulo mais associados*/
            var Satisfied = await base.IsSatisfiedBy(candidate);
            //não satisfez retorna 
            if (!Satisfied)
                return Satisfied;

            _usuarioRepository.AtualizarNoSave(candidate);
            return true;

        }

        public override IEnumerable<Resources<string>> ObterRes() => null;
    }

    public class AuthInserirUsuarioDadosValidos : CompositeSpecificationNotifications<RegisterUserViewModel>, 
        IAuthInserirUsuarioDadosValidos
    {
        readonly ResourcesManageMemory _resourcesManageMemory;



        public AuthInserirUsuarioDadosValidos(IUser user, ListNotificacoes<Notificacao> notificacoes,ResourcesManageMemory resourcesManageMemory)
            : base(user, notificacoes)
        {
            _resourcesManageMemory = resourcesManageMemory;

            ObterResources();
            AplicarRegras();

        }

        void AplicarRegras()
        {

     
            RuleFor(x => x.DataNascimento).Custom(FluentValidationCommons.Maior18(Res.ObterResourceMessageClass(null, Resources.Enuns.ResourceValueEnum.Maiorde18)));

            RuleFor(x => x.Nome).Custom(FluentValidationCommons.Nome(Res.ObterResourceMessageClass(null, Resources.Enuns.ResourceValueEnum.DescricaoNomeRequerido)));

            RuleFor(x => x.SobreNome).Custom(FluentValidationCommons.Nome(Res.ObterResourceMessageClass(null, Resources.Enuns.ResourceValueEnum.SobreNomeRequerido)));

            RuleFor(x => x.Email).EmailAddress().WithMessage(Res.ObterResourceMessage(null, Resources.Enuns.ResourceValueEnum.EmailInvalido));

            RuleFor(x => x).Must(x => x.ConfirmarSenha.Equals(x.Senha))
                           .WithMessage(Res.ObterResourceMessage(Resources.Enuns.ModulosEnum.Usuario, Resources.Enuns.ResourceValueEnum.UsuarioSenhaConfirmNaoBatem));

            RuleFor(x => x.CPF).Custom(FluentValidationCommons.ECpf(Res.ObterResourceMessageClass(null, Resources.Enuns.ResourceValueEnum.CPFInvalido)));

            RuleFor(x => x.Senha).Custom((senha, context) =>
            {
                ForcaDaSenha[] forcaDaSenhas = { ForcaDaSenha.Aceitavel, ForcaDaSenha.Forte, ForcaDaSenha.Segura };

                if (string.IsNullOrEmpty(senha))
                {
                    context.AddFailure(new ValidationFailureCustom(null, null, new Erro(Res.ObterResourceMessageClass(Resources.Enuns.ModulosEnum.Usuario, Resources.Enuns.ResourceValueEnum.UsuarioSenhaInvalida))));
                    return;
                }

                var forcaSenha = new ChecaForcaSenha(senha);

                if (!forcaDaSenhas.Contains(forcaSenha.ForcaDaSenha))
                {
                    var res = Res.ObterResourceMessageClass(Resources.Enuns.ModulosEnum.Usuario, Resources.Enuns.ResourceValueEnum.UsuarioForcaSenhaInvalida);
                    res.Mensagem = string.Format(res.Mensagem, forcaSenha.ForcaDaSenha);
                    context.AddFailure(new ValidationFailureCustom(null, null, new Erro(res)));
                }
                return;

            });
        }

        private void ObterResources()
        {
            AdicionarListaResources(_resourcesManageMemory.ObterResources(null, new List<Resources.Models.Resources<string>>() {
                new Resources<string>{
                    Modulos = Resources.Enuns.ModulosEnum.Usuario,
                    ResourceValue = Resources.Enuns.ResourceValueEnum.UsuarioForcaSenhaInvalida
                },
                   new Resources<string>{
                    Modulos = Resources.Enuns.ModulosEnum.Usuario,
                    ResourceValue = Resources.Enuns.ResourceValueEnum.UsuarioSenhaInvalida
                },
                new Resources<string>{
                    Modulos = Resources.Enuns.ModulosEnum.Usuario,
                    ResourceValue = Resources.Enuns.ResourceValueEnum.UsuarioSenhaConfirmNaoBatem
                },
                  new Resources<string>{
                    ResourceValue = Resources.Enuns.ResourceValueEnum.EmailInvalido
                },
                  new Resources<string>{
                    ResourceValue = Resources.Enuns.ResourceValueEnum.CPFInvalido
                },
                  new Resources<string>{
                    ResourceValue = Resources.Enuns.ResourceValueEnum.DescricaoNomeRequerido
                },
                   new Resources<string>{
                    ResourceValue = Resources.Enuns.ResourceValueEnum.SobreNomeRequerido
                },
                    new Resources<string>{
                    ResourceValue = Resources.Enuns.ResourceValueEnum.Maiorde18
                }
                   
            }));
        }

        public override IEnumerable<Resources<string>> ObterRes() => null;
    }

    public class AuthInserirUsuario : CompositeSpecificationNotifications<Usuario>, IAuthInserirUsuario
    {

        readonly IAuthInserirUsuarioDadosValidos _authInserirUsuarioDadosValidos;
        readonly IMapper _mapper;
        readonly IUsuarioRepository _usuarioRepository;
        readonly ResourcesManageMemory _resourcesManageMemory;

        public AuthInserirUsuario(IUsuarioRepository usuarioRepository,
                                IMapper mapper,
                                IAuthInserirUsuarioDadosValidos authInserirUsuarioDadosValidos,
                                IUser user, 
                                ListNotificacoes<Notificacao> notificacoes
                            , ResourcesManageMemory resourcesManageMemory) : base(user, notificacoes)
        {

            _authInserirUsuarioDadosValidos = authInserirUsuarioDadosValidos;
            _usuarioRepository = usuarioRepository;
            _mapper = mapper;
            _resourcesManageMemory = resourcesManageMemory;

            ObterResources();


        }

         void ObterResources()
        {
            AdicionarListaResources(_resourcesManageMemory.ObterResources(null, new List<Resources.Models.Resources<string>>() {
                new Resources<string>{
                    Modulos = Resources.Enuns.ModulosEnum.Usuario,
                    ResourceValue = Resources.Enuns.ResourceValueEnum.UsuarioCPFExistente
                },
                   new Resources<string>{
                    Modulos = Resources.Enuns.ModulosEnum.Usuario,
                    ResourceValue = Resources.Enuns.ResourceValueEnum.UsuarioInativo
                },
                new Resources<string>{
                    Modulos = Resources.Enuns.ModulosEnum.Usuario,
                    ResourceValue = Resources.Enuns.ResourceValueEnum.UsuarioEmailExistente
                }
            }));
        }

        public async Task<RegisterUserViewModel> InserirUsuario(RegisterUserViewModel registerUserViewModel)
        {
            if (!await _authInserirUsuarioDadosValidos.IsSatisfiedBy(registerUserViewModel))
            {
                return null;
            }
            var usuario = _mapper.Map<Usuario>(registerUserViewModel);

            byte[] SenhaHash = null;
            byte[] SenhaSalt = null;
            registerUserViewModel.Senha.CriarSenhaHash(out SenhaHash, out SenhaSalt);

            usuario.SenhaHash = SenhaHash;
            usuario.SenhaSalt = SenhaSalt;

            if (await IsSatisfiedBy(usuario))
            {
                registerUserViewModel.Id = usuario.Id.ToString();
                return registerUserViewModel;
            }
            return null;
        }

        public override async Task<bool> IsSatisfiedBy(Usuario candidate)
        {
            /*escrita padrão pra esse método*/
            candidate.StateEntityBase = StateEntityBaseEnum.Inclusao;
            /*somatoria das regras desse módulo mais associados*/
            var Satisfied = await base.IsSatisfiedBy(candidate);
            //não satisfez retorna 
            if (!Satisfied)
                return Satisfied;


            var usuarioCpf =  await  _usuarioRepository.ObterUsuarioCPF(candidate.CPF);
            if (usuarioCpf!= null)
            {
                var res = Res.ObterResourceMessageClass(Resources.Enuns.ModulosEnum.Usuario, Resources.Enuns.ResourceValueEnum.UsuarioCPFExistente);
                res.Mensagem = string.Format(res.Mensagem, candidate.CPF,
                                                usuarioCpf.Ativo ? null :
                                                Res.ObterResourceMessage(Resources.Enuns.ModulosEnum.Usuario, Resources.Enuns.ResourceValueEnum.UsuarioInativo));

                _notificacoes.Add(new Erro(res));
                

            }
            var userEmail = await _usuarioRepository.ObterUsuarioEmail(candidate.Email);
            if (userEmail != null)
            {
                var res = Res.ObterResourceMessageClass(Resources.Enuns.ModulosEnum.Usuario, Resources.Enuns.ResourceValueEnum.UsuarioEmailExistente);
                res.Mensagem = string.Format(res.Mensagem, candidate.Email,
                                             userEmail.Ativo ? null :
                                             Res.ObterResourceMessage(Resources.Enuns.ModulosEnum.Usuario, Resources.Enuns.ResourceValueEnum.UsuarioInativo));
                _notificacoes.Add(new Erro(res));
            }
            /*adicionar e não salvar*/
            if (_notificacoes.TemErros)
                return false;

            candidate.CPF = candidate.CPF.SomenteNumeros();
            await _usuarioRepository.AdicionarNoSave(candidate);
            return true;
        }

        public override IEnumerable<Resources<string>> ObterRes() => null;
    }

    /**/


}
