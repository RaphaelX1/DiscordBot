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
        [Command("personagem-atributo-cad")]
        [Description("associa um novo atributo ao personagem")]
        public async Task CadastrarPersonagemAtributo(CommandContext context, [Description("Nome da personagem")] string nomePersonagem, [Description("nome do atributo personagem")] string nomeAtributo,
           [Description("valor do Atributo")] string valor)
        {
            try
            {
                await _personagemServico.CadastrarAtributo(valor, nomePersonagem, nomeAtributo);

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

        [Command("personagem-atributo-info")]
        [Description("Busca informações dos atributos de uma personagem.")]
        public async Task ObterPersonagemAtributo(CommandContext context, [Description("Nome da personagem")] string nome)
        {
            try
            {
                var retorno = await _personagemServico.ObterAtributosInfo(nome);

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
        [Command("personagem-atributo-edit")]
        [Description("Busca informações dos atributos de uma personagem.")]
        public async Task ObterPersonagemAtributo(CommandContext context, [Description("Nome da personagem")] string nomePersonagem, [Description("nome do atributo personagem")] string nomeAtributo,
          [Description("valor a adicionar no Atributo")] string valor)
        {
            try
            {
                await _personagemServico.EditarAtributo(valor, nomePersonagem, nomeAtributo);

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
        [Command("personagem-atributo-del")]
        [Description("deleta a associação de um atributo a personagem informada.")]
        public async Task ExcluirPersonagem(CommandContext context, [Description("Nome da personagem")] string nomePersonagem, [Description("nome do atributo personagem")] string nomeAtributo)
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
                    await _personagemServico.ExcluirAtributo(nomePersonagem, nomeAtributo);
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
