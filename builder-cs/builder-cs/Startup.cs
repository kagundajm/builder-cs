using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentMigrator.Runner;
using Identity.Dapper.Postgres;
using Identity.Dapper.Postgres.Models;
using Identity.Dapper.Postgres.Stores;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace BuilderCS
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
            //services.AddIdentity<ApplicationUser, ApplicationRole>()
            //    .AddDefaultTokenProviders();  //.AddDefaultUI();
            //services.AddTransient<IUserStore<ApplicationUser>, UserStore>();
            //services.AddTransient<IRoleStore<ApplicationRole>, RoleStore>();
            //services.AddTransient<IDatabaseConnectionFactory>(
            //    provider => new PostgresConnectionFactory(
            //        Configuration.GetConnectionString(AppConnectionString)));

            //services.AddTransient<IDbConnectionFactory>(
            //    provider => new DbConnectionFactory(
            //        Configuration.GetConnectionString(AppConnectionString)));

            //services.AddTransient<IAccountService, AccountService>();


            //services.AddFluentMigratorCore()
            //  .ConfigureRunner(
            //    builder => builder
            //      // Add PostgreSQL support fot FluentMigrator
            //      .AddPostgres()
            //      .WithGlobalConnectionString(AppConnectionString)
            //      .ScanIn(typeof(Account).Assembly).For.Migrations()
            //    ).AddLogging(log => log.AddFluentMigratorConsole());

            services.AddRazorPages();

            //services.AddScoped<AuthenticationStateProvider, CustomAuthenticationStateProvider>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env,
                          IMigrationRunner migrationRunner)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
            });

            //migrationRunner.MigrateUp();
        }
    }
}
