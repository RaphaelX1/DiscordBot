using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using Mensagens;
using Servicos;
using System.Threading.Tasks;

namespace Modulos
{
    public partial class ComandosModulo : BaseCommandModule
    {
        public ArcanoServico _arcanoServico;
        public AtributoServico _atributoServico;
        public DadoServico _dadoServico;
        public EfeitoServico _efeitoServico;
        public FormacaoServico _formacaoServico;
        public NacaoServico _nacaoServico;
        public PericiaServico _periciaServico;
        public PersonagemServico _personagemServico;
        public ReligiaoServico _religiaoServico;
        public VantagemServico _vantagemServico;

        public ComandosModulo(ArcanoServico arcanoServico, DadoServico dadoServico, 
            AtributoServico atributoServico, FormacaoServico formacaoServico,
            NacaoServico nacaoServico, PericiaServico periciaServico, 
            PersonagemServico personagemServico, ReligiaoServico religiaoServico, 
            EfeitoServico efeitoServico, VantagemServico vantagemServico)
        {
            _arcanoServico = arcanoServico;
            _atributoServico = atributoServico;
            _dadoServico = dadoServico;
            _efeitoServico = efeitoServico;
            _formacaoServico = formacaoServico;
            _nacaoServico = nacaoServico;
            _periciaServico = periciaServico;
            _personagemServico = personagemServico;
            _religiaoServico = religiaoServico;
            _vantagemServico = vantagemServico;
        }

        [Command("bo")]
        [Description("Verifica se o bot está em atividade")]
        public async Task Bo(CommandContext context) 
        {
            await context.Channel.SendMessageAsync(MensagensInterativas.ObterAleatoriamente()).ConfigureAwait(false);
        }
    }
}
