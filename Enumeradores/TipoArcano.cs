using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Enumeradores
{
    public enum TipoArcano
    {
        [Description("Indefinido")]
        Indefinido,
        [Description("Virtude")]
        Virtude,
        [Description("Hubris")]
        Hubris
    }
}
