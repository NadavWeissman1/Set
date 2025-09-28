namespace Set.Views;
using Set.ViewModels;
public partial class LoginPage : ContentPage
{
	public LoginPage()
	{
		InitializeComponent();
		BindingContext = new LoginPageVM();
	}
}