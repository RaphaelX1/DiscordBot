using System;
using System.Collections.Generic;
using System.Text;

namespace Comum
{
    public class RegraException: Exception
    {
        public RegraException(string mensagem): base(mensagem)
        {

        }
    }
}
