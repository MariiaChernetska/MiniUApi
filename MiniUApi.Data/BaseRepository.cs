using System;
using System.Data.SqlClient;

namespace MiniUApi.Data
{
    public class BaseRepository : IDisposable
    {
        private ConnectionStringProvider _provider;

        protected SqlConnection Connection { get; private set; }

        public BaseRepository()
        {
            _provider = new ConnectionStringProvider();
            Connection = new SqlConnection(_provider.ConnectionString);
        }

        public void Dispose()
        {
            Connection.Dispose();
        }
    }
}
