using Firebase.Auth;
using Firebase.Auth.Providers;
using Plugin.CloudFirestore;
using Set.Models;

namespace Set.ModelsLogic
{
    class FbData : FbDataModel
    {
        public FbData()
        {
            
        }
        public override async Task<bool> CreateUserWithEmailAndPasswordAsync(string email, string password, string name, Func<Task, Task<bool>> OnCompleteRegister)
        {
            Task<Firebase.Auth.UserCredential> firebaseTask = facl.CreateUserWithEmailAndPasswordAsync(email, password, name);
            bool succeeded;

            try
            {
                UserCredential credential = await firebaseTask;

                // Immediately sign in the new user so Firestore writes can succeed
                await facl.SignInWithEmailAndPasswordAsync(email, password);

            }
            catch (Exception ex)
            {
                TaskCompletionSource<Firebase.Auth.UserCredential> tcs = new();
                tcs.SetException(ex);
                firebaseTask = tcs.Task;
            }
            finally
            {
                succeeded = await OnCompleteRegister(firebaseTask);
            }
            return succeeded;
        }
        public override async Task<bool> SignInWithEmailAndPWdAsync(string email, string password, Func<Task, Task<bool>> OnCompleteLogin)
        {
            // Start Firebase sign-in
            Task<Firebase.Auth.UserCredential> firebaseTask = facl.SignInWithEmailAndPasswordAsync(email, password);
            bool succeeded;

            try
            {
                // Await Firebase sign-in
                await firebaseTask;
            }
            catch (Exception ex)
            {
                // Wrap the exception in a Task to pass to the callback
                TaskCompletionSource<Firebase.Auth.UserCredential> tcs = new();
                tcs.SetException(ex);
                firebaseTask = tcs.Task;
            }
            finally
            {
                // Always invoke the callback, even if the sign-in failed
                succeeded = await OnCompleteLogin(firebaseTask);
            }
            return succeeded;
        }
        public override async Task<T> GetUserDataAsync<T>(string key)
        {
            //this is a dummy function, since I dont have a database yet. But the structure of it still remains.
            await Task.CompletedTask;
            return default!;
        }
        public override async void SendResetEmailPasswordAsync(string email, Action<Task> OnComplete)
        {
            await facl.ResetEmailPasswordAsync(email).ContinueWith(OnComplete);
        }
        public override string DisplayName
        {
            get
            {
                string dn = string.Empty;
                if (facl.User != null)
                    dn = facl.User.Info.DisplayName;
                return dn;
            }
        }
        public override string UserId
        {
            get
            {
                return facl.User.Uid;
            }
        }
    }
}
