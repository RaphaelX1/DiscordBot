using Comum;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using DSharpPlus.Interactivity.Extensions;
using Mensagens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modulos
{
    public partial class ComandosModulo
    {
        [RequireRoles(RoleCheckMode.Any, "Master")]
        [Command("pericia-cad")]
        [Description("cadastra uma lista de novas perícias")]
        public async Task CadastrarPericia(CommandContext context, [Description("Nomes das pericias")] params string[] nomes)
        {
            try
            {
                await _periciaServico.Cadastrar(nomes);

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


        [Command("pericia-info")]
        [Description("Busca informações de todas as perícias cadastradas.")]
        public async Task ObterPericia(CommandContext context)
        {
            try
            {
                var retorno = await _periciaServico.ObterTodosInfo();

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
        [Command("pericia-edit")]
        [Description("edita as informações da perícia informada.")]
        public async Task EditarPericia(CommandContext context, [Description("Nome da perícia")] string nome, [Description("Novo nome da perícia")] string novoNome)
        {
            try
            {
                var retorno = await _periciaServico.Obter(nome);

                if (retorno == null)
                    throw new RegraException(MensagensCrud.NaoEncontrado);


                retorno.Nome = novoNome;

                await _periciaServico.Editar(retorno);
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
        [Command("pericia-del")]
        [Description("deleta uma péricia informada.")]
        public async Task ExcluirPericia(CommandContext context, [Description("Nome da perícia")] string nome)
        {
            try
            {
                var interactivity = context.Client.GetInteractivity();

                var mensagemConfirmacao = await context.Channel.SendMessageAsync(embed: _periciaServico.ConfirmarExclusao()).ConfigureAwait(false);

                await mensagemConfirmacao.CreateReactionAsync(DiscordEmoji.FromUnicode("👍")).ConfigureAwait(false);
                await mensagemConfirmacao.CreateReactionAsync(DiscordEmoji.FromUnicode("👎")).ConfigureAwait(false);

                var resultado = await interactivity.CollectReactionsAsync(mensagemConfirmacao, TimeSpan.FromSeconds(5)).ConfigureAwait(false);

                if (resultado.Select(o => o.Emoji.ToString()).Contains("👍") && resultado.SelectMany(o => o.Users.ToList()).Contains(context.User))
                {
                    await _periciaServico.Excluir(nome);
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
