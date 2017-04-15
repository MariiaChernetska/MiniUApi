using System;

namespace MiniUApi.Models.ViewModels
{
    public class LoginData
    {
        public string UserName { get; set; }

        public string Token { get; set; }

        public Guid ID { get; set; }
    }
}