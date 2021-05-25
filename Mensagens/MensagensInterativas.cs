using System;
using System.Collections.Generic;
using System.Text;

namespace Mensagens
{
    public static class MensagensInterativas
    {
        public static string BotLoko => $"{MensagensEmoji.ClowFace} ÅŤÅQŮĘ ĐØ§ BØT ĽØĶØ {MensagensEmoji.ClowFace}! Ta-_Em_-CHoK kk {MensagensEmoji.QuestionWord}{MensagensEmoji.QuestionWord}";

        public static string Dollar => $"Putz, mais de cinco conto o dólar bicho. {MensagensEmoji.FrustratedFace}{MensagensEmoji.MoneyFlying}";

        public static string RapterOne => $"Rapter one, ready to Deploy!! {MensagensEmoji.Choper} {MensagensEmoji.Oni}";

        public static string SolidSnake => $"Kept you waitin, unh?! {MensagensEmoji.Cigar} {MensagensEmoji.Snake}";

        public static string YeahPapito => $"Yeah Papito, C'mon Kids! {MensagensEmoji.SunGlassesFace}";

        public static string YesBaby => $"Yes Baby, Thank you! {MensagensEmoji.RiseHand}{MensagensEmoji.RiseHand}";


        public static string ObterAleatoriamente() 
        {
            var mensagens = RegistrarMensagensLista();
            var random = new Random();
            var valor = random.Next(mensagens.Count);

            return mensagens[valor];
        }

        public static List<string> RegistrarMensagensLista() 
        {
            var respostasPossiveis = new List<string>();

            respostasPossiveis.AddRange(new[]
            {
                BotLoko,
                Dollar,
                RapterOne,
                SolidSnake,
                YeahPapito, 
                YesBaby
            });

            return respostasPossiveis;
        }
    }
}
