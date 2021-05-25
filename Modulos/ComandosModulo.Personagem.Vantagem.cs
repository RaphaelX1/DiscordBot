using Comum;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using DSharpPlus.Interactivity.Extensions;
using Mensagens;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Modulos
{
    public partial class ComandosModulo
    {
        [RequireRoles(RoleCheckMode.Any, "Master")]
        [Command("personagem-vantagem-cad")]
        [Description("associa uma nova vantagem ao personagem")]
        public async Task CadastrarPersonagemVantagem(CommandContext context, [Description("Nome da personagem")] string nomePersonagem, [Description("nome da vantagem")] string nomeVantagem)
        {
            try
            {
                await _personagemServico.CadastrarVantagem(nomePersonagem, nomeVantagem);

                await context.Channel.SendMessageAsync(MensagensCrud.Sucesso).ConfigureAwait(false);
            }
            catch (RegraException e)
            {
                await context.Channel.SendMessageAsync(e.Message).ConfigureAwait(false);
            }
            catch (Exception)
            {
                await context.Channel.SendMessageAsync(MensagensCrud.Falha).ConfigureAwait(false);
            }
        }


        [Command("personagem-vantagem-info")]
        [Description("Busca informações das vantagens de uma personagem.")]
        public async Task ObterPersonagemVantagem(CommandContext context, [Description("Nome da personagem")] string nome)
        {
            try
            {
                var retorno = await _personagemServico.ObterVantagensInfo(nome);

                if (retorno == null)
                    throw new RegraException(MensagensCrud.NaoEncontrado);

                await context.Channel.SendMessageAsync(embed: retorno).ConfigureAwait(false);

            }
            catch (RegraException e)
            {
                await context.Channel.SendMessageAsync(e.Message).ConfigureAwait(false);
            }
            catch (Exception)
            {
                await context.Channel.SendMessageAsync(MensagensCrud.Falha).ConfigureAwait(false);
            }

        }


        [RequireRoles(RoleCheckMode.Any, "Master")]
        [Command("personagem-vantagem-del")]
        [Description("deleta a associação de uma vantagem a personagem informada.")]
        public async Task ExcluirPersonagemVantagem(CommandContext context, [Description("Nome da personagem")] string nomePersonagem, [Description("nome da formacao")] string nomeFormacao)
        {
            try
            {
                var interactivity = context.Client.GetInteractivity();

                var mensagemConfirmacao = await context.Channel.SendMessageAsync(embed: _personagemServico.ConfirmarExclusao()).ConfigureAwait(false);

                await mensagemConfirmacao.CreateReactionAsync(DiscordEmoji.FromUnicode("👍")).ConfigureAwait(false);
                await mensagemConfirmacao.CreateReactionAsync(DiscordEmoji.FromUnicode("👎")).ConfigureAwait(false);

                var resultado = await interactivity.CollectReactionsAsync(mensagemConfirmacao, TimeSpan.FromSeconds(5)).ConfigureAwait(false);

                if (resultado.Select(o => o.Emoji.ToString()).Contains("👍") && resultado.SelectMany(o => o.Users.ToList()).Contains(context.User))
                {
                    await _personagemServico.ExcluirVantagem(nomePersonagem, nomeFormacao);
                    await context.Channel.SendMessageAsync(MensagensCrud.Sucesso).ConfigureAwait(false);
                }
                else
                {
                    await context.Channel.SendMessageAsync(MensagensCrud.Abortado).ConfigureAwait(false);
                }

            }
            catch (RegraException e)
            {
                await context.Channel.SendMessageAsync(e.Message).ConfigureAwait(false);
            }
            catch (Exception)
            {
                await context.Channel.SendMessageAsync(MensagensCrud.Falha).ConfigureAwait(false);
            }

        }
    }
}
