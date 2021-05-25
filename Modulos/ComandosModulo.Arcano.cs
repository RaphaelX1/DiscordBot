using Comum;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using DSharpPlus.Interactivity.Extensions;
using DTO;
using Enumeradores;
using Mensagens;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Modulos
{

    public partial class ComandosModulo
    {
        #region Virtude

        [RequireRoles(RoleCheckMode.Any, "Master")]
        [Command("virtude-cad")]
        [Description("cadastra uma nova virtude")]
        public async Task CadastrarVirtude(CommandContext context, [Description("Nome da virtude")] string nome, [Description("Informações sobre a virtude")] string descricao)
        {
            try
            {
                await _arcanoServico.Cadastrar(new ArcanoDTO()
                {
                    Nome = nome,
                    Descricao = descricao,
                    TipoArcano = TipoArcano.Virtude

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

        [Command("virtude-info")]
        [Description("Busca informações de uma virtude.")]
        public async Task ObterVirtude(CommandContext context, [Description("Nome da virtude")] string nome)
        {
            try
            {
                var retorno = await _arcanoServico.ObterInfo(nome, TipoArcano.Virtude);

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
        [Command("virtude-edit")]
        [Description("edita as informações da virtude informada.")]
        public async Task EditarVirtude(CommandContext context, [Description("Nome da virtude")] string nome, [Description("Novo nome da virtude")] string novoNome, [Description("Nova descrição da virtude")] string descricao)
        {
            try
            {
                var retorno = await _arcanoServico.Obter(nome, TipoArcano.Hubris);

                if (retorno == null)
                    throw new RegraException(MensagensCrud.NaoEncontrado);


                retorno.Descricao = descricao;
                retorno.Nome = novoNome;

                await _arcanoServico.Editar(retorno);
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
        [Command("virtude-del")]
        [Description("deleta a virtude informada.")]
        public async Task ExcluirVirtude(CommandContext context, [Description("Nome da virtude")] string nome)
        {
            try
            {
                var interactivity = context.Client.GetInteractivity();

                var mensagemConfirmacao = await context.Channel.SendMessageAsync(embed: _arcanoServico.ConfirmarExclusao()).ConfigureAwait(false);

                await mensagemConfirmacao.CreateReactionAsync(DiscordEmoji.FromUnicode("👍")).ConfigureAwait(false);
                await mensagemConfirmacao.CreateReactionAsync(DiscordEmoji.FromUnicode("👎")).ConfigureAwait(false);

                var resultado = await interactivity.CollectReactionsAsync(mensagemConfirmacao, TimeSpan.FromSeconds(5)).ConfigureAwait(false);

                if (resultado.Select(o => o.Emoji.ToString()).Contains("👍") && resultado.SelectMany(o => o.Users.ToList()).Contains(context.User))
                {
                    await _arcanoServico.Excluir(nome, TipoArcano.Virtude);
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

        #endregion

        #region Hubris

        [RequireRoles(RoleCheckMode.Any, "Master")]
        [Command("hubris-cad")]
        [Description("cadastra uma nova húbis")]
        public async Task CadastrarHubris(CommandContext context, [Description("Nome da húbris")] string nome, [Description("Informações sobre a húbris")] string descricao)
        {
            try
            {
                await _arcanoServico.Cadastrar(new ArcanoDTO()
                {
                    Nome = nome,
                    Descricao = descricao,
                    TipoArcano = TipoArcano.Hubris

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

        [Command("hubris-info")]
        [Description("Busca informações de uma húbris.")]
        public async Task ObterHubris(CommandContext context, [Description("Nome da húbris")] string nome)
        {
            try
            {
                var retorno = await _arcanoServico.ObterInfo(nome, TipoArcano.Hubris);

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
        [Command("hubris-edit")]
        [Description("edita as informações da húbris informada.")]
        public async Task EditarHubris(CommandContext context, [Description("Nome da húbris")] string nome, [Description("Novo nome da húbris")] string novoNome, [Description("Nova descrição da húbris")] string descricao)
        {
            try
            {
                var retorno = await _arcanoServico.Obter(nome, TipoArcano.Hubris);

                if (retorno == null)
                    throw new RegraException(MensagensCrud.NaoEncontrado);


                retorno.Descricao = descricao;
                retorno.Nome = novoNome;

                await _arcanoServico.Editar(retorno);
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
        [Command("hubris-del")]
        [Description("deleta a virtude informada.")]
        public async Task ExcluirHubris(CommandContext context, [Description("Nome da hubris")] string nome)
        {
            try
            {
                var interactivity = context.Client.GetInteractivity();

                var mensagemConfirmacao = await context.Channel.SendMessageAsync(embed: _arcanoServico.ConfirmarExclusao()).ConfigureAwait(false);

                await mensagemConfirmacao.CreateReactionAsync(DiscordEmoji.FromUnicode("👍")).ConfigureAwait(false);
                await mensagemConfirmacao.CreateReactionAsync(DiscordEmoji.FromUnicode("👎")).ConfigureAwait(false);

                var resultado = await interactivity.CollectReactionsAsync(mensagemConfirmacao, TimeSpan.FromSeconds(5)).ConfigureAwait(false);

                if (resultado.Select(o => o.Emoji.ToString()).Contains("👍") && resultado.SelectMany(o => o.Users.ToList()).Contains(context.User))
                {
                    await _arcanoServico.Excluir(nome, TipoArcano.Hubris);
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

        #endregion


    }
}
