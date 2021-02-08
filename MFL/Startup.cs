using System;
using System.Net.Http.Headers;
using MFL.Common.Config;
using MFL.Data.Context;
using MFL.Data.Repository;
using MFL.Services;
using MFL.Services.Clients;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using MFL.Data.Models;
using System.Net;
using System.Net.Http;
using MFL.Jobs;
using Hangfire;
using Hangfire.MySql;

namespace MFL
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors();
            services.AddControllers();

            services.AddHangfire(option => {
                option.UseStorage(
                    new MySqlStorage(
                        Configuration.GetConnectionString("HangfireConnection"),
                        new MySqlStorageOptions
                        {
                            TablesPrefix = "Hangfire"
                        }));
            });

            services.AddHangfireServer();

            var container = new CookieContainer();
            services.AddSingleton(container);
            services.AddHttpClient<IMFLHttpClient, MFLHttpClient>(client =>
                {
                    client.BaseAddress = new Uri("https://api.myfantasyleague.com/");
                    client.DefaultRequestHeaders.Add("User-Agent", "DOTNETCOREMFL");
                })
                .ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler()
                 {
                     CookieContainer = container,
                     UseCookies = true
                 });

            services.AddDbContext<MFLContext>(options => options.UseMySQL(Configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IPlayerService, PlayerService>();
            services.AddScoped<IWaiverService, WaiverService>();
            services.AddScoped<ILeagueService, LeagueService>();
            services.AddScoped<IFranchiseService, FranchiseService>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            var _appSettings = Configuration
                .GetSection(AppSettingsOptions.AppSettings)
                .Get<AppSettingsOptions>();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);

            services.AddAuthentication(x =>
                {
                    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(x =>
                {
                    x.RequireHttpsMetadata = false;
                    x.SaveToken = true;
                    x.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IUserService userService, ILeagueService leagueService, IPlayerService playerService)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseHttpsRedirection();

            var user = new AuthenticationRequest()
            {
                Username = "Chevren",
                Password = "Bleem9598!"
            };

            userService.AuthenticateUser(user).Wait();
            //leagueService.GetMyLeagues().Wait();
            //leagueService.GetMyLeagues().Wait();
            playerService.SyncPlayers(2020).Wait();
        }
    }
}
