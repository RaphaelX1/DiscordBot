using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.Interactivity;
using DSharpPlus.Interactivity.Extensions;
using System.Threading.Tasks;

namespace Modulos
{
    public class BotModulo
    {
        public DiscordClient _client;
        public CommandsNextConfiguration _commandsConfig;
        public InteractivityConfiguration _interactivityConfig;

        public BotModulo(DiscordClient client, CommandsNextConfiguration commandsConfig, InteractivityConfiguration interactivityConfig)
        {
            _client = client;
            _commandsConfig = commandsConfig;
            _interactivityConfig = interactivityConfig;

        }
        public async Task RunAsync() 
        {
            _client.UseInteractivity(_interactivityConfig);

            var commands = _client.UseCommandsNext(_commandsConfig);

            commands.RegisterCommands<ComandosModulo>();

            await _client.ConnectAsync();

            await Task.Delay(-1);
        }

    }
}
