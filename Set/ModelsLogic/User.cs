using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using Microsoft.Maui;
using Set.Models;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml.Linq;
using static Set.Models.ConstData;
namespace Set.ModelsLogic
{
    class User:UserModel
    {
        readonly Strings dynamicStrings = new();
        public override async Task<bool> Login()
        {
            bool success = await fbd.SignInWithEmailAndPWdAsync(Email, Password, OnCompleteLogin);
            return success;
        }
        public override void ResetPassword()
        {
            fbd.SendResetEmailPasswordAsync(ResetEmail, OnCompleteResetPassword);
        }
        private void OnCompleteResetPassword(Task task)
        {
        }
        public override async Task<bool> Register()
        {
            bool succeeded = await fbd.CreateUserWithEmailAndPasswordAsync(Email, Password, UserName, OnCompleteRegister);
            return succeeded;
        }
        private async Task<bool> OnCompleteRegister(Task task)
        {
            if (task.IsCompletedSuccessfully)
            {
                RegisterSaveToPreferences();
                await Toast.Make(Strings.UserCreatedText,ToastDuration.Long,14).Show();
                return true;
            }
            else
            {
                string errorMessage = IdentifyFireBaseError(task);
                await Shell.Current.DisplayAlert(Strings.CreateUserError, errorMessage, Strings.Ok);
                return false;
            }
        }
        private async Task<bool> OnCompleteLogin(Task task)
        {
            if (task.IsCompletedSuccessfully)
            {
                await LoginSaveToPreferencesAsync();
                await Toast.Make(Strings.UserLoggedText, ToastDuration.Long,14).Show();
                return true;
            }
            else
            {
                string errorMessage = IdentifyFireBaseError(task);
                await Shell.Current.DisplayAlert(Strings.LoginErrorTitle, errorMessage, Strings.Ok);
                return false;
            }
        }
        public override string IdentifyFireBaseError(Task task)
        {
            Exception? ex = task.Exception?.InnerException;
            string errorMessage = string.Empty;

            if (ex != null)
            {
                try
                {
                    // Find the "Response:" part
                    int responseIndex = ex.Message.IndexOf("Response:");
                    if (responseIndex >= 0)
                    {
                        // Take everything after "Response:"
                        string jsonPart = ex.Message.Substring(responseIndex + "Response:".Length).Trim();

                        // Some Firebase responses might have extra closing braces, remove trailing stuff
                        int lastBrace = jsonPart.LastIndexOf('}');
                        if (lastBrace >= 0)
                            jsonPart = jsonPart.Substring(0, lastBrace + 1);

                        // Parse JSON
                        JsonDocument json = JsonDocument.Parse(jsonPart);

                        JsonElement errorElem = json.RootElement.GetProperty("error");
                        string firebaseMessage = errorElem.GetProperty("message").ToString();

                        errorMessage = firebaseMessage switch
                        {
                            Keys.EmailExistsErrorKey => Strings.EmailExistsError,
                            Keys.OperationNotAllowedErrorKey => Strings.OperationNotAllowedError,
                            Keys.WeakPasswordErrorKey => Strings.WeakPasswordError,
                            Keys.MissingEmailErrorKey => Strings.MissingEmailError,
                            Keys.MissingPasswordErrorKey => Strings.MissingPasswordError,
                            Keys.InvalidEmailErrorKey => Strings.InvalidEmailError,
                            Keys.InvalidCredentialsErrorKey => Strings.InvalidCredentialsError,
                            Keys.UserDisabledErrorKey => Strings.UserDisabledError,
                            Keys.ManyAttemptsErrorKey => Strings.ManyAttemptsError,
                            _ => Strings.DefaultRegisterError,
                        };
                    }
                }
                catch
                {
                    errorMessage = Strings.FailedJsonError;
                }
            }
            return errorMessage;
        }

