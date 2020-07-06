
using System;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using Open.IdentityServer.Domain.Identity;
using Open.IdentityServer.Domain.Modules.LdapModule;
using Open.IdentityServer.Domain.Modules.ProviderModule;
using Open.IdentityServer.Infrastructure;
using Open.IdentityServer.Models;

namespace Open.IdentityServer
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment environment)
        {
            this.Configuration = configuration;
            this.Environment = environment;
        }

        public IConfiguration Configuration { get; }
        public IWebHostEnvironment Environment { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            string provider = this.Configuration["Provider"];
            string connectionString = this.Configuration.GetSection("ConnectionStrings").GetSection(provider)["Default"];
            string migrationAssembly = $"Open.IdentityServer.Migrations.{provider}";

            services.Configure<LdapSettings>(this.Configuration.GetSection("LDAP"));

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

            var authentication = services.AddAuthentication();

            var google = this.Configuration.GetSection("External").GetSection(nameof(Provider.Google)).Get<GoogleProviderSetting>();
            if (!string.IsNullOrWhiteSpace(google?.ClientId))
            {
                authentication.AddGoogle(nameof(Provider.Google), options =>
                {
                    options.CallbackPath = new PathString("/auth/google-callback");

                    options.ClientId = google.ClientId;
                    options.ClientSecret = google.ClientSecret;
                });
            }

            var github = this.Configuration.GetSection("External").GetSection(nameof(Provider.GitHub)).Get<GitHubProviderSetting>();
            if (!string.IsNullOrWhiteSpace(github?.ClientId))
            {
                authentication.AddGitHub(nameof(Provider.GitHub), options =>
                {
                    options.CallbackPath = new PathString("/auth/github-callback");

                    options.ClientId = github.ClientId;
                    options.ClientSecret = github.ClientSecret;

                });
            }

            var azure = this.Configuration.GetSection("External").GetSection(nameof(Provider.Azure)).Get<AzureProviderSetting>();
            if (!string.IsNullOrWhiteSpace(azure?.ClientId))
            {
                authentication.AddOpenIdConnect(nameof(Provider.Azure), Provider.Azure, options =>
                {
                    options.CallbackPath = new PathString("/auth/azure-callback");

                    options.Authority = azure.AuthorityFull;
                    options.ClientId = azure.ClientId;
                    options.ClientSecret = azure.ClientSecret;
                    options.ResponseType = azure.ResponseType;
                });
            }

            if (this.Environment.IsDevelopment())
            {
                identityBuilder.AddDeveloperSigningCredential();
            }
            else
            {
                // TO-DO temporary, need to set up proper key
                identityBuilder.AddDeveloperSigningCredential();
            }

            services.AddSpaStaticFiles(options => options.RootPath = "wwwroot");
            services.AddResponseCompression();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public static void Configure(IApplicationBuilder app, IWebHostEnvironment env)
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
