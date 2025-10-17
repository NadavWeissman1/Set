using Set.Models;
using Set.ModelsLogic;
using System.Windows.Input;
namespace Set.ViewModels { 

    internal partial class RegisterPageVM:ObservableObject
    {
        private readonly User user = new();
        public ICommand NavToLoginCommand => new Command(NavToLogin);
        public ICommand RegisterCommand { get; }
        public ICommand ToggleIsPasswordCommand { get; }
        public ICommand ToggleIsConfirmPasswordCommand { get; }
        public bool IsBusy { get; set; } = false;
        public string UserName
        {
            get => user.UserName;
            set
            {
                user.UserName = value;
                (RegisterCommand as Command)?.ChangeCanExecute();
            }
        }
        public string Password
        {
            get => user.Password;
            set
            {
                user.Password = value;
                (RegisterCommand as Command)?.ChangeCanExecute();
            }
        }
        public string ConfirmPassword
        {
            get => user.ConfirmPassword;
            set
            {
                user.ConfirmPassword = value;
                (RegisterCommand as Command)?.ChangeCanExecute();
            }
        }
        public string Email
        {
            get => user.Email;
            set
            {
                user.Email = value;
                (RegisterCommand as Command)?.ChangeCanExecute();
            }
        }
        public bool CanRegister()
        {
            return !string.IsNullOrWhiteSpace(user.UserName)&& !string.IsNullOrWhiteSpace(user.Password)&& !string.IsNullOrWhiteSpace(user.Email)&&!string.IsNullOrWhiteSpace(user.ConfirmPassword)&&user.CanRegister();
        }
        public bool IsPassword { get; set; } = true;
        public bool IsConfirmPassword { get; set; } = true;
        public RegisterPageVM()
        {
            RegisterCommand = new Command(async () => await Register());
            ToggleIsPasswordCommand = new Command(ToggleIsPassword);
            ToggleIsConfirmPasswordCommand = new Command(ToggleIsConfirmPassword);
        }
        public void ToggleIsPassword()
        {
            IsPassword = !IsPassword;
            OnPropertyChanged(nameof(IsPassword));
        }
        public void ToggleIsConfirmPassword()
        {
            IsConfirmPassword = !IsConfirmPassword;
            OnPropertyChanged(nameof(IsConfirmPassword));
        }

        private async void NavToLogin()
        {
            await Shell.Current.GoToAsync("///LoginPage");
        }
        public static async void NavToMainPage()
        {
            await Shell.Current.GoToAsync("///MainPage");
        }
        private async Task Register()
        {
            if (CanRegister())
            {
                IsBusy = true;
                OnPropertyChanged(nameof(IsBusy));
                bool isSuccesful = await user.Register();
                IsBusy = false;
                OnPropertyChanged(nameof(IsBusy));
                if(isSuccesful) 
                   NavToMainPage();
            }
        }


    }
}
