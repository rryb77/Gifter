using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace Gifter.Repositories
{
    // Setup as an abstract class so it can ONLY be used by inheritance
    public abstract class BaseRepository
    {
        private readonly string _connectionString;

        public BaseRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        // Protected so it is available to child classes, but nowhere else in the code
        protected SqlConnection Connection
        {
            get
            {
                return new SqlConnection(_connectionString);
            }
        }
    }
}