        private void RegisterSaveToPreferences()
        {
            Preferences.Set(Keys.UserNameKey, UserName);
            Preferences.Set(Keys.PasswordKey, Password);
            Preferences.Set(Keys.EmailKey, Email);
            Preferences.Set(Keys.ConfirmPasswordKey, ConfirmPassword);
        }
        private async Task LoginSaveToPreferencesAsync()
        {
            //THIS NEXT LINE IS A DUMMY JUST SO THE FUNCTION WILL STILL BE ASYNC, BECAUSE IN THE FUTURE WE WANNA GET
            //THE NAME FROM THE DATABASE, AND ITS ASYNC.
            await Task.CompletedTask;
            // Store everything in Preferences
            //FOR NOW, IT WILL STORE THE NAME DUMMY IN PREFERENCES, BUT WHEN ADDING A DATABASE IT SHOULD STORE
            //A STRING THAT CONTAINS THE VALUE OF THE NAME THAT'S IN THE DATABASE.

            Preferences.Set(Keys.UserNameKey, "dummy");
            Preferences.Set(Keys.EmailKey, Email);
        }

        public override bool CanLogin()
        {
            return IsEmailValid() && IsPasswordValid();   
        }

        public override bool CanRegister()
        {
            return IsUsernameValid() && IsPasswordValid() && IsEmailValid() && IsConfirmPasswordValid();
        }
        private bool IsUsernameValid()
        {
            if (UserName.Length < MinCharInUN)
            {
                Shell.Current.DisplayAlert(Strings.UserNameShortErrorTitle, dynamicStrings.UserNameShortErrorMessage, Strings.Ok);
                return false;
            }
            if (UserName.Length > MaxCharInUN)
            {
                Shell.Current.DisplayAlert(Strings.UserNameTooLongErrorTitle, dynamicStrings.UserNameTooLongErrorMessage, Strings.Ok);
                return false;
            }
            if (!ContainsNumber(UserName))
            {
                Shell.Current.DisplayAlert(Strings.UserNameNumberErrorTitle, Strings.UserNameNumberErrorMessage, Strings.Ok);
                return false;
            }
            return true;
        }
        private bool IsPasswordValid()
        {
            if (Password.Length < MinCharInPW)
            {
                Shell.Current.DisplayAlert(Strings.PasswordShortErrorTitle, dynamicStrings.PasswordShortErrorMessage, Strings.Ok);
                return false;
            }
            if(Password.Length > MaxCharInPW)
            {
                Shell.Current.DisplayAlert(Strings.PasswordTooLongErrorTitle, dynamicStrings.PasswordTooLongErrorMessage, Strings.Ok);
                return false;
            }
            if (!ContainsNumber(Password))
            {
                Shell.Current.DisplayAlert(Strings.PasswordNumberErrorTitle, Strings.PasswordNumberErrorMessage, Strings.Ok);
                return false;
            }
            if (!HasLowerCase(Password))
            {
                Shell.Current.DisplayAlert(Strings.PasswordLowerCaseErrorTitle, Strings.PasswordLowerCaseErrorMessage, Strings.Ok);
                return false;
            }
            if (!HasUpperCase(Password))
            {
                Shell.Current.DisplayAlert(Strings.PasswordUpperCaseErrorTitle, Strings.PasswordUpperCaseErrorMessage, Strings.Ok);
                return false;
            }
            return true;
        }
        public bool IsConfirmPasswordValid()
        {
            if(Password!=ConfirmPassword)
            {
                Shell.Current.DisplayAlert(Strings.ConfirmPasswordErrorTitle, Strings.ConfirmPasswordErrorMessage, Strings.Ok);
                return false;
            }
            return true;
        }
        public bool IsEmailValid()
        {
            if(Email.Length < MinCharInEmail)
            {
               Shell.Current.DisplayAlert(Strings.EmailShortErrorTitle, dynamicStrings.EmailShortErrorMessage, Strings.Ok);
            }
            else if (HasDot(Email) && HasAtSign(Email))
            {
                if (AtIndex(Email) == 0 || AtIndex(Email) == Email.Length - 1 || DotIndex(Email) == 0 || DotIndex(Email) == Email.Length - 1)
                {
                    Shell.Current.DisplayAlert(Strings.EmailInvalidErrorTitle, Strings.EmailInvalidErrorMessage, Strings.Ok);
                    return false;
                }
            }
            else
            {
                Shell.Current.DisplayAlert(Strings.EmailInvalidErrorTitle, Strings.EmailInvalidErrorMessage, Strings.Ok);
                return false;
            }
                return true;
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
