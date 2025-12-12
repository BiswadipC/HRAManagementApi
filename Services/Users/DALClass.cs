using Dapper;
using Domain.Users;
using Infrastructure.Models;
using Repository.Common;
using Repository.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Users
{
    namespace NUsers
    {
        internal sealed class DALClass : IUserResponse
        {
            private readonly SampleContext context;
            private readonly IDapperService dapper;

            public DALClass(SampleContext context, IDapperService dapper)
            {
                this.context = context;
                this.dapper = dapper;
            } // constructor...

            public async Task<string> AddNewUser(NewUserResponse user)
            {
                DynamicParameters dp = new DynamicParameters();
                dp.Add("@username", user.Username);
                dp.Add("@password", user.Password);

                await dapper.SaveDataSP("spCreateNewUser", dp);
                return "Success";
            } // AddNewUser...
        } // class...
    } // namespace NUsers...
}
