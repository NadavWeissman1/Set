using Set.ModelsLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Set.Models
{
    abstract class UserModel
    {
        protected FbData fbd = new();
        public string Email { get; set; } = string.Empty;
        public string ConfirmPassword { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public abstract bool Login();
        public abstract void Register();
        public abstract bool CanLogin();
        public abstract bool CanRegister();

    }
}
