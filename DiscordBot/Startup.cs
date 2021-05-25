using Comum;
using DSharpPlus;
using DSharpPlus.CommandsNext;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Servicos;
using System;
using System.Threading;
using System.Threading.Tasks;
using Repositorio;
using Microsoft.Extensions.Hosting;
using AutoMapper;
using Modelos;
using DTO;
using DSharpPlus.Interactivity;
using Modulos;

namespace DiscordBot
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services) 
        {
            InstanciarRepositorios(services);
            InstanciarServicos(services);
            InstanciarModulos(services);
            InstanciarDiscordServicos(services);

            //Inicia Bot
            services.AddHostedService<StartupService>();

        }

        public void InstanciarDiscordServicos(IServiceCollection services) 
        {
            var discordConfiguration = new DiscordConfiguration()
            {
                Token = BotCredenciais.Token,
                TokenType = TokenType.Bot,
                AutoReconnect = true,
            };

            var interactivityConfiguration = new InteractivityConfiguration()
            {
                PollBehaviour = DSharpPlus.Interactivity.Enums.PollBehaviour.KeepEmojis,
                Timeout = TimeSpan.FromMinutes(3)
            };

            services.AddSingleton<InteractivityConfiguration, InteractivityConfiguration>(options => new InteractivityConfiguration(interactivityConfiguration));
            services.AddSingleton<DiscordClient, DiscordClient>(options => new DiscordClient(discordConfiguration));
            services.AddSingleton<CommandsNextConfiguration, CommandsNextConfiguration>(options => new CommandsNextConfiguration()
            {
                StringPrefixes = new[] { BotCredenciais.Prefixo },
                EnableMentionPrefix = true,
                EnableDms = false,
                Services = services.BuildServiceProvider()

            });

        }

        public void InstanciarServicos(IServiceCollection services) 
        {
            services.AddSingleton<ArcanoServico, ArcanoServico>();
            services.AddSingleton<AtributoServico, AtributoServico>();
            services.AddSingleton<DadoServico, DadoServico>();
            services.AddSingleton<EfeitoServico, EfeitoServico>();
            services.AddSingleton<FormacaoServico, FormacaoServico>();
            services.AddSingleton<NacaoServico, NacaoServico>();
            services.AddSingleton<PericiaServico, PericiaServico>();
            services.AddSingleton<PersonagemServico, PersonagemServico>();
            services.AddSingleton<ReligiaoServico, ReligiaoServico>();
            services.AddSingleton<VantagemServico, VantagemServico>();
        }

        public void InstanciarModulos(IServiceCollection services)
        {
            services.AddSingleton<BotModulo, BotModulo>();
            services.AddSingleton<ComandosModulo, ComandosModulo>();
        }

        public void InstanciarRepositorios(IServiceCollection services) 
        {
            services.AddAutoMapper(typeof(SourceMappingProfile).Assembly);
            services.AddDbContext<AplicacaoContexto>();

            services.AddSingleton<BaseRepositorio, BaseRepositorio>();
            services.AddSingleton<ArcanoRepositorio, ArcanoRepositorio>();
            services.AddSingleton<AtributoRepositorio, AtributoRepositorio>();
            services.AddSingleton<EfeitoRepositorio, EfeitoRepositorio>();
            services.AddSingleton<FormacaoRepositorio, FormacaoRepositorio>();
            services.AddSingleton<NacaoRepositorio, NacaoRepositorio>();
            services.AddSingleton<PericiaRepositorio, PericiaRepositorio>();
            services.AddSingleton<PersonagemRepositorio, PersonagemRepositorio>();
            services.AddSingleton<ReligiaoRepositorio, ReligiaoRepositorio>();
            services.AddSingleton<VantagemRepositorio, VantagemRepositorio>();


        }

        public void Configure(IApplicationBuilder app, Microsoft.Extensions.Hosting.IHostingEnvironment env) 
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }
        }

    }

    public class StartupService : IHostedService
    {
        private IServiceProvider services;

        public StartupService(IServiceProvider services)
        {
            this.services = services;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            var scope = services.CreateScope();
            var botServico = scope.ServiceProvider.GetRequiredService<BotModulo>();
            await botServico.RunAsync();
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
