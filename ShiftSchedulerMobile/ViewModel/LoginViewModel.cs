using System.Windows.Input;
using ShiftSchedulerMobile.Models;
using ShiftSchedulerMobile.Services;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace ShiftSchedulerMobile.ViewModels
{
    public class LoginViewModel : INotifyPropertyChanged
    {
        private readonly LoginService _loginService;

        private string _email;
        private string _password;
        private bool _isLoginSuccessful;

        public event PropertyChangedEventHandler PropertyChanged;

        public LoginViewModel()
        {
            _loginService = new LoginService(new HttpClient());
            LoginCommand = new Command(async () => await LoginAsync());
        }

        public string Email
        {
            get => _email;
            set
            {
                _email = value;
                OnPropertyChanged();
            }
        }

        public string Password
        {
            get => _password;
            set
            {
                _password = value;
                OnPropertyChanged();
            }
        }

        public bool IsLoginSuccessful
        {
            get => _isLoginSuccessful;
            set
            {
                _isLoginSuccessful = value;
                OnPropertyChanged();
            }
        }

        public ICommand LoginCommand { get; }

        public async Task LoginAsync()
        {
            try
            {
                var employee = await _loginService.LoginAsync(Email, Password);
                IsLoginSuccessful = employee != null;
                // Håndter yderligere logik, hvis login er succesfuldt
            }
            catch (HttpRequestException ex)
            {
                // Håndter fejl her (fx vis en besked til brugeren)
            }
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
