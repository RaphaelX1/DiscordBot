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
        [Command("personagem-pericia-cad")]
        [Description("associa uma nova pericia ao personagem")]
        public async Task CadastrarPersonagemPericia(CommandContext context, [Description("Nome da personagem")] string nomePersonagem, [Description("nome da pericia")] string nomePericia,
           [Description("valor da pericia")] string valor)
        {
            try
            {
                await _personagemServico.CadastrarPericia(valor, nomePersonagem, nomePericia);

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

        [Command("personagem-pericia-info")]
        [Description("Busca informações das perícias de uma personagem.")]
        public async Task ObterPersonagemPericia(CommandContext context, [Description("Nome da personagem")] string nome)
        {
            try
            {
                var retorno = await _personagemServico.ObterPericiasInfo(nome);

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
        [Command("personagem-pericia-edit")]
        [Description("edita informações das pericias de uma personagem.")]
        public async Task EditarPersonagemPericia(CommandContext context, [Description("Nome da personagem")] string nomePersonagem, [Description("nome da pericia do personagem")] string nomePericia,
          [Description("valor a adicionar na pericia")] string valor)
        {
            try
            {
                await _personagemServico.EditarPericia(valor, nomePersonagem, nomePericia);

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

        [RequireRoles(RoleCheckMode.Any, "Master")]
        [Command("personagem-pericia-del")]
        [Description("deleta a associação de uma pericia a personagem informada.")]
        public async Task ExcluirPersonagemPericia(CommandContext context, [Description("Nome da personagem")] string nomePersonagem, [Description("nome da pericia personagem")] string nomePericia)
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
                    await _personagemServico.ExcluirPericia(nomePersonagem, nomePericia);
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
