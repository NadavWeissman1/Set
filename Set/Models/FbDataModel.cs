using Firebase.Auth;
using Firebase.Auth.Providers;
using Plugin.CloudFirestore;
namespace Set.Models
{
    abstract class FbDataModel
    {
        protected FirebaseAuthClient facl;
        protected IFirestore fdb;
        public abstract string DisplayName { get; }
        public abstract string UserId { get; }
        public abstract Task<bool> CreateUserWithEmailAndPasswordAsync(string email, string password, string name, Func<Task, Task<bool>> OnCompleteRegister);
        public abstract Task<bool> SignInWithEmailAndPWdAsync(string email, string password, Func<Task, Task<bool>> OnCompleteLogin);
        public abstract void SendResetEmailPasswordAsync(string email, Action<Task> OnComplete);
        public abstract Task<T> GetUserDataAsync<T>(string key);

        public FbDataModel()
        {
            FirebaseAuthConfig fac = new()
            {
                ApiKey = Keys.FbApiKey,
                AuthDomain = Keys.FbAppDomainKey,
                Providers = [new EmailProvider()]
            };
            facl = new FirebaseAuthClient(fac);
            fdb = CrossCloudFirestore.Current.Instance;
        }
    }
}