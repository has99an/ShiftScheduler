using Microsoft.Maui.Controls;
using ShiftSchedulerMobile.ViewModels;

namespace ShiftSchedulerMobile.Views
{
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
            BindingContext = new LoginViewModel();
        }
    }
}
