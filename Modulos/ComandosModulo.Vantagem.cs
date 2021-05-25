using Comum;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using DSharpPlus.Interactivity.Extensions;
using DTO;
using Mensagens;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Modulos
{
    public partial class ComandosModulo
    {
        [RequireRoles(RoleCheckMode.Any, "Master")]
        [Command("vantagem-cad")]
        [Description("cadastra uma nova vantagem")]
        public async Task CadastrarVantagem(CommandContext context, [Description("Nome da vantagem")] string nome, [Description("Informações sobre a vantagem")] string descricao)
        {
            try
            {
                await _vantagemServico.Cadastrar(new VantagemDTO()
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

        [Command("vantagem-info")]
        [Description("Busca informações de uma vantagem.")]
        public async Task ObterVantagem(CommandContext context, [Description("Nome da vantagem")] string nome)
        {
            try
            {
                var retorno = await _vantagemServico.ObterInfo(nome);

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

        [Command("vantagem-info")]
        [Description("Busca informações de todas as vantagens cadastradas.")]
        public async Task ObterVantagem(CommandContext context)
        {
            try
            {
                var retorno = await _vantagemServico.ObterTodosInfo();

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
        [Command("vantagem-edit")]
        [Description("edita as informações da vantagem informada.")]
        public async Task EditarVantagem(CommandContext context, [Description("Nome da vantagem")] string nome, [Description("Novo nome da vantagem")] string novoNome, [Description("Nova descrição da vantagem")] string descricao)
        {
            try
            {
                var retorno = await _vantagemServico.Obter(nome);

                if (retorno == null)
                    throw new RegraException(MensagensCrud.NaoEncontrado);


                retorno.Descricao = descricao;
                retorno.Nome = novoNome;

                await _vantagemServico.Editar(retorno);
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
        [Command("vantagem-del")]
        [Description("deleta uma vantagem informada.")]
        public async Task ExcluirVantagem(CommandContext context, [Description("Nome da vantagem")] string nome)
        {
            try
            {
                var interactivity = context.Client.GetInteractivity();

                var mensagemConfirmacao = await context.Channel.SendMessageAsync(embed: _vantagemServico.ConfirmarExclusao()).ConfigureAwait(false);

                await mensagemConfirmacao.CreateReactionAsync(DiscordEmoji.FromUnicode("👍")).ConfigureAwait(false);
                await mensagemConfirmacao.CreateReactionAsync(DiscordEmoji.FromUnicode("👎")).ConfigureAwait(false);

                var resultado = await interactivity.CollectReactionsAsync(mensagemConfirmacao, TimeSpan.FromSeconds(5)).ConfigureAwait(false);

                if (resultado.Select(o => o.Emoji.ToString()).Contains("👍") && resultado.SelectMany(o => o.Users.ToList()).Contains(context.User))
                {
                    await _vantagemServico.Excluir(nome);
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
