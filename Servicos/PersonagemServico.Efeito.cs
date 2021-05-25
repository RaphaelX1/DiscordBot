using Comum;
using DSharpPlus.Entities;
using DTO;
using Enumeradores;
using Mensagens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Servicos
{
    public partial class PersonagemServico
    {
        public async Task<DiscordEmbedBuilder> ObterEfeitosInfo(string nome)
        {
            var personagem = await _personagemRepositorio.ObterEfeitos(nome);

            if (personagem == null)
                return null;

            var tuplas = new List<Tuple<string, string>>();

            tuplas.AddRange(MontarEfeitos(personagem.Efeitos));

            if (!tuplas.Any())
                return null;

            return LayoutHandler
              .MontarInfo(personagem.Nome, "Efeitos", "Efeitos aplicados ao Herói", tuplas, TipoInfo.Efeito, DiscordColor.Orange);
        }

        public List<Tuple<string, string>> MontarEfeitos(IEnumerable<PersonagemEfeitoDTO> personagemEfeitosDTO)
        {
            var tuplas = new List<Tuple<string, string>>();

            foreach (var efeito in personagemEfeitosDTO)
            {
                var icones = new List<string>();

                for (int i = 0; i < efeito.Valor; i++)
                {
                    icones.Add(MensagensEmoji.DiamondOrange);
                }

                tuplas.Add(new Tuple<string, string>(efeito.Efeito.Nome, string.Join(" ", icones.Select(o => o))));
            }

            return tuplas;
        }

        public async Task CadastrarEfeito(string valor, string nomePersonagem, string nomeEfeito)
        {
            var personagemEfeitoIds = await VerificarEfeitoExistente(nomePersonagem, nomeEfeito);

            var personagemEfeito = new PersonagemEfeitoDTO() 
            { 
                PersonagemId = personagemEfeitoIds.Item1,
                EfeitoId = personagemEfeitoIds.Item2,
                Valor = valor.ParseIfValid()
            };

            await _personagemRepositorio.CadastrarEfeito(personagemEfeito);
        }

        public async Task EditarEfeito(string valor, string nomePersonagem, string nomeEfeito)
        {
            var personagemEfeito = await VerificarEfeitoEdicao(nomePersonagem, nomeEfeito);

            await _personagemRepositorio.EditarEfeito(personagemEfeito.PersonagemId, personagemEfeito.EfeitoId, valor.ParseIfValid());
        }


        public async Task ExcluirEfeito(string nomePersonagem, string nomeEfeito)
        {
            var personagem = await _personagemRepositorio.ObterEfeitos(nomePersonagem);

            var efeito = await _efeitoRepositorio.Obter(nomeEfeito);

            ValidarPersonagemEfeitoDados(personagem, efeito);

            var personagemEfeito = personagem.Efeitos.FirstOrDefault(o => o.EfeitoId == efeito.Id);

            if (personagemEfeito == null)
                throw new RegraException(MensagensCrud.NaoEncontrado);


            await _personagemRepositorio.ExcluirEfeito(personagem.Id, efeito.Id);
        }

        public async Task<Tuple<Guid, Guid>> VerificarEfeitoExistente(string nomePersonagem, string nomeEfeito)
        {
            var personagem = await _personagemRepositorio.ObterEfeitos(nomePersonagem);

            var efeito = await _efeitoRepositorio.Obter(nomeEfeito);

            ValidarPersonagemEfeitoDados(personagem, efeito);

            var personagemEfeito = personagem.Efeitos.FirstOrDefault(o => o.EfeitoId == efeito.Id);

            if (personagemEfeito != null)
                throw new RegraException(MensagensCrud.JaExistente);

            return new Tuple<Guid, Guid>(personagem.Id, efeito.Id);
        }

        public async Task<PersonagemEfeitoDTO> VerificarEfeitoEdicao(string nomePersonagem, string nomeEfeito)
        {
            var personagem = await _personagemRepositorio.ObterEfeitos(nomePersonagem);

            var efeito = await _efeitoRepositorio.Obter(nomeEfeito);

            ValidarPersonagemEfeitoDados(personagem, efeito);

            var personagemEfeito = personagem.Efeitos.FirstOrDefault(o => o.EfeitoId == efeito.Id);

            if (personagemEfeito == null)
                throw new RegraException(MensagensCrud.NaoEncontrado);

            return personagemEfeito;
        }

        public void ValidarPersonagemEfeitoDados(PersonagemDTO personagemDTO, EfeitoDTO efeitoDTO)
        {
            if (personagemDTO == null)
                throw new RegraException(MensagensCrud.PersonagemNaoEncontrada);

            if (efeitoDTO == null)
                throw new RegraException(MensagensCrud.EfeitoNaoEncontrado);
        }
    }
}
