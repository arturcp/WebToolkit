using System.Configuration;

namespace WebToolkit.Index
{
    public static class IndexSettings
    {
        public static string IndexPath
        {
            get
            {
                return ConfigurationManager.AppSettings["Search.IndexPath"];
            }
        }

    }
}
