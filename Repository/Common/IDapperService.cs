using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Common
{
    public interface IDapperService
    {
        Task<List<T>> QuerySqlRows<T>(string sql, object values = null!);
        Task<T> QuerySqlRecord<T>(string sql, object values = null!);
        Task<T> QuerySqlFirst<T>(string sql, object values = null!);
        Task<List<T>> QueryWithSPList<T>(string StoreProcedure, object values = null!);
        Task<T> QueryWithSPRecord<T>(string StoreProcedure, object values = null!);
        Task SaveData(string Query, object values = null!);
        Task SaveDataSP(string StoreProcedure, object values = null!);
    } // interface...
}
