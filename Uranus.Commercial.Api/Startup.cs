using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using OpenIddict;
using RawRabbit.vNext;
using System.Linq;
using Uranus.Commercial.Api.Extensions;
using Uranus.Commercial.CommandStack.Commands;
using Uranus.Commercial.CommandStack.Domain.EventHandlers;
using Uranus.Commercial.CommandStack.Events;
using Uranus.Commercial.Infrastructure.Framework;
using Uranus.Commercial.Infrastructure.Persistance.Context;
using Uranus.Commercial.Messaging;
using Uranus.Commercial.Security.Context;
using Uranus.Commercial.Security.Model;

namespace Uranus.Commercial.Api
{
    public partial class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Register Entity Framework
            services.AddEntityFramework()                                   
                    .AddDbContext<SecurityDbContext>(options => 
                        options.UseSqlServer(Configuration["Data:ConnectionStrings:DefaultConnection"],
                                             b => b.MigrationsAssembly("Uranus.Commercial.Api"))
                    );

            services.AddEntityFramework()
                    .AddDbContext<ApplicationDbContext>(options =>
                        options.UseSqlServer(Configuration["Data:ConnectionStrings:DefaultConnection"],
                                             b => b.MigrationsAssembly("Uranus.Commercial.Api"))
            );

            // Register the Identity services
            services.AddIdentity<ApplicationUser, IdentityRole>(o =>
                    {
                        o.Password.RequireDigit = false;
                        o.Password.RequireLowercase = false;
                        o.Password.RequireUppercase = false;
                        o.Password.RequireNonAlphanumeric = false;
                        o.Password.RequiredLength = 6;
                    })
                    .AddEntityFrameworkStores<SecurityDbContext>()
                    .AddDefaultTokenProviders();

            // Register the OpenIddict services
            services.AddOpenIddict<ApplicationUser, IdentityRole, SecurityDbContext>()
                    .AddMvcBinders()
                    .EnableAuthorizationEndpoint("/connect/authorize")
                    .EnableLogoutEndpoint("/connect/logout")
                    .EnableTokenEndpoint("/connect/token")
                    .EnableUserinfoEndpoint("/connect/userinfo")
                    .AllowAuthorizationCodeFlow()
                    .AllowPasswordFlow()
                    .AllowRefreshTokenFlow()
                    .DisableHttpsRequirement()
                    .AddEphemeralSigningKey();

            // Add Messaging with Raw Rabbit
            services.AddRawRabbit();

            // Add framework services.
            //services.AddMvc(config => {
            //    // Forces 401 instead of redirect to Account/Login route
            //    var policy = new AuthorizationPolicyBuilder().AddAuthenticationSchemes("Bearer")
            //                                                 .RequireAuthenticatedUser()
            //                                                 .Build();

            //    config.Filters.Add(new AuthorizeFilter(policy));
            //});

            services.AddMvc();

            // Dependency Injection            
            services.AddApplicationDependencies();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            // Add Logger
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            // Add Developer Error Page
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }

            // Add Cors
            app.UseCors(builder =>
                builder.AllowAnyHeader()
                       .AllowAnyMethod()
                       .AllowAnyOrigin()
            );

            // Add OpenIddict
            app.UseOAuthValidation();
            app.UseIdentity();
            app.UseOpenIddict();

            // Add Mvc
            app.UseMvcWithDefaultRoute();

            // Security Seed
            using (var context = new SecurityDbContext(app.ApplicationServices.GetRequiredService<DbContextOptions<SecurityDbContext>>()))
            {
                if (!context.Applications.Any())
                {
                    context.Applications.Add(new OpenIddictApplication
                    {
                        ClientId = "postman",
                        DisplayName = "Postman",
                        RedirectUri = "https://www.getpostman.com/oauth2/callback",
                        Type = OpenIddictConstants.ClientTypes.Public
                    });

                    context.SaveChanges();
                }
            }

            // Command/Events Handlers Registration to Bus
            var rawRabbitCommandBus = (RawRabbitCommandBus)app.ApplicationServices.GetService(typeof(ICommandBus));
            var rawRabbitEventBus = (RawRabbitEventBus)app.ApplicationServices.GetService(typeof(IEventBus));

            rawRabbitCommandBus.RegisterHandler(typeof(CreateMyEntityCommand), typeof(ICommandHandler<CreateMyEntityCommand>)).Wait();
            rawRabbitCommandBus.RegisterHandler(typeof(UpdateMyEntityCommand), typeof(ICommandHandler<UpdateMyEntityCommand>)).Wait();

            rawRabbitEventBus.RegisterHandler(typeof(MyEntityCreatedEvent), typeof(MyEntityCreatedEventHandler)).Wait();
            rawRabbitEventBus.RegisterHandler(typeof(MyEntityCreatedEvent), typeof(MyEntityCreated2EventHandler)).Wait();
        }
    }
}
