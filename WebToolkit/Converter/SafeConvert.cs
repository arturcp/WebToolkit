using System;

namespace WebToolkit.Converter
{
    public static class SafeConvert
    {
        public static bool ToBool(object value)
        {
            try{return Convert.ToBoolean(value);}catch{return false;}
        }
    }
}
