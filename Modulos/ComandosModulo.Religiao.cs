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
        [Command("religiao-cad")]
        [Description("cadastra uma nova religião")]
        public async Task CadastrarReligiao(CommandContext context, [Description("Nome da religião")] string nome, [Description("Informações sobre a religião")] string descricao)
        {
            try
            {
                await _religiaoServico.Cadastrar(new ReligiaoDTO()
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

        [Command("religiao-info")]
        [Description("Busca informações de uma religião.")]
        public async Task ObterReligiao(CommandContext context, [Description("Nome da religião")] string nome)
        {
            try
            {
                var retorno = await _religiaoServico.ObterInfo(nome);

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

        [Command("religiao-info")]
        [Description("Busca informações de todas as religiões cadastradas.")]
        public async Task ObterReligiao(CommandContext context)
        {
            try
            {
                var retorno = await _religiaoServico.ObterTodosInfo();

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

        [Command("religiao-fieis-info")]
        [Description("Busca informações de todos os personagem cadastrados associados a religião.")]
        public async Task ObterFieis(CommandContext context, [Description("Nome da religião")] string nome)
        {
            try
            {
                var retorno = await _religiaoServico.ObterFieisInfo(nome);

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
        [Command("religiao-edit")]
        [Description("edita as informações da religião informada.")]
        public async Task EditarReligiao(CommandContext context, [Description("Nome da religião")] string nome, [Description("Novo nome da religião")] string novoNome, [Description("Nova descrição da religião")] string descricao)
        {
            try
            {
                var retorno = await _religiaoServico.Obter(nome);

                if (retorno == null)
                    throw new RegraException(MensagensCrud.NaoEncontrado);


                retorno.Descricao = descricao;
                retorno.Nome = novoNome;

                await _religiaoServico.Editar(retorno);
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
        [Command("religiao-del")]
        [Description("deleta uma religião informada.")]
        public async Task ExcluirReligiao(CommandContext context, [Description("Nome da religião")] string nome)
        {
            try
            {
                var interactivity = context.Client.GetInteractivity();

                var mensagemConfirmacao = await context.Channel.SendMessageAsync(embed: _religiaoServico.ConfirmarExclusao()).ConfigureAwait(false);

                await mensagemConfirmacao.CreateReactionAsync(DiscordEmoji.FromUnicode("👍")).ConfigureAwait(false);
                await mensagemConfirmacao.CreateReactionAsync(DiscordEmoji.FromUnicode("👎")).ConfigureAwait(false);

                var resultado = await interactivity.CollectReactionsAsync(mensagemConfirmacao, TimeSpan.FromSeconds(5)).ConfigureAwait(false);

                if (resultado.Select(o => o.Emoji.ToString()).Contains("👍") && resultado.SelectMany(o => o.Users.ToList()).Contains(context.User))
                {
                    await _religiaoServico.Excluir(nome);
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
