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
        public async Task<DiscordEmbedBuilder> ObterAtributosInfo(string nome)
        {
            var personagem = await _personagemRepositorio.ObterAtributos(nome);

            if (personagem == null)
                return null;

            var tuplas = new List<Tuple<string, string>>();

            tuplas.AddRange(MontarAtributos(personagem.Atributos));

            if (!tuplas.Any())
                return null;

            return LayoutHandler
              .MontarInfo(personagem.Nome, "Atributos", "Habilidades do Herói", tuplas, TipoInfo.Atributo, DiscordColor.Chartreuse);
        }

        public List<Tuple<string, string>> MontarAtributos(IEnumerable<PersonagemAtributoDTO> personagemAtributosDTO)
        {
            var tuplas = new List<Tuple<string, string>>();

            foreach (var atributo in personagemAtributosDTO)
            {
                var icones = new List<string>();

                for (int i = 0; i < atributo.Valor; i++)
                {
                    icones.Add(MensagensEmoji.Star);
                }

                tuplas.Add(new Tuple<string, string>(atributo.Atributo.Nome, string.Join(" ", icones.Select(o => o))));
            }

            return tuplas;
        }

        public async Task CadastrarAtributo(string valor, string nomePersonagem, string nomeAtributo)
        {
            var personagemAtributoIds = await VerificarAtributoExistente(nomePersonagem, nomeAtributo);

            var personagemAtributo = new PersonagemAtributoDTO() 
            { 
                PersonagemId = personagemAtributoIds.Item1,
                AtributoId = personagemAtributoIds.Item2,
                Valor = valor.ParseIfValid()
            };

            await _personagemRepositorio.CadastrarAtributo(personagemAtributo);
        }

        public async Task EditarAtributo(string valor, string nomePersonagem, string nomeAtributo)
        {
            var personagemAtributo = await VerificarAtributoEdicao(nomePersonagem, nomeAtributo);

            await _personagemRepositorio.EditarAtributo(personagemAtributo.PersonagemId, personagemAtributo.AtributoId, valor.ParseIfValid());
        }


        public async Task ExcluirAtributo(string nomePersonagem, string nomeAtributo)
        {
            var personagem = await _personagemRepositorio.ObterAtributos(nomePersonagem);

            var atributo = await _atributoRepositorio.Obter(nomeAtributo);

            ValidarPersonagemAtributoDados(personagem, atributo);

            var personagemAtributo = personagem.Atributos.FirstOrDefault(o => o.AtributoId == atributo.Id);

            if (personagemAtributo == null)
                throw new RegraException(MensagensCrud.NaoEncontrado);

            await _personagemRepositorio.ExcluirAtributo(personagem.Id, atributo.Id);
        }

        public async Task<Tuple<Guid, Guid>> VerificarAtributoExistente(string nomePersonagem, string nomeAtributo)
        {
            var personagem = await _personagemRepositorio.ObterAtributos(nomePersonagem);

            var atributo = await _atributoRepositorio.Obter(nomeAtributo);

            ValidarPersonagemAtributoDados(personagem, atributo);

            var personagemAtributo = personagem.Atributos.FirstOrDefault(o => o.AtributoId == atributo.Id);

            if (personagemAtributo != null)
                throw new RegraException(MensagensCrud.JaExistente);

            return new Tuple<Guid, Guid>(personagem.Id, atributo.Id);
        }

        public async Task<PersonagemAtributoDTO> VerificarAtributoEdicao(string nomePersonagem, string nomeAtributo)
        {
            var personagem = await _personagemRepositorio.ObterAtributos(nomePersonagem);

            var atributo = await _atributoRepositorio.Obter(nomeAtributo);

            ValidarPersonagemAtributoDados(personagem, atributo);

            var personagemAtributo = personagem.Atributos.FirstOrDefault(o => o.AtributoId == atributo.Id);

            if (personagemAtributo == null)
                throw new RegraException(MensagensCrud.NaoEncontrado);

            return personagemAtributo;

        }

        public void ValidarPersonagemAtributoDados(PersonagemDTO personagemDTO, AtributoDTO atributoDTO)
        {
            if (personagemDTO == null)
                throw new RegraException(MensagensCrud.PersonagemNaoEncontrada);

            if (atributoDTO == null)
                throw new RegraException(MensagensCrud.AtributoNaoEncontrado);
        }
    }
}
