using System.Data;
using System.Data.Common;
using System.Data.Entity.Infrastructure.Interception;
using System.Linq;

namespace Model.Data
{
    public class DateInterceptor : IDbInterceptor
    {
        public void ReaderExecuting(DbCommand command,
            DbCommandInterceptionContext<DbDataReader> interceptionContext)
        {
            var dateParameters = command.Parameters.OfType<DbParameter>()
                .Where(p => p.DbType == DbType.DateTime2);
            foreach (var parameter in dateParameters)
            {
                parameter.DbType = DbType.DateTime;
            }
        }
    }
}
