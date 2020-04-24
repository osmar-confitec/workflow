
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkFlowIdentity.Context;
using WorkFlowIdentity.Models;

namespace WorkFlowApi.Config
{
    public static class IdentityConfigApi
    {

        public static IServiceCollection AddIdentityConfiguration(this IServiceCollection services,
       IConfiguration configuration)
        {


            /*adicionar do dbcontext */
            
            services.AddDbContext<ApplicationDbContextIdentity>(options =>
              options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
            services.AddIdentity<ApplicationUser, IdentityRole>()

                .AddEntityFrameworkStores<ApplicationDbContextIdentity>()
                .AddErrorDescriber<IdentityMensagensPortugues>()
                .AddDefaultTokenProviders();
                

            /*Adicionando store widout entitiy framework*/

            /*
            services.AddTransient<IUserStore<ApplicationUserWidoutEntity>, UserStore>();
            services.AddTransient<IRoleStore<ApplicationRoleWidoutEntity>, RoleStore>();

            services.AddIdentity<ApplicationUserWidoutEntity, ApplicationRoleWidoutEntity>()
                .AddDefaultTokenProviders()
                .AddErrorDescriber<IdentityMensagensPortugues>()
                ;
                */

            services.Configure<IdentityOptions>(options =>
            {
                // Password settings
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 0;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;
            });

            return services;


        }
    }
}
