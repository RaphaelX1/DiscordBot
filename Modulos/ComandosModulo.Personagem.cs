using Comum;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using DSharpPlus.Interactivity.Extensions;
using DTO;
using Mensagens;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modulos
{
    public partial class ComandosModulo
    {
        [RequireRoles(RoleCheckMode.Any, "Master")]
        [Command("personagem-cad")]
        [Description("cadastra uma nova personagem")]
        public async Task CadastrarVantagem(CommandContext context, [Description("Nome da personagem")] string nome, [Description("Informações sobre a personagem")] string descricao,
            [Description("Religião da personagem")] string religiao, [Description("Nacao da personagem")] string nacao)
        {
            try
            {
                await _personagemServico.Cadastrar(new PersonagemDTO()
                {
                    Nome = nome,
                    Bio = descricao,
                    Ferimentos = 0,
                    FerimentosDramaticos = 0,
                    Fortuna = 0
                }, nacao, religiao);

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

        [Command("personagem-info")]
        [Description("Busca informações de uma personagem.")]
        public async Task ObterPersonagem(CommandContext context, [Description("Nome da personagem")] string nome)
        {
            try
            {
                var retorno = await _personagemServico.ObterInfo(nome);

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

        [Command("personagem-full-info")]
        [Description("Busca todas as informações de uma personagem.")]
        public async Task ObterPersonagemFull(CommandContext context, [Description("Nome da personagem")] string nome)
        {
            try
            {
                await context.Channel.SendMessageAsync(MensagensCrud.SeguraAsPontaAi).ConfigureAwait(false);

                var retorno = await _personagemServico.ObterFullInfo(nome);

                if (retorno == null)
                    throw new RegraException(MensagensCrud.NaoEncontrado);

                await context.Channel.SendMessageAsync(MensagensCrud.ProntoParça).ConfigureAwait(false);
                await context.Channel.SendPaginatedMessageAsync(context.User, retorno).ConfigureAwait(false);

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
        [Command("personagem-edit")]
        [Description("edita as informações da personagem informada.")]
        public async Task EditarPersonagem(CommandContext context, [Description("Nome da personagem")] string nome, [Description("Novo nome da personagem")] string novoNome, [Description("Nova descrição da personagem")] string descricao,
            [Description("Religião da personagem")] string religiao, [Description("Nacao da personagem")] string nacao)
        {
            try
            {
                var retorno = await _personagemServico.Obter(nome);

                if (retorno == null)
                    throw new RegraException(MensagensCrud.NaoEncontrado);


                retorno.Bio = descricao;
                retorno.Nome = novoNome;

                await _personagemServico.Editar(retorno, nacao, religiao);
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
        [Command("personagem-fortuna-edit")]
        [Description("edita as informações da fortuna da personagem (tanto + quanto -).")]
        public async Task EditarFortuna(CommandContext context, [Description("Nome da personagem")] string nome, [Description("quantidade de fortuna a associar")] string fortuna)
        {
            try
            {
                var retorno = await _personagemServico.Obter(nome);

                if (retorno == null)
                    throw new RegraException(MensagensCrud.NaoEncontrado);

                await _personagemServico.AssociarFortuna(retorno, fortuna);
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
        [Command("personagem-ferimento-edit")]
        [Description("edita as informações de ferimento da personagem (tanto + quanto -).")]
        public async Task EditarFerimento(CommandContext context, [Description("Nome da personagem")] string nome, [Description("ferimento a associar ao personagem")] string ferimento)
        {
            try
            {
                var retorno = await _personagemServico.Obter(nome);

                if (retorno == null)
                    throw new RegraException(MensagensCrud.NaoEncontrado);

                await _personagemServico.AssociarFerimentos(retorno, ferimento);
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
        [Command("personagem-ferimento-dramatico-edit")]
        [Description("edita as informações de ferimento da personagem (tanto + quanto -).")]
        public async Task EditarFerimentoDramatico(CommandContext context, [Description("Nome da personagem")] string nome, [Description("ferimento dramático a associar ao personagem")] string ferimentoDramatico)
        {
            try
            {
                var retorno = await _personagemServico.Obter(nome);

                if (retorno == null)
                    throw new RegraException(MensagensCrud.NaoEncontrado);

                await _personagemServico.AssociarFerimentos(retorno, ferimentoDramatico, true);
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
        [Command("personagem-del")]
        [Description("deleta uma personagem informada.")]
        public async Task ExcluirPersonagem(CommandContext context, [Description("Nome da personagem")] string nome)
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
                    await _personagemServico.Excluir(nome);
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
