using Mensagens;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;

namespace Comum
{
    public static class FormatHandler
    {
        public static IEnumerable<string> ToLowerList(this IEnumerable<string> list) 
        {
           return list.Select(o => o.ToLower());
        }

        public static string GetDescription<T>(this T e) where T : IConvertible
        {
            if (e is System.Enum)
            {
                Type type = e.GetType();
                Array values = System.Enum.GetValues(type);
                foreach (int val in values)
                {
                    if (val == e.ToInt32(CultureInfo.InvariantCulture))
                    {
                        var memInfo = type.GetMember(type.GetEnumName(val));
                        if (memInfo[0]
                            .GetCustomAttributes(typeof(DescriptionAttribute), false)
                            .FirstOrDefault() is DescriptionAttribute descriptionAttribute)
                        {
                            return descriptionAttribute.Description;
                        }
                    }
                }
            }

            return null;
        }

        public static int ParseIfValid(this string item)
        {
            int valor;

            if (!int.TryParse(item, out valor))
                throw new RegraException(MensagensCrud.ConverterTextoEmNumero);

            return valor;

        }
    }
}
