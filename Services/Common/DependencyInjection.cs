using Infrastructure.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Repository.Authentication;
using Repository.Common;
using Repository.Department;
using Repository.Designation;
using Repository.PayScale;
using Repository.Users;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Common
{
    public static class DependencyInjection
    {
        public static void InjectDependency(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSignalR();
            services.AddHttpContextAccessor();
            services.AddDbContext<SampleContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("SqlConnection"));
            });
            services.AddScoped<IDbConnection>(db => new SqlConnection(configuration.GetConnectionString("SqlConnection")));

            services.AddCors(p => p.AddPolicy("corsapp", builder =>
            {
                builder.WithOrigins("http://localhost:4200").AllowAnyMethod().AllowAnyHeader().AllowCredentials();
            }));

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(configuration.GetValue<string>("SecurityKey")!)),
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidateLifetime = true,
                        ClockSkew = TimeSpan.Zero
                    };

                    options.Events = new JwtBearerEvents()
                    {
                        OnMessageReceived = context =>
                        {
                            if (context.Request.Cookies.ContainsKey("AuthCookie"))
                            {
                                context.Token = context.Request.Cookies["AuthCookie"];
                            }
                            return Task.CompletedTask;
                        }
                    };
                });
            services.AddSingleton<IAuthorizationPolicyProvider, DynamicAuthorizationPolicyProvider>();
            services.AddAuthorization();

            services.AddControllers();
            services.AddControllers().AddJsonOptions(options => options.JsonSerializerOptions.PropertyNamingPolicy = null);
            services.AddEndpointsApiExplorer();
            
            services.AddScoped<IDapperService, Services.Common.DapperService>();
            services.AddScoped<IUserResponse, Services.Users.NUsers.DALClass>();
            services.AddScoped<IAuthenticationResponse, Services.Authentication.NAuthentication.DALClass>();
            services.AddScoped<IDepartmentResponse, Services.Department.NDepartment.DALClass>();
            services.AddScoped<IDesignationResponse, Services.Designation.NDesignation.DALClass>();
            services.AddScoped<IPayScaleResponse, Services.PayScale.DALClass>();
        } // InjectDependency...
    } // class...

    public class DynamicAuthorizationPolicyProvider : DefaultAuthorizationPolicyProvider
    {
        private readonly IServiceProvider provider;

        public DynamicAuthorizationPolicyProvider(IOptions<AuthorizationOptions> options, IServiceProvider provider) : base(options)
        {
            this.provider = provider;
        } // constructor...

        public override async Task<AuthorizationPolicy?> GetPolicyAsync(string policyName)
        {
            var policy = await base.GetPolicyAsync(policyName);
            if(policy != null)
            {
                return policy;
            } // end if...

            using var scope = provider.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<SampleContext>();
            var modules = db.Modules.ToList();

            foreach (var module in modules)
            {
                if(policyName == $"VIEW_{module.ModuleName}")
                {
                    return new AuthorizationPolicyBuilder().RequireClaim($"VIEW_{module.ModuleName}", "View").Build();
                }
                if(policyName == $"EDIT_{module.ModuleName}")
                {
                    return new AuthorizationPolicyBuilder().RequireClaim($"EDIT_{module.ModuleName}", "Edit").Build();
                }
            } // end of foreach loop...

            return null;
        } // GetPolicyAsync...
    } // class...
}
