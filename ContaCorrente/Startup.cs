using System.Collections.Generic;
using System.IO;
using ContaCorrente.CrossCutting.Filters;
using ContaCorrente.Domain.Repositories;
using ContaCorrente.Domain.Services;
using ContaCorrente.Infrastructures;
using ContaCorrente.Infrastructures.Persistances;
using ContaCorrente.Infrastructures.Repositories;
using ContaCorrente.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.PlatformAbstractions;
using Swashbuckle.AspNetCore.Swagger;

namespace ContaCorrente
{
    public class Startup
    {
        private readonly IHostingEnvironment _env;
        private readonly IConfiguration _configuration;
        private readonly ILoggerFactory _loggerFactory;

        public Startup(IHostingEnvironment env, IConfiguration configuration, ILoggerFactory loggerFactory)
        {
            _env = env;
            _configuration = configuration;
            _loggerFactory = loggerFactory;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            ConfigureConnection(services);

            ConfigureSwagger(services);

            ConfigureMvc(services);

            services.AddSingleton(x => _configuration);

            services.AddScoped<ILancamentoService, LancamentoService>();

            services.AddScoped<IContaRepository, ContaRepository>();

            services.AddScoped<ILancamentoRepository, LancamentoRepository>();
        }

        private static void ConfigureMvc(IServiceCollection services)
        {
            services.AddMvc(options =>
            {
                options.Filters.Add<ErrorHandlingAttribute>();
            }).SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }
        
        public virtual void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseAuthentication();
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1.0/swagger.json", "Versioned API v1.0");
                });
            }
            app.UseAuthentication();
            app.UseMvc();
        }

        protected virtual void ConfigureSwagger(IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1.0",
                    new Info
                    {
                        Title = "Conta Corrente API",
                        Version = "v1.0",
                        Contact = new Contact
                        {
                            Name = "Thiago Fialho",
                            Url = "https://bitbucket.org/tcfialho/"
                        }
                    });

                var caminhoAplicacao = PlatformServices.Default.Application.ApplicationBasePath;
                var nomeAplicacao = PlatformServices.Default.Application.ApplicationName;
                var caminhoXmlDoc = Path.Combine(caminhoAplicacao, $"{nomeAplicacao}.xml");

                if (File.Exists(caminhoXmlDoc))
                {
                    c.IncludeXmlComments(caminhoXmlDoc);
                }

                c.DescribeAllEnumsAsStrings();

                c.AddSecurityDefinition("Bearer", new ApiKeyScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                    Name = "Authorization",
                    In = "header",
                    Type = "apiKey"
                });

                c.AddSecurityRequirement(new Dictionary<string, IEnumerable<string>>
                {
                    {"Bearer", new string[] { }},
                });
            });
        }

        protected virtual void ConfigureConnection(IServiceCollection services)
        {
            services.AddDbContext<ContaCorrenteContext>(options => {
                var dataconnection = "|DataDirectory|";
                var defaultConnection = _configuration.GetConnectionString("DefaultConnection");
                var appDataPath = Path.Combine(_env.ContentRootPath, "App_Data");
                var connectionString = defaultConnection.Replace(dataconnection, appDataPath);

                options.UseSqlite(connectionString);

                var loggerFactory = new ServiceCollection()
                    .AddLogging(builder => builder.AddDebug().AddFilter(DbLoggerCategory.Database.Command.Name, LogLevel.Information))
                    .BuildServiceProvider()
                    .GetService<ILoggerFactory>();

                options.UseLoggerFactory(loggerFactory);
            });
            services.AddScoped<IDatabaseContext, ContaCorrenteContext>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            
        }
    }
}
