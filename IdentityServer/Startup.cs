
using System;

using IdentityServer.Domain.Identity;
using IdentityServer.Domain.Modules.LdapModule;
using IdentityServer.Domain.Modules.ProviderModule;
using IdentityServer.Infrastructure;
using IdentityServer.Models;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace IdentityServer
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment environment)
        {
            Configuration = configuration;
            Environment = environment;
        }

        public IConfiguration Configuration { get; }
        public IWebHostEnvironment Environment { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            string provider = Configuration["Provider"];
            string connectionString = Configuration.GetSection("ConnectionStrings").GetSection(provider)["Default"];
            string migrationAssembly = $"IdentityServer.Migrations.{provider}";

            services.AddSingleton(Configuration.GetSection("LDAP").Get<LdapSetting>());

            services.AddControllers();

            services.AddDbContext<AppIdentityDbContext>(options => options.UseProvider(provider, connectionString, migrationAssembly));

            services.AddScoped<LdapService>();

            services.AddIdentity<AppUser, AppRole>().AddEntityFrameworkStores<AppIdentityDbContext>().AddDefaultTokenProviders();

            var identityBuilder = services.AddIdentityServer(options =>
            {
                options.UserInteraction.LoginUrl = "/account/login";
                options.UserInteraction.LogoutUrl = "/account/logout";
            })
            .AddOperationalStore<AppPersistedGrantDbContext>(options =>
            {
                options.ConfigureDbContext = conf => conf.UseProvider(provider, connectionString, migrationAssembly);

                options.EnableTokenCleanup = true;
                options.TokenCleanupInterval = 30;
            })
            .AddConfigurationStore<AppConfigurationDbContext>(options =>
            {
                options.ConfigureDbContext = conf => conf.UseProvider(provider, connectionString, migrationAssembly);
            })
            .AddInMemoryIdentityResources(Config.GetIdentityResources())
            .AddInMemoryApiResources(Config.GetApiResources())
            .AddInMemoryClients(Config.GetClients())
            .AddProfileService<ProfileService>()
            .AddAspNetIdentity<AppUser>();

            services.AddAuthentication()
                .AddGoogle(nameof(Provider.Google), options =>
                {
                    options.CallbackPath = new PathString("/auth/google-callback");

                    var google = Configuration.GetSection("External").GetSection(nameof(Provider.Google)).Get<GoogleProviderSetting>();
                    options.ClientId = google.ClientId;
                    options.ClientSecret = google.ClientSecret;
                })
                .AddGitHub(nameof(Provider.GitHub), options =>
                {
                    options.CallbackPath = new PathString("/auth/github-callback");

                    var github = Configuration.GetSection("External").GetSection(nameof(Provider.GitHub)).Get<GitHubProviderSetting>();
                    options.ClientId = github.ClientId;
                    options.ClientSecret = github.ClientSecret;

                })
                .AddOpenIdConnect(nameof(Provider.Azure), Provider.Azure, options =>
                {
                    options.CallbackPath = new PathString("/auth/azure-callback");

                    var azure = Configuration.GetSection("External").GetSection(nameof(Provider.Azure)).Get<AzureProviderSetting>();
                    options.Authority = azure.AuthorityFull;
                    options.ClientId = azure.ClientId;
                    options.ClientSecret = azure.ClientSecret;
                    options.ResponseType = azure.ResponseType;
                });



            if (Environment.IsDevelopment())
            {
                identityBuilder.AddDeveloperSigningCredential();
            }
            else
            {
                throw new Exception("Need to confgure key");
            }

            services.AddSpaStaticFiles(options => options.RootPath = "ClientApp/dist");
            services.AddResponseCompression();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(options => options.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());

            app.UseRouting();

            app.UseIdentityServer();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseStaticFiles();
            app.UseSpaStaticFiles();

            app.UseResponseCompression();

            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                {
                    spa.UseProxyToSpaDevelopmentServer("http://localhost:4400");
                }
            });
        }
    }
}
