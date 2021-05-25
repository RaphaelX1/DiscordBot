using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicos
{
    public class DadoServico
    {
        public async Task<string> Lancar(int quantidadeDados, int valorDado) 
        {
            var retorno = new List<int>();
            var random = new Random();

            for (int i = 0; i < quantidadeDados; i++)
            {
                retorno.Add(random.Next(1, valorDado + 1));
            }

            return string.Join(", ", retorno.Select(o => o));
        }
    }
}
