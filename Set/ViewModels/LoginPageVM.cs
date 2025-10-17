using Set.Models;
using Set.ModelsLogic;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace Set.ViewModels
{
    internal partial class LoginPageVM:ObservableObject
    {
        private readonly User user = new();
        public ICommand NavToRegisterCommand => new Command(NavToRegister);
        public ICommand LoginCommand { get; }
        public ICommand ToggleIsPasswordCommand { get; }
        public ICommand ResetPasswordCommand => new Command(ResetPassword);
        public bool IsBusy { get; set; } = false;
        public string ResetEmail
        {
            get => user.ResetEmail;
            set
            {
                user.ResetEmail = value;
            }
        }
        public string Email {
            get => user.Email;
            set
            {
                user.Email = value;
                (LoginCommand as Command)?.ChangeCanExecute();
            }
        }
        public string Password {
            get => user.Password;
            set
            {
                user.Password = value;
                (LoginCommand as Command)?.ChangeCanExecute();
            }
        }
        public bool IsPassword {  get; set; } = true;
        
        public LoginPageVM()
        {
            LoginCommand = new Command(async () => await Login());
            ToggleIsPasswordCommand = new Command(ToggleIsPassword);
        }

        public void ToggleIsPassword()
        {
            IsPassword = !IsPassword;
            OnPropertyChanged(nameof(IsPassword));
        }

        private async Task Login()
        {
            if (CanLogin())
            {
                IsBusy = true;
                OnPropertyChanged(nameof(IsBusy));
                bool isSuccesful = await user.Login();
                await Task.Delay(3500);
                IsBusy = false;
                OnPropertyChanged(nameof(IsBusy));
                if (isSuccesful) 
                    NavToMainPage();
            }
        }
        public async void ResetPassword()
        {
            string resetEmail = await Shell.Current.DisplayPromptAsync(
                Strings.ResetPWPrompt,
                Strings.ResetEmailPrompt,
                Strings.Ok,
                Strings.Cancel,
                maxLength: 50,
                keyboard: Microsoft.Maui.Keyboard.Email
                );
            ResetEmail = resetEmail;
            user.ResetPassword();
        }
        public static async void NavToMainPage()
        {
            await Shell.Current.GoToAsync("///MainPage");
        }
        private async void NavToRegister()
        {
            await Shell.Current.GoToAsync("///RegisterPage");
        }

        private bool CanLogin()
        {
            return (!string.IsNullOrEmpty(Email) && !string.IsNullOrEmpty(Password)&&user.CanLogin());
        }


    }
}
