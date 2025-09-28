using Set.Models;
using Set.ModelsLogic;
using System.Windows.Input;
namespace Set.ViewModels { 

    internal partial class RegisterPageVM:ObservableObject
    {
        private readonly User user = new();
        private readonly LoginWithInfo loginWithInfo = new();
        public ICommand RegisterCommand { get; }
        public ICommand ToggleIsPasswordCommand { get; }
        public ICommand ToggleIsConfirmPasswordCommand { get; }
        public string IsLoginInfo => loginWithInfo.IsLoginInfo;
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
            return !string.IsNullOrWhiteSpace(user.UserName)&& !string.IsNullOrWhiteSpace(user.Password)&& !string.IsNullOrWhiteSpace(user.Email)&&!string.IsNullOrWhiteSpace(user.ConfirmPassword);
        }
        public bool IsPassword { get; set; } = true;
        public bool IsConfirmPassword { get; set; } = true;
        public RegisterPageVM()
        {
            RegisterCommand = new Command(Register, CanRegister);
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
        private async void Register()
        {
                user.Register();         
                await Shell.Current.GoToAsync("///LoginPage"); 
        }


    }
}
