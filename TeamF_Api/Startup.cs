using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamF_Api.DAL;
using TeamF_Api.Security;
using TeamF_Api.Security.PasswordEncoders;
using TeamF_Api.Security.Token;
using TeamF_Api.Services.Implementations;
using TeamF_Api.Services.Interfaces;

namespace TeamF_Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddDbContext<CAFFShopDbContext>(
                options => options
                    .UseLazyLoadingProxies()
                    .UseSqlServer(Configuration.GetConnectionString("SQLServer"))
            ); ;

            services.Configure<SecurityConfiguration>(Configuration.GetSection("Security"));
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(jwtBearerOptions =>
                {
                    jwtBearerOptions.SaveToken = true;
                    jwtBearerOptions.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration.GetSection("Security").GetSection("Secret").Value)),
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidateLifetime = true,
                        ClockSkew = TimeSpan.FromMinutes(5)
                    };
                });

            services.AddAuthorization(options =>
            {
                options.DefaultPolicy = new AuthorizationPolicyBuilder()
                    .RequireClaim(SecurityConstants.RoleClaim, SecurityConstants.BaseUserRole)
                    .RequireClaim(SecurityConstants.UserNameClaim)
                    .Build();
                options.AddPolicy(SecurityConstants.AdminPolicy, policy => policy
                    .RequireClaim(SecurityConstants.RoleClaim, SecurityConstants.BaseUserRole)
                    .RequireClaim(SecurityConstants.RoleClaim, SecurityConstants.AdminRole)
                    .RequireClaim(SecurityConstants.UserNameClaim)
                );
            });

            services.AddScoped<IUserService, UserService>();
            services.AddTransient<IPasswordEncoder, BcryptPasswordEncoder>();
            services.AddScoped<ITokenGenerator, JwtTokenGenerator>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
