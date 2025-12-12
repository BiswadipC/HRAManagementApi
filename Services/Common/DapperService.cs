using Dapper;
using Repository.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Common
{
    public class DapperService : IDapperService
    {
        private readonly IDbConnection conn;

        public DapperService(IDbConnection conn)
        {
            this.conn = conn;
        } // constructor...

        public async Task<List<T>> QuerySqlRows<T>(string sql, object values = null!)
        {
            List<T> tt = new List<T>();
            try
            {
                tt = (await conn.QueryAsync<T>(sql, values)).ToList();
                return tt;
            }
            catch
            {
                return tt;
            }
        } // QuerySql...

        public async Task<T> QuerySqlRecord<T>(string sql, object values = null!)
        {
            T t;
            try
            {
                t = await conn.QuerySingleAsync<T>(sql, values);
                return t;
            }
            catch
            {
                throw new NotImplementedException();
            }
        } // QuerySqlRecord...

        public async Task<T> QuerySqlFirst<T>(string sql, object values = null!)
        {
            T t;
            try
            {
                t = await conn.QueryFirstAsync<T>(sql, values);
                return t;
            }
            catch
            {
                throw new NotImplementedException();
            }
        } // QuerySqlFirst...

        public async Task<List<T>> QueryWithSPList<T>(string StoreProcedure, object values = null!)
        {
            List<T> tt = new List<T>();
            try
            {
                tt = (await conn.QueryAsync<T>(StoreProcedure, values, commandType: CommandType.StoredProcedure)).ToList();
                return tt;
            }
            catch
            {
                return tt;
            }
        } // QueryWithSPList...

        public async Task<T> QueryWithSPRecord<T>(string StoreProcedure, object values = null!)
        {
            T t;
            try
            {
                t = await conn.QuerySingleAsync<T>(StoreProcedure, values, commandType: CommandType.StoredProcedure);
                return t;
            }
            catch
            {
                throw new NotImplementedException();
            }
        } // QueryWithSPRecord...

        public async Task SaveData(string Query, object values = null!)
        {
            await conn.ExecuteAsync(Query, values);
        } // SaveData..

        public async Task SaveDataSP(string StoreProcedure, object values = null!)
        {
            await conn.ExecuteAsync(StoreProcedure, values, commandType: CommandType.StoredProcedure);
        } // SaveDataSP...
    } // class...
}
