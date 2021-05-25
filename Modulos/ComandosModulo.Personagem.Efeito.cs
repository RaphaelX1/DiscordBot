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
        [Command("personagem-efeito-cad")]
        [Description("associa um novo efeito ao personagem")]
        public async Task CadastrarPersonagemEfeito(CommandContext context, [Description("Nome da personagem")] string nomePersonagem, [Description("nome do efeito personagem")] string nomeEfeito,
           [Description("valor do efeito")] string valor)
        {
            try
            {
                await _personagemServico.CadastrarEfeito(valor, nomePersonagem, nomeEfeito);

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

        [Command("personagem-efeito-info")]
        [Description("Busca informações dos efeitos de uma personagem.")]
        public async Task ObterPersonagemEfeito(CommandContext context, [Description("Nome da personagem")] string nome)
        {
            try
            {
                var retorno = await _personagemServico.ObterEfeitosInfo(nome);

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
        [Command("personagem-efeito-edit")]
        [Description("edita informações dos efeitos de uma personagem.")]
        public async Task ObterPersonagemEfeito(CommandContext context, [Description("Nome da personagem")] string nomePersonagem, [Description("nome do efeito personagem")] string nomeEfeito,
          [Description("valor a adicionar no efeito")] string valor)
        {
            try
            {
                await _personagemServico.EditarEfeito(valor, nomePersonagem, nomeEfeito);

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
        [Command("personagem-efeito-del")]
        [Description("deleta a associação de um atributo a personagem informada.")]
        public async Task ExcluirPersonagemEfeito(CommandContext context, [Description("Nome da personagem")] string nomePersonagem, [Description("nome do efeito personagem")] string nomeEfeito)
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
                    await _personagemServico.ExcluirEfeito(nomePersonagem, nomeEfeito);
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
