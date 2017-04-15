using System.Linq;
using System.Data;
using Dapper;
using MiniUApi.Models.Database;
using MiniUApi.Models.ViewModels;

namespace MiniUApi.Data
{
    public class UserRepository : BaseRepository
    {
       
        public User CheckUser(string userName, string password)
        {
            var p = new DynamicParameters();
            p.Add("@Login", userName, size: 40);
            p.Add("@Exists", direction: ParameterDirection.Output, size: 1);
            p.Add("@Error", direction: ParameterDirection.Output, size: 40);
            p.Add("@Password", password, size: 40);
            var res = Connection.Query<User>("CheckUser", p, commandType: CommandType.StoredProcedure).FirstOrDefault();
            var ex = p.Get<dynamic>("@Exists");
            if (p.Get<dynamic>("@Exists") != "0")
            {
                return res;
            }
            else
            {
                return null;
            }
        }

        public QueryResult<User> InsertUser(UserRegisterViewModel userModel)
        {
            var p = new DynamicParameters();
            p.Add("@Login", userModel.Login, size: 40);
            p.Add("@FullName", userModel.FullName, size: 40);
            p.Add("@Password", userModel.Password, size: 40);
            p.Add("@Error", direction: ParameterDirection.Output, size: 40);
            p.Add("@ErrorCode", direction: ParameterDirection.Output, dbType: DbType.Int32);
            p.Add("@Error", direction: ParameterDirection.Output, size: 40);
            p.Add("@ErrorCode", direction: ParameterDirection.Output, dbType: DbType.Int32);

            var user = Connection.Query<User>("InsertUser", p, commandType: CommandType.StoredProcedure).FirstOrDefault();
            var error = p.Get<string>("@Error");
            var errorCode = p.Get<dynamic>("@ErrorCode");

            if (errorCode == 1)
            {
                return new QueryResult<User>(error);
            }
            else
            {
                return new QueryResult<User>(user);
            }
        }
           
    
    }
}
