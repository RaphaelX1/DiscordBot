using Enumeradores;
using System;
using System.Collections.Generic;
using System.Text;

namespace Comum
{
    public static class Imagens
    {
        public static string Dependurado => "https://i.pinimg.com/474x/e4/4e/39/e44e3919f62ffd9e68965556bc7e71b6.jpg";
        public static string Louco => "https://i.pinimg.com/originals/b1/42/2d/b1422d40a97bae3a9e77757a4d19ecaf.png";
        public static string Magico => "https://images-na.ssl-images-amazon.com/images/I/519zwavaOFL._AC_SY355_.jpg";
        public static string Diabo => "https://i.pinimg.com/236x/64/f1/55/64f1558cf26275c95c8c0b2561eebbc2--tarot-comic-book.jpg";
        public static string Torre => "https://i.pinimg.com/originals/90/60/b6/9060b6ffb86a08df38e411dda236bb79.jpg";
        public static string Enamorados => "https://i.pinimg.com/236x/dd/2f/00/dd2f0097628e662cd4ce250e5f10374c--tarot.jpg";
        public static string Lua => "https://static.wikia.nocookie.net/jjba/images/8/8a/18_The_Moon.jpg/revision/latest/scale-to-width-down/340?cb=20130907151541&path-prefix=fr";
        public static string Sol => "https://i.pinimg.com/originals/d1/c2/68/d1c268be9a3331d7ce85c572994f8275.png";
        public static string Imperador => "https://i.pinimg.com/originals/5f/5e/e2/5f5ee2e47bad5ab77fc0ace627249211.jpg";

        public static string CartaTarot => "https://www.horoscope.com/images-US/tarot/back/tarot-card.png";

        public static string Punho => "https://i.ibb.co/rQvkLsP/Icon-3-10.png";

        public static string Medalha => "https://i.ibb.co/hH48rWK/Icon-5-10.png";

        public static string Pena => "https://i.ibb.co/nM4K8GP/Icon-4-70.png";

        public static string Espada => "https://i.ibb.co/cyVwsyt/Icon-5-46.png";

        public static string Castelo => "https://i.ibb.co/wJrJH45/Icon-3-17.png";

        public static string Reza => "https://i.ibb.co/hy59L8S/Icon-7-06.png";

        public static string Pirata => "https://i.ibb.co/WGBCcMt/Icon-3-27.png";

        public static string Pilula => "https://i.ibb.co/LrfWrbG/Icon-2-60.png";

        public static string ObterImagem(string nome)
        {
            if (nome.ToLower().Equals(Arcanos.Dependurado.GetDescription().ToLower()))
                return Dependurado;
            else if (nome.ToLower().Equals(Arcanos.Louco.GetDescription().ToLower()))
                return Louco;
            else if (nome.ToLower().Equals(Arcanos.Magico.GetDescription().ToLower()))
                return Magico;
            else if (nome.ToLower().Equals(Arcanos.Diabo.GetDescription().ToLower()))
                return Diabo;
            else if (nome.ToLower().Equals(Arcanos.Torre.GetDescription().ToLower()))
                return Torre;
            else if (nome.ToLower().Equals(Arcanos.Enamorados.GetDescription().ToLower()))
                return Enamorados;
            else if (nome.ToLower().Equals(Arcanos.Noite.GetDescription().ToLower()))
                return Lua;
            else if (nome.ToLower().Equals(Arcanos.Sol.GetDescription().ToLower()))
                return Sol;
            else if (nome.ToLower().Equals(Arcanos.Imperador.GetDescription().ToLower()))
                return Imperador;


            else
                return CartaTarot;
        }

        public static string ObterImagem(TipoInfo tipoInfo)
        {
            if (tipoInfo == TipoInfo.Atributo)
                return Punho;
            else if (tipoInfo == TipoInfo.Formacao)
                return Medalha;
            else if (tipoInfo == TipoInfo.Vantagem)
                return Pena;
            else if (tipoInfo == TipoInfo.Pericia)
                return Espada;
            else if (tipoInfo == TipoInfo.Nacao)
                return Castelo;
            else if (tipoInfo == TipoInfo.Religiao)
                return Reza;
            else if (tipoInfo == TipoInfo.Personagem)
                return Pirata;
            else if (tipoInfo == TipoInfo.Personagem)
                return Pirata;
            else if (tipoInfo == TipoInfo.Efeito)
                return Pilula;

            else
                return CartaTarot;
        }
    }
}
