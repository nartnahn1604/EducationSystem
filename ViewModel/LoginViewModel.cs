using IT008_UIT.Queries;
using IT008_UIT.Stores;
using MVVMEssentials.Services;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace IT008_UIT.ViewModel
{
    public class LoginViewModel : BaseViewModel
    {
        private readonly AuthenticationStore _authenticationStore;
        public string Username => _authenticationStore.CurrentUser?.DisplayName ?? "Unknown";
        private string _secretMessage;
        public string SecretMessage
        {
            get
            {
                return _secretMessage;
            }
            set
            {
                _secretMessage = value;
                OnPropertyChanged(nameof(SecretMessage));
            }
        }
        public ICommand LoadSecretMessageCommand { get; }
        public ICommand DangNhapCommand { get; set; }
        public ICommand QuenMatKhauCommand { get; set; }
        public ICommand PasswordVisibleCommand { get; set; }

        public LoginViewModel(AuthenticationStore authenticationStore,
            IGetSecretMessageQuery getSecretMessageQuery,
            INavigationService profileNavigationService,
            INavigationService loginNavigationService)
        {
            _authenticationStore = authenticationStore;

            LoadSecretMessageCommand = new LoadSecretMessageCommand(this, getSecretMessageQuery);
            QuenMatKhauCommand = new QuenMatKhauScreen(authenticationStore, loginNavigationService);
        }
        public LoginViewModel()
        {
            DangNhapCommand = new RelayCommand<Window>((p) => { return p == null ? false : true; }, (p) =>
            {
                MainWindow homescreen = new MainWindow();
                homescreen.Show();
                p.Close();
            }
            );

            QuenMatKhauCommand = new RelayCommand<Window>((p) => { return p == null ? false : true; }, (p) =>
            {
                QuenMatKhauScreen quenmatkhauscreen = new QuenMatKhauScreen();
                quenmatkhauscreen.Show();
                p.Close();
            }
            );

            PasswordVisibleCommand = new RelayCommand<PasswordBox>((p) => { return p == null ? false : true; }, (p) =>
            {

            }
            );
        }
    }
}
