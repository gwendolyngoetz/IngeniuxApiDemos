using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using Dapper;
using IngeniuxApiDemos.Common.Configuration;
using IngeniuxApiDemos.Source.Database.Model;

namespace IngeniuxApiDemos.Source.Database
{
    public class DatabaseSourceEndpoint : SourceEndpoint<IEnumerable<Park>>
    {
        public DatabaseSourceEndpoint(IConfiguration config) : base(config) { }

        protected override IEnumerable<Park> ExecuteInternal()
        {
            using (var connection = new SqlConnection(_config.Source.Database.ConnectionString))
            {
                return connection.Query<Park>("select * from Parks").ToList();
            }
        }
    }
}
