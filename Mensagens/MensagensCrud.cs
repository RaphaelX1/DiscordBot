using System;
using System.Collections.Generic;
using System.Text;

namespace Mensagens
{
    public class MensagensCrud
    {
        public static string Sucesso => $"Feito. {MensagensEmoji.PeaceLove}";

        public static string Falha => $"Ixi, algo deu errado aqui. {MensagensEmoji.FrustratedFace}";

        public static string NaoEncontrado => $"Não achei nada com essa informação aqui. {MensagensEmoji.FrustratedFace}";

        public static string ConfirmacaoExclusao => $"Excluir também vai excluir os itens associados, tem certeza? {MensagensEmoji.Warning}";

        public static string Abortado => $"Top, abortado então. {MensagensEmoji.Top}";

        public static string JaExistente => $"Já existe um registro com essas características. {MensagensEmoji.Top}";

        public static string ReligiaoNaoEncontrada => $"Não encontrei a religião informada, confere a ortografia e tente outra vez. {MensagensEmoji.FrustratedFace}";

        public static string NacaoNaoEncontrada => $"Não encontrei a nação informada, confere a ortografia e tente outra vez. {MensagensEmoji.FrustratedFace}";

        public static string PersonagemNaoEncontrada => $"Não encontrei a personagem informada, confere a ortografia e tente outra vez. {MensagensEmoji.FrustratedFace}";

        public static string AtributoNaoEncontrado => $"Não encontrei o atributo informado, confere a ortografia e tente outra vez. {MensagensEmoji.FrustratedFace}";

        public static string EfeitoNaoEncontrado => $"Não encontrei o Efeito informado, confere a ortografia e tente outra vez. {MensagensEmoji.FrustratedFace}";

        public static string PericiaNaoEncontrada => $"Não encontrei a perícia informada, confere a ortografia e tente outra vez. {MensagensEmoji.FrustratedFace}";

        public static string FormacaoNaoEncontrada => $"Não encontrei a formação informada, confere a ortografia e tente outra vez. {MensagensEmoji.FrustratedFace}";

        public static string ArcanoNaoEncontrado => $"Não encontrei o Arcano informado, confere a ortografia e tente outra vez. {MensagensEmoji.FrustratedFace}";

        public static string PersonagemAtributoNaoEncontrado => $"Não encontrei o atributo associado ao personagem informado, confere a ortografia e tente outra vez. {MensagensEmoji.FrustratedFace}";

        public static string ConverterTextoEmNumero => $"Não rola de converter texto pra número, coloca um valor válido, valeu. {MensagensEmoji.PeaceLove}";

        public static string SeguraAsPontaAi => $"Segura as pontas aí mermão {MensagensEmoji.Disappointed}{MensagensEmoji.RiseHand}";

        public static string ProntoParça => $"Pronto meu parça {MensagensEmoji.Flushed}{MensagensEmoji.Top}";

    }
}
