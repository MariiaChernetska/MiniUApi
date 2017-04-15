using System;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using MiniUApi.Models.Database;
using MiniUApi.Models.ViewModels;
using MiniUApi.Data;

namespace MiniUApi.Services
{
    public class UserService 
    {
        private UserRepository _userRepository;

        public UserService()
        {
            _userRepository = new UserRepository();
        }
        public QueryResult<LoginData> Authenticate(string userName, string password)
        {
            var hash = GetHash(password);

            return Authenticate(userName, hash);
        }

        public QueryResult<LoginData> Authenticate(string userName, byte[] hash)
        {
            string password = GetStringFromBytes(hash);
            var res = _userRepository.CheckUser(userName, password);
            if (res != null)
            {
                return new QueryResult<LoginData>(
                    new LoginData
                    {
                        Token = CreateToken(res.Login, res.Password),
                        UserName = userName,
                        ID = res.ID
                    }
                    );
            }
            else
            {
                return new QueryResult<LoginData>("Wrong email or password");
            }
        }
        public QueryResult<User> InsertUser(UserRegisterViewModel userModel)
        {
            userModel.Password = GetStringFromBytes(GetHash(userModel.Password));
            var res = _userRepository.InsertUser(userModel);
            return res;
        }

        private string CreateToken(string username, string password)
        {
            var plainTextBytes = Encoding.UTF8.GetBytes(username+":"+password);
            return Convert.ToBase64String(plainTextBytes);
        }
        private static string GetStringFromBytes(byte[] hash)
        {
            return BitConverter.ToString(hash).Replace("-", String.Empty);
        }

        private byte[] GetHash(string password)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] checkSum = md5.ComputeHash(Encoding.UTF8.GetBytes(password));
            return checkSum;
        }
        public void Dispose()
        {
            _userRepository.Dispose();
        }
    }
}