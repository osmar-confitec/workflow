using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WorkFlowApi.Config
{
    public static class CorsConfig
    {

        public static IServiceCollection AddCorsConfiguration(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("Development",
                    //  builder => builder.AllowAnyOrigin()
                    builder => builder.WithOrigins("https://localhost:5001", "http://localhost:4200/")
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials());

                options.AddPolicy("Production",
                  builder =>
                      builder
                          .WithMethods("GET")
                          .WithOrigins("http://desenvolvedor.io")
                          .SetIsOriginAllowedToAllowWildcardSubdomains()
                          //.WithHeaders(HeaderNames.ContentType, "x-custom-header")
                          .AllowAnyHeader());
            });

            return services;

        }
    }
}
