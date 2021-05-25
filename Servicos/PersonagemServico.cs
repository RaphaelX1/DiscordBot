using Comum;
using DSharpPlus.Entities;
using DSharpPlus.Interactivity;
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
    public partial class PersonagemServico : BaseServico
    {
        public ArcanoRepositorio _arcanoRepositorio;
        public AtributoRepositorio _atributoRepositorio;
        public FormacaoRepositorio _formacaoRepositorio;
        public EfeitoRepositorio _efeitoRepositorio;
        public NacaoRepositorio _nacaoRepositorio;
        public PericiaRepositorio _periciaRepositorio;
        public PersonagemRepositorio _personagemRepositorio;
        public ReligiaoRepositorio _religiaoRepositorio;
        public VantagemRepositorio _vantagemRepositorio;

        public PersonagemServico(ArcanoRepositorio arcanoRepositorio, AtributoRepositorio atributoRepositorio, 
            FormacaoRepositorio formacaoRepositorio, NacaoRepositorio nacaoRepositorio, 
            PersonagemRepositorio personagemRepositorio, ReligiaoRepositorio religiaoRepositorio, 
            EfeitoRepositorio efeitoRepositorio, PericiaRepositorio periciaRepositorio, 
            VantagemRepositorio vantagemRepositorio)
        {
            _arcanoRepositorio = arcanoRepositorio;
            _atributoRepositorio = atributoRepositorio;
            _formacaoRepositorio = formacaoRepositorio;
            _efeitoRepositorio = efeitoRepositorio;
            _nacaoRepositorio = nacaoRepositorio;
            _personagemRepositorio = personagemRepositorio;
            _religiaoRepositorio = religiaoRepositorio;
            _periciaRepositorio = periciaRepositorio;
            _vantagemRepositorio = vantagemRepositorio; 
        }

        public async Task<DiscordEmbedBuilder> ObterInfo(string nome)
        {
            var personagem = await _personagemRepositorio.ObterCompleto(nome);

            if (personagem == null)
                return null;

            var tuplas = new List<Tuple<string, string>>();

            tuplas.Add(new Tuple<string, string>("Ferimentos:", $"Normais: {personagem.Ferimentos}, Dramáticos: {personagem.Ferimentos}"));
            tuplas.Add(new Tuple<string, string>("Regionalização:", $"Religião: {personagem.Religiao.Nome}, Nação: {personagem.Nacao.Nome}"));


            tuplas.Add(MontarFormacoes(personagem.Formacoes));

            tuplas.Add(MontarVantagens(personagem.Vantagens));

            if (personagem.Arcanos.Any())
                tuplas.Add(new Tuple<string, string>("Arcanos:", "Atuais"));

            tuplas.AddRange(MontarArcanos(personagem.Arcanos));

            return LayoutHandler
              .MontarInfo(personagem.Nome, $"Fortuna: {personagem.Fortuna}", "Descrição do Herói", tuplas, TipoInfo.Personagem, DiscordColor.Blue);
        }


        public async Task<List<Page>> ObterFullInfo(string nome)
        {
            var personagem = await _personagemRepositorio.ObterCompleto(nome);

            if (personagem == null)
                return null;

            var paginas = new List<Page>();
            var tuplasPagina1 = new List<Tuple<string, string>>();
            var tuplasPagina2 = new List<Tuple<string, string>>();

            tuplasPagina1.Add(new Tuple<string, string>("Ferimentos:", $"Normais: {personagem.Ferimentos}, Dramáticos: {personagem.Ferimentos}"));
            tuplasPagina1.Add(new Tuple<string, string>("Regionalização:", $"Religião: {personagem.Religiao.Nome}, Nação: {personagem.Nacao.Nome}"));

            tuplasPagina1.Add(MontarFormacoes(personagem.Formacoes));

            tuplasPagina1.Add(MontarVantagens(personagem.Vantagens));

            if (personagem.Arcanos.Any())
                tuplasPagina1.Add(new Tuple<string, string>("Arcanos:", "Atuais"));

            tuplasPagina1.AddRange(MontarArcanos(personagem.Arcanos));


            if (personagem.Atributos.Any())
                tuplasPagina2.Add(new Tuple<string, string>("Atributos", "Habilidades base do personagem:"));

            tuplasPagina2.AddRange(MontarAtributos(personagem.Atributos));

            if (personagem.Periciais.Any())
                tuplasPagina2.Add(new Tuple<string, string>("Períciais", "Capacidades associadas ao personagem:"));

            tuplasPagina2.AddRange(MontarPericias(personagem.Periciais));

            if (personagem.Efeitos.Any())
                tuplasPagina2.Add(new Tuple<string, string>("Efeitos", "Situações adversas associadas ao personagem (Bonus ou Ônus):"));

            tuplasPagina2.AddRange(MontarEfeitos(personagem.Efeitos));

            paginas.Add(LayoutHandler
              .MontarInfoPaginada(personagem.Nome, $"Fortuna: {personagem.Fortuna}", "Descrição do Herói", tuplasPagina1, TipoInfo.Personagem, DiscordColor.Blue));

            paginas.Add(LayoutHandler
             .MontarInfoPaginada(personagem.Nome, $"Status e Atribuições associadas ao personsagem", "Descrição do Herói", tuplasPagina2, TipoInfo.Personagem, DiscordColor.Blue));

            return paginas;
        }

        public async Task<PersonagemDTO> Obter(string nome)
        {
            return await _personagemRepositorio.Obter(nome);
        }

        public async Task Cadastrar(PersonagemDTO personagemDTO, string nacaoNome, string religiaoNome)
        {
            await VerificarExistente(personagemDTO.Nome);

            var nacao = await _nacaoRepositorio.Obter(nacaoNome);

            var religiao = await _religiaoRepositorio.Obter(religiaoNome);

            AssociarNacaoReligiao(nacao, religiao, personagemDTO);

            await _personagemRepositorio.Cadastrar(personagemDTO);
        }

        public async Task Editar(PersonagemDTO personagemDTO, string nacaoNome, string religiaoNome)
        {
            var nacao = await _nacaoRepositorio.Obter(nacaoNome);

            var religiao = await _religiaoRepositorio.Obter(religiaoNome);

            AssociarNacaoReligiao(nacao, religiao, personagemDTO);

            await _personagemRepositorio.Editar(personagemDTO);
        }

        public async Task Excluir(string nome)
        {
            await _personagemRepositorio.Excluir(nome);
        }

        public async Task VerificarExistente(string nome)
        {
            if (await _personagemRepositorio.VerificarExistente(nome))
            {
                throw new RegraException(MensagensCrud.JaExistente);
            };
        }

        public void AssociarNacaoReligiao(NacaoDTO nacaoDTO, ReligiaoDTO religiaoDTO, PersonagemDTO personagemDTO)
        {
            if (nacaoDTO == null)
                throw new RegraException(MensagensCrud.NacaoNaoEncontrada);

            if (religiaoDTO == null)
                throw new RegraException(MensagensCrud.ReligiaoNaoEncontrada);

            personagemDTO.ReligiaoId = religiaoDTO.Id;
            personagemDTO.NacaoId = nacaoDTO.Id;
        }

        public async Task AssociarFerimentos(PersonagemDTO personagemDTO, string quantidadeFerimentos, bool dramatico = false)
        {
            int quantidadeFerimentosConvertido;

            if (!int.TryParse(quantidadeFerimentos, out quantidadeFerimentosConvertido))
                throw new RegraException(MensagensCrud.ConverterTextoEmNumero);

            if (dramatico)
                personagemDTO.FerimentosDramaticos += quantidadeFerimentosConvertido;
            else
                personagemDTO.Ferimentos += quantidadeFerimentosConvertido;

            await _personagemRepositorio.Editar(personagemDTO);
        }

        public async Task AssociarFortuna(PersonagemDTO personagemDTO, string fortuna)
        {
            personagemDTO.Fortuna += fortuna.ParseIfValid();

            await _personagemRepositorio.Editar(personagemDTO);
        }

    }
}
