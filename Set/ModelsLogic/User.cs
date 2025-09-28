using Set.Models;
namespace Set.ModelsLogic
{
    class User:UserModel
    {
        public override bool Login()
        {
            return true;
        }
        public override void Register()
        {
            Preferences.Set(Keys.UserNameKey, UserName);
            Preferences.Set(Keys.PasswordKey, Password);
            Preferences.Set(Keys.EmailKey, Email);
            Preferences.Set(Keys.ConfirmPasswordKey, ConfirmPassword);
        }
        public User()
        {
            UserName = Preferences.Get(Keys.UserNameKey, string.Empty);
            Password = Preferences.Get(Keys.PasswordKey, string.Empty);
            Email = Preferences.Get(Keys.EmailKey, string.Empty);
            ConfirmPassword = Preferences.Get(Keys.ConfirmPasswordKey, string.Empty);
        }
    }
}
