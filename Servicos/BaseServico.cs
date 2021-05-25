using DSharpPlus.Entities;


namespace Servicos
{
    public abstract class BaseServico
    {
        public DiscordEmbedBuilder ConfirmarExclusao()
        {
            return LayoutHandler.MontarInfoConfirmacaoExclusao();
        }
    }
}
