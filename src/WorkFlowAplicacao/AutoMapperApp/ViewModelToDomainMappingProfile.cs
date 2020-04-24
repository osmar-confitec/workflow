using AutoMapper;

using System;
using System.Collections.Generic;
using System.Text;
using WorkFlowDominio.Entities;
using WorkFlowIdentity.Models;
using WorkFlowViewModel;

namespace WorkFlowAplicacao.AutoMapperApp
{
    public class ViewModelToDomainMappingProfile : Profile
    {


        public ViewModelToDomainMappingProfile()
        {
            CreateMap<ClienteIncluirViewModel, Cliente>();
            CreateMap<ClienteAlterarViewModel, Cliente>();
            CreateMap<RegisterUserViewModel, Usuario>();

            //    CreateMap<UsuarioIncluirViewModel, Usuario>()


            //        ;
            //    CreateMap<UsuarioAlterarViewModel, Usuario>();
            //    CreateMap<UsuarioLogarViewModel, Usuario>();
        }
    }
}
