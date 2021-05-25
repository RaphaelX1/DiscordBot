using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using Mensagens;
using System;
using System.Threading.Tasks;

namespace Modulos
{
    public partial class ComandosModulo
    {
        [Command("d")]
        [Description("rola a quantidade de dados do tipo selecionado")]
        public async Task RolarDado(CommandContext context, [Description("Valor total do dado")] int valorDado, [Description("Quantidade de dados")] int quantidadeDados = 1)
        {
            try
            {
                var resultado = await _dadoServico.Lancar(quantidadeDados, valorDado);

                await context.Channel.SendMessageAsync(resultado).ConfigureAwait(false);
            }
            
            catch (Exception)
            {
                await context.Channel.SendMessageAsync(MensagensCrud.Falha).ConfigureAwait(false);
            }

        }
    }
}
