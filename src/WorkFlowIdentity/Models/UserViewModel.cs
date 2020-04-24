using System;
using System.Collections.Generic;
using System.Text;

namespace WorkFlowIdentity.Models
{
    public class RegisterUserViewModel
    {

        public string Id { get; set; }

        public string Email { get; set; }

        public string Senha { get; set; }

        public string ConfirmarSenha { get; set; }

        public string Nome { get; set; }

        public string SobreNome { get; set; }

        public string CPF { get; set; }

        public DateTime DataNascimento { get; set; }

    }

    public class LoginUserViewModel
    {

        public string Email { get; set; }

        public string Senha { get; set; }
    }

    public class UserTokenViewModel
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public IEnumerable<ClaimViewModel> Claims { get; set; }
    }

    public class LoginResponseViewModel
    {
        public string AccessToken { get; set; }
        public double ExpiresIn { get; set; }
        public UserTokenViewModel UserToken { get; set; }
    }

    public class ClaimViewModel
    {
        public int Id { get; set; }

        public string UserId { get; set; }

        public string ClaimType { get; set; }

        public string ClaimValue { get; set; }
    }
}
