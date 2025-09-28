namespace Set.Views;
using Set.ViewModels;

public partial class RegisterPage : ContentPage
{
	public RegisterPage()
	{
		InitializeComponent();
		BindingContext = new RegisterPageVM();
    }
}