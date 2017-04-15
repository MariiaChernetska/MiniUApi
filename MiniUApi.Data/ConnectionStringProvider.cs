
using System.Configuration;

namespace MiniUApi.Data
{
    class ConnectionStringProvider
    {
        public string ConnectionString
        {
            get
            {
                return ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            }
        }
    }
}
