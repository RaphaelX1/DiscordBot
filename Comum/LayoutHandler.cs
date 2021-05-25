using Comum;
using DSharpPlus.Entities;
using DSharpPlus.Interactivity;
using DSharpPlus.Net;
using Enumeradores;
using Mensagens;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using static DSharpPlus.Entities.DiscordEmbedBuilder;

namespace Servicos
{
    public static class LayoutHandler
    {

        public static DiscordEmbedBuilder MontarInfo(string titulo, string corpo, string rodape, DiscordColor cor) 
        {
            return new DiscordEmbedBuilder()
            {
                Title = titulo,
                Color = cor,
                Description = corpo,

                Thumbnail = new EmbedThumbnail() 
                { 
                    Url = Imagens.ObterImagem(titulo)
                },

                Footer = new EmbedFooter()
                {
                    Text = rodape
                }
            };

        }

        public static DiscordEmbedBuilder MontarInfo(string titulo, string corpo, string rodape, TipoInfo tipoInfo, DiscordColor cor)
        {
            return new DiscordEmbedBuilder()
            {
                Title = titulo,
                Color = cor,
                Description = corpo,

                Thumbnail = new EmbedThumbnail()
                {
                    Url = Imagens.ObterImagem(tipoInfo)
                },

                Footer = new EmbedFooter()
                {
                    Text = rodape
                }
            };

        }

        public static DiscordEmbedBuilder MontarInfo(string titulo, string corpo, string rodape, IEnumerable<Tuple<string, string>> campos, TipoInfo tipoInfo, DiscordColor cor)
        {
            var info = new DiscordEmbedBuilder()
            {
                Title = titulo,
                Color = cor,
                Description = corpo,

                Thumbnail = new EmbedThumbnail()
                {
                    Url = Imagens.ObterImagem(tipoInfo)
                },

                Footer = new EmbedFooter()
                {
                    Text = rodape
                }
            };

            foreach (var campo in campos)
            {
                info.AddField(campo.Item1, string.IsNullOrWhiteSpace(campo.Item2) ? "-": campo.Item2);
            }

            return info;
        }

        public static Page MontarInfoPaginada(string titulo, string corpo, string rodape, IEnumerable<Tuple<string, string>> campos, TipoInfo tipoInfo, DiscordColor cor)
        {
            var page = new Page();
            
            var info = new DiscordEmbedBuilder()
            {
                Title = titulo,
                Color = cor,
                Description = corpo,

                Thumbnail = new EmbedThumbnail()
                {
                    Url = Imagens.ObterImagem(tipoInfo)
                },

                Footer = new EmbedFooter()
                {
                    Text = rodape
                }
            };

            foreach (var campo in campos)
            {
                info.AddField(campo.Item1, string.IsNullOrWhiteSpace(campo.Item2) ? "-" : campo.Item2);
            }

            page.Embed = info;

            return page;
        }

        public static DiscordEmbedBuilder MontarInfoConfirmacaoExclusao()
        {
            return new DiscordEmbedBuilder()
            {
                Title = "Opa!",
                Color = DiscordColor.Yellow,
                Description = MensagensCrud.ConfirmacaoExclusao,
            };
        }

    }
}
