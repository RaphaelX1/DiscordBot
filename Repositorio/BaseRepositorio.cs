using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Repositorio
{
    public class BaseRepositorio
    {
        public AplicacaoContexto _contexto;

        public IMapper _mapper;

        public BaseRepositorio(AplicacaoContexto aplicacaoContexto, IMapper mapper)
        {
            _contexto = aplicacaoContexto;
            _mapper = mapper;
        }

        public Para Converter<De, Para>(De de)
        {
            return _mapper.Map<Para>(de);
        }

        public Para Converter<De, Para>(De de, Para para)
        {
            return _mapper.Map(de, para);
        }

        public List<Para> ConverterLista<De, Para>(IEnumerable<De> des)
        {
            var retorno = new List<Para>();

            foreach (var de in des)
            {
                retorno.Add(_mapper.Map<Para>(de));
            }

            return retorno;
        }
    }
}
