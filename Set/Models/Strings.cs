namespace Set.Models
{
    internal class Strings
    {
        public const string LoginPageTitle = "Login Here!";
        public const string UserNamePrompt = "Enter Username";
        public const string PasswordPrompt = "Enter Password";
        public const string LoginButtonPrompt = "Login";
        public const string RegisterPageTitle = "Register Here!";
        public const string EmailPrompt = "Enter Email";
        public const string RegisterButtonPrompt = "Register";
        public const string ConfirmPasswordPrompt = "Enter Your Password To Confirm";
        public const string Ok = "Ok";
        public const string CreateUserError = "User Not Created";
        public const string AlreadyHaveAccount = "Already Have An Account? ";
        public const string Login = "Click To Login!";
        public const string DontHaveAccount = "Don't Have An Account?";
        public const string Register = "Click To Register!";
        public const string UKErrorPrompt = "An unknown error occurred";
        public const string ForgotPW = "Forgot Your Password? ";
        public const string ResetPW = "Reset Here";
        public const string ResetPWPrompt = "Reset Password";
        public const string ResetEmailPrompt = "Please Enter Your Account's Email:";
        public const string Cancel = "Cancel";
        public const string EmailShortErrorTitle = "Email too short:";
        public string EmailShortErrorMessage = "The Email you provided is too short.\n" +
            "Your Email's minimum length must be " + ConstData.MinCharInEmail + " characters.\n" +
            "Please re-enter it and try again.";
        public const string EmailInvalidErrorTitle = "Invalid Email:";
        public const string EmailInvalidErrorMessage = "The Email you provided is invalid.\n" +
            "Please make sure it has '@' sign and a '.' and try again.";
        public const string PasswordShortErrorTitle = "Password too short:";
        public string PasswordShortErrorMessage = "The Password you provided is too short.\n" +
            "Your Password's minimum length must be " + ConstData.MinCharInPW + " characters.\n" +
            "Please re-enter it and try again.";
        public const string PasswordNumberErrorTitle = "Password doesn't have a number:";
        public const string PasswordNumberErrorMessage = "Your Password must contain at least one number.\n" +
            "Please add one or more digits, and try again.";
        public const string PasswordLowerCaseErrorTitle = "Password doesn't have a lower-case letter:";
        public const string PasswordLowerCaseErrorMessage = "Your Password must contain at least one lower-case letter.\n" +
            "Please add one and try again.";
        public const string PasswordUpperCaseErrorTitle = "Password doesn't have an upper-case letter:";
        public const string PasswordUpperCaseErrorMessage = "Your Password must contain at least one upper-case letter.\n" +
            "Please add one and try again.";
        public const string UserNameShortErrorTitle = "UserName too short:";
        public string UserNameShortErrorMessage = "The UserName you provided is too short.\n" +
            "Your Username's minimum length must be " + ConstData.MinCharInUN + " characters.\n" +
            "Please re-enter it and try again.";
        public const string UserNameNumberErrorTitle = "Username doesn't have a number:";
        public const string UserNameNumberErrorMessage = "Your username must contain at least one number.\n" +
            "Please add one or more digits and try again.";
        public const string PasswordTooLongErrorTitle = "Password too long:";
        public string PasswordTooLongErrorMessage = "The password you provided is too long.\n" +
            "Your Password's maximum length must be " + ConstData.MaxCharInPW + " characters.\n" +
            "Please re-enter it and try again.";
        public const string UserNameTooLongErrorTitle = "Username too long:";
        public string UserNameTooLongErrorMessage = "The UserName you provided is too Long.\n" +
            "Your Password's maximum length must be " + ConstData.MaxCharInUN + " characters.\n" +
            "Please re-enter it and try again.";
        public const string EmailExistsError = "The Email address is already used by another account.";
        public const string OperationNotAllowedError = "Unable to create an account in this method.";
        public const string WeakPasswordError = "The Password is too weak. It should be at least 8 characters.";
        public const string MissingEmailError = "Please provide an Email to create an account.";
        public const string MissingPasswordError = "Please provide a Password to create an account.";
        public const string InvalidEmailError = "Please provide a valid Email to create an account.";
        public const string InvalidCredentialsError = "One of the provided credentials (Email/Password) was incorrect.\n" +
            "Please re-enter your input and try again.";
        public const string UserDisabledError = "Your account has been disabled.\n" +
            "Contact our team at 'nadavweissman10@gmail.com' for more details, and we will try to help you.";
        public const string ManyAttemptsError = "There have been too many failed attempts, and your account has been temporarily disabled.\n" +
            "Please try again later.";
        public const string DefaultRegisterError = "Something went wrong, please try again later.\n" +
            "If the error persists, contact our developer team at 'Nadavweissman10@gmail.com'.";
        public const string FailedJsonError = "Something went wrong.\nThe system couldn't identify the error.\n" +
            "Please try again";
        public const string RegisterErrorTitle = "Error creating a user:";
        public const string LoginErrorTitle = "Error logging in:";
        public const string ConfirmPasswordErrorTitle = "Passwords do not match:";
        public const string ConfirmPasswordErrorMessage = "Your input in this field doesn't match the Password You've written.\n"+
            "Please match these fields and try again";
        public const string UserCreatedText = "User Created!\n" +
            "You'll now be moved to the Main Page";
        public const string UserLoggedText = "User Logged in!\n" +
            "You'll now be moved to the Main Page";

    }
}
