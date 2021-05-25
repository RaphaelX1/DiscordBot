using Comum;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using DSharpPlus.Interactivity.Extensions;
using DTO;
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
        [Command("formacao-cad")]
        [Description("cadastra uma nova formação")]
        public async Task CadastrarFormacao(CommandContext context, [Description("Nome da formação")] string nome, [Description("Informações sobre a formação")] string descricao)
        {
            try
            {
                await _formacaoServico.Cadastrar(new FormacaoDTO()
                {
                    Nome = nome,
                    Descricao = descricao,

                });

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

        [Command("formacao-info")]
        [Description("Busca informações de uma formação.")]
        public async Task ObterFormacao(CommandContext context, [Description("Nome da formação")] string nome)
        {
            try
            {
                var retorno = await _formacaoServico.ObterInfo(nome);

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

        [Command("formacao-info")]
        [Description("Busca informações de todas as formações cadastradas.")]
        public async Task ObterFormacao(CommandContext context)
        {
            try
            {
                var retorno = await _formacaoServico.ObterTodosInfo();

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
        [Command("formacao-edit")]
        [Description("edita as informações da formação informada.")]
        public async Task EditarFormacao(CommandContext context, [Description("Nome da formação")] string nome, [Description("Novo nome da formação")] string novoNome, [Description("Nova descrição da formação")] string descricao)
        {
            try
            {
                var retorno = await _formacaoServico.Obter(nome);

                if (retorno == null)
                    throw new RegraException(MensagensCrud.NaoEncontrado);


                retorno.Descricao = descricao;
                retorno.Nome = novoNome;

                await _formacaoServico.Editar(retorno);
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
        [Command("formacao-del")]
        [Description("deleta uma formação informada.")]
        public async Task ExcluirFormacao(CommandContext context, [Description("Nome da formação")] string nome)
        {
            try
            {
                var interactivity = context.Client.GetInteractivity();

                var mensagemConfirmacao = await context.Channel.SendMessageAsync(embed: _formacaoServico.ConfirmarExclusao()).ConfigureAwait(false);

                await mensagemConfirmacao.CreateReactionAsync(DiscordEmoji.FromUnicode("👍")).ConfigureAwait(false);
                await mensagemConfirmacao.CreateReactionAsync(DiscordEmoji.FromUnicode("👎")).ConfigureAwait(false);

                var resultado = await interactivity.CollectReactionsAsync(mensagemConfirmacao, TimeSpan.FromSeconds(5)).ConfigureAwait(false);

                if (resultado.Select(o => o.Emoji.ToString()).Contains("👍") && resultado.SelectMany(o => o.Users.ToList()).Contains(context.User))
                {
                    await _formacaoServico.Excluir(nome);
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
