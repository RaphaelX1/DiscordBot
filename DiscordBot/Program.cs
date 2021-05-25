using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Servicos;
using System;

namespace DiscordBot
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
            .UseKestrel(options => options.AddServerHeader = false)
            .UseStartup<Startup>();
    }
}
