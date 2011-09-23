using System;

namespace WebToolkit.Converter
{
    public static class SafeConvert
    {
        public static bool ToBool(object value)
        {
            try{return Convert.ToBoolean(value);}catch{return false;}
        }

        public static int ToInt(object value)
        {
            try { return Convert.ToInt32(value); }catch { return 0; }
        }
    }
}
