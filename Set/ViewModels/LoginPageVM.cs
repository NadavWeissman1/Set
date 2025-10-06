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
        public bool IsBusy { get; set; } = false;
        public string UserName {
            get => user.UserName;
            set
            {
                user.UserName = value;
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
            LoginCommand = new Command(async () => await Login(), CanLogin);
            ToggleIsPasswordCommand = new Command(ToggleIsPassword);
        }

        public void ToggleIsPassword()
        {
            IsPassword = !IsPassword;
            OnPropertyChanged(nameof(IsPassword));
        }

        private async Task Login()
        {
            IsBusy = true;
            OnPropertyChanged(nameof(IsBusy));
            await Task.Delay(4000);
            IsBusy = false;
            OnPropertyChanged(nameof(IsBusy));
            NavToMainPage();
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
            return (!string.IsNullOrEmpty(UserName) && !string.IsNullOrEmpty(Password)&&user.CanLogin());
        }


    }
}
