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
        [Command("efeito-cad")]
        [Description("cadastra um novo efeito")]
        public async Task CadastrarEfeito(CommandContext context, [Description("Nome do efeito")] string nome, [Description("Informações sobre o efeito")] string descricao)
        {
            try
            {
                await _efeitoServico.Cadastrar(new EfeitoDTO()
                {
                    Nome = nome,
                    Descricao = descricao
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


        [Command("efeito-info")]
        [Description("Busca informações de todas os efeitos cadastrados.")]
        public async Task ObterEfeito(CommandContext context)
        {
            try
            {
                var retorno = await _efeitoServico.ObterTodosInfo();

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
        [Command("efeito-edit")]
        [Description("edita as informações de um efeito informado.")]
        public async Task EditarEfeito(CommandContext context, [Description("Nome do efeito")] string nome, [Description("Novo nome do efeito")] string novoNome, [Description("Nova descricao do efeito")] string novaDescricao)
        {
            try
            {
                var retorno = await _efeitoServico.Obter(nome);

                if (retorno == null)
                    throw new RegraException(MensagensCrud.NaoEncontrado);


                retorno.Nome = novoNome;
                retorno.Descricao = novaDescricao;

                await _efeitoServico.Editar(retorno);
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
        [Command("efeito-del")]
        [Description("deleta um efeito informado.")]
        public async Task ExcluirEfeito(CommandContext context, [Description("Nome do efeito")] string nome)
        {
            try
            {
                var interactivity = context.Client.GetInteractivity();

                var mensagemConfirmacao = await context.Channel.SendMessageAsync(embed: _efeitoServico.ConfirmarExclusao()).ConfigureAwait(false);

                await mensagemConfirmacao.CreateReactionAsync(DiscordEmoji.FromUnicode("👍")).ConfigureAwait(false);
                await mensagemConfirmacao.CreateReactionAsync(DiscordEmoji.FromUnicode("👎")).ConfigureAwait(false);

                var resultado = await interactivity.CollectReactionsAsync(mensagemConfirmacao, TimeSpan.FromSeconds(5)).ConfigureAwait(false);

                if (resultado.Select(o => o.Emoji.ToString()).Contains("👍") && resultado.SelectMany(o => o.Users.ToList()).Contains(context.User))
                {
                    await _efeitoServico.Excluir(nome);
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
