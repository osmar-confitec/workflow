using AutoMapper;

using System;
using System.Collections.Generic;
using System.Text;
using WorkFlowDominio.Entities;
using WorkFlowIdentity.Models;
using WorkFlowViewModel;

namespace WorkFlowAplicacao.AutoMapperApp
{
   public class DomainToViewModelMappingProfile : Profile
    {
        public DomainToViewModelMappingProfile()
        {
            
            CreateMap<Cliente, ClienteIncluirViewModel>();
            CreateMap<Cliente, ClienteAlterarViewModel>();

            CreateMap<Usuario, RegisterUserViewModel>();

            //CreateMap<Usuario, UsuarioIncluirViewModel>()
            //    .ForMember(dest => dest.Senha, opts => opts.MapFrom(source => source.Senha))
            //    .ForMember(dest => dest.CPF, opts => opts.MapFrom(source => source.CPF.FormatCPF()))
            //    ;
            //CreateMap<Usuario, UsuarioAlterarViewModel>();
            //CreateMap<Usuario, UsuarioLogarViewModel>();
            //UsuarioLogarViewModel

            //UsuarioIncluirViewModel
        }
    }
}
