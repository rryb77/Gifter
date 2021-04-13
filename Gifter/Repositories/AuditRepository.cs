using Gifter.Utils;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gifter.Repositories
{
    public class AuditRepository : BaseRepository
    {
        public AuditRepository(IConfiguration configuration) : base(configuration) { }

        public void Add(string tableName, string operations, string oldValue, string newValue)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        INSERT INTO Audit (TableName, Operation, DateTime, OldValue, NewValue)
                        VALUES (@TableName, @Operation, @DateTime, @OldValue, @NewValue)";

                    DbUtils.AddParameter(cmd, "@TableName", tableName);
                    DbUtils.AddParameter(cmd, "@Operation", operations);
                    DbUtils.AddParameter(cmd, "@DateTime", DateTime.Now);
                    DbUtils.AddParameter(cmd, "@OldValue", oldValue);
                    DbUtils.AddParameter(cmd, "@NewValue", newValue);

                    cmd.ExecuteScalar();
                }
            }
        }
    }
}
