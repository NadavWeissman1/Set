using Set.ModelsLogic;
namespace Set.Models
{
    abstract class UserModel
    {
        protected FbData fbd = new();
        public string Email { get; set; } = string.Empty;
        public string ConfirmPassword { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string ResetEmail { get; set; } = string.Empty;
        public abstract Task Login();
        public abstract Task Register();
        public abstract string IdentifyFireBaseError(Task task);
        public abstract bool CanLogin();
        public abstract bool CanRegister();
        public abstract void ResetPassword();

    }
}
