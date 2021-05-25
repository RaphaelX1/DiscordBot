using Comum;
using DSharpPlus.Entities;
using DTO;
using Enumeradores;
using Mensagens;
using Repositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicos
{
    public partial class PersonagemServico
    {
        public async Task<DiscordEmbedBuilder> ObterFormacoessInfo(string nome)
        {
            var personagem = await _personagemRepositorio.ObterFormacoes(nome);

            if (personagem == null)
                return null;

            var tuplas = new List<Tuple<string, string>>();

            tuplas.Add(MontarFormacoes(personagem.Formacoes));

            if (!tuplas.Any())
                return null;

            return LayoutHandler
              .MontarInfo(personagem.Nome, "Atuais", "Formações do Herói", tuplas, TipoInfo.Formacao, DiscordColor.Chartreuse);
        }

        public Tuple<string, string> MontarFormacoes(IEnumerable<PersonagemFormacaoDTO> personagemFormacoesDTO)
        {
            return new Tuple<string, string>("Formações: ", string.Join(", ", personagemFormacoesDTO.Select(o => o.Formacao.Nome)));
        }

        public async Task CadastrarFormacao(string nomePersonagem, string nomeFormacao)
        {
            var personagemFormacaoIds = await VerificarFormacaoExistente(nomePersonagem, nomeFormacao);

            var personagemFormacao = new PersonagemFormacaoDTO() 
            { 
                PersonagemId = personagemFormacaoIds.Item1,
                FormacaoId = personagemFormacaoIds.Item2
            };

            await _personagemRepositorio.CadastrarFormacao(personagemFormacao);
        }

        public async Task ExcluirFormacao(string nomePersonagem, string nomeFormacao)
        {
            var personagem = await _personagemRepositorio.ObterFormacoes(nomePersonagem);

            var formacao = await _formacaoRepositorio.Obter(nomeFormacao);

            ValidarPersonagemFormacaoDados(personagem, formacao);

            var personagemFormacao = personagem.Formacoes.FirstOrDefault(o => o.FormacaoId == formacao.Id);

            if (personagemFormacao == null)
                throw new RegraException(MensagensCrud.NaoEncontrado);

            await _personagemRepositorio.ExcluirAtributo(personagem.Id, formacao.Id);
        }

        public async Task<Tuple<Guid, Guid>> VerificarFormacaoExistente(string nomePersonagem, string nomeFormacao)
        {
            var personagem = await _personagemRepositorio.ObterFormacoes(nomePersonagem);

            var formacao = await _formacaoRepositorio.Obter(nomeFormacao);

            ValidarPersonagemFormacaoDados(personagem, formacao);

            var personagemFormacao = personagem.Formacoes.FirstOrDefault(o => o.FormacaoId == formacao.Id);

            if (personagemFormacao != null)
                throw new RegraException(MensagensCrud.JaExistente);

            return new Tuple<Guid, Guid>(personagem.Id, formacao.Id);
        }

      

        public void ValidarPersonagemFormacaoDados(PersonagemDTO personagemDTO, FormacaoDTO formacaoDTO)
        {
            if (personagemDTO == null)
                throw new RegraException(MensagensCrud.PersonagemNaoEncontrada);

            if (formacaoDTO == null)
                throw new RegraException(MensagensCrud.FormacaoNaoEncontrada);
        }
    }
}
