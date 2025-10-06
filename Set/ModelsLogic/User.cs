using Set.Models;
using System.Text.Json;
using System.Threading.Tasks;
using static Set.Models.ConstData;

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
            fbd.CreateUserWithEmailAndPasswordAsync(Email, Password, UserName, OnComplete);
        }

        private void OnComplete(Task task)
        {
            if (!task.IsCompletedSuccessfully)
            {
                MainThread.BeginInvokeOnMainThread(async () =>
                {
                    string errorMessage = task.Exception?.Message ?? Strings.UKErrorPrompt;
                    await Shell.Current.DisplayAlert(Strings.CreateUserError, errorMessage, Strings.Ok);
                });
            }//next line breaks mvvm, needs fixing in the future
            else
            {
                Shell.Current.GoToAsync("///LoginPage");
            }
        }


        private void SaveToPreferences()
        {
            Preferences.Set(Keys.UserNameKey, UserName);
            Preferences.Set(Keys.PasswordKey, Password);
            Preferences.Set(Keys.EmailKey, Email);
            Preferences.Set(Keys.ConfirmPasswordKey, ConfirmPassword);
        }

        public override bool CanLogin()
        {
            return IsUsernameValid() && IsPasswordValid();   
        }

        public override bool CanRegister()
        {
            return IsUsernameValid() && IsPasswordValid() && IsEmailValid() && IsConfirmPasswordValid();
        }
        private bool IsUsernameValid()
        {
            return UserName.Length >= MinCharInUN && UserName.Length <= MaxCharInUN;
        }
        private bool IsPasswordValid()
        {
            return Password.Length >= MinCharInPW && Password.Length <= MaxCharInPW&&HasLowerCase(Password)&&HasUpperCase(Password)&&ContainsNumber(Password);
        }
        public bool IsConfirmPasswordValid()
        {
            return ConfirmPassword.Length >= MinCharInPW && ConfirmPassword.Length <= MaxCharInPW && HasLowerCase(ConfirmPassword) && HasUpperCase(ConfirmPassword)&&ContainsNumber(ConfirmPassword)&&Password==ConfirmPassword;
        }
        public bool IsEmailValid()
        {
            if (HasDot(Email) && HasAtSign(Email))
            {
                if(AtIndex(Email)!=0&&AtIndex(Email)!=Email.Length-1&&DotIndex(Email)!=0&&DotIndex(Email)!=Email.Length-1&&Email.Length>=MinCharInEmail)
                {
                    return true;
                }
            }
            return false;
        }
        private static bool HasUpperCase(string str)
        {
            for(int i = 0; i < str.Length; i++)
            {
                if (str[i]>='A' && str[i] <= 'Z')
                    return true;
            }
            return false;
        }
        private static bool HasLowerCase(string str)
        {
            for (int i = 0; i < str.Length; i++)
            {
                if (str[i] >= 'a' && str[i] <= 'z')
                    return true;
            }
            return false;
        }
        private static bool ContainsNumber(string str)
        {
            for (int i = 0; i < str.Length; i++)
            {
                if (str[i] >= '0' && str[i] <= '9')
                    return true;
            }
            return false;
        }
        private static bool HasAtSign(string str)
        {
            for(int i = 0; i < str.Length; i++)
            {
                if (str[i] == '@')
                {
                    return true;
                }
            }
            return false;
        }
        private static bool HasDot(string str)
        {
            for(int i = 0; i < str.Length; i++)
            {
                if (str[i] == '.')
                {
                    return true;
                }
            }
            return false;
        }
        private static int AtIndex(string str)
        {
            for(int i = 0; i < str.Length; i++)
            {
                if (str[i] == '@') return i;
            }
            return -1;
        }
        private static int DotIndex(string str)
        {
            for (int i = 0; i < str.Length; i++)
            {
                if (str[i] == '.') return i;
            }
            return -1;
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
