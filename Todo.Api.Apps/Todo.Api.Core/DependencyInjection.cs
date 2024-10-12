using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Todo.Api.Core.Services.Account;
using Todo.Api.Core.Services.Core;
using Todo.Api.Core.Services.Master;
using Todo.Api.Shared.Objects;

namespace Todo.Api.Core
{
    public static class DependencyInjection
    {
        public static IServiceCollection RegisterCore(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            services.Configure<SecurityConfig>(options => configuration.Bind(nameof(SecurityConfig), options));

            services.AddScoped<AccountService>();
            services.AddScoped<TokenService>();
            services.AddScoped<ActivityService>();
            services.AddScoped<DocumentNumberService>();

            services.AddHttpClient();

            return services;
        }
    }
}
