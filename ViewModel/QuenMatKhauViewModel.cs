using System.Windows;
using System.Windows.Input;

namespace IT008_UIT.ViewModel
{
    public class QuenMatKhauViewModel
    {
        public ICommand QuenMatKhauCommand { get; set; }
        public ICommand BacktoLoginCommand { get; set; }

        public QuenMatKhauViewModel()
        {
            QuenMatKhauCommand = new RelayCommand<Window>((p) => { return p == null ? false : true; }, (p) =>
            {
                DoiMatKhauScreen screen = new DoiMatKhauScreen();
                screen.Show();
                p.Close();
            }
            );
            BacktoLoginCommand = new RelayCommand<Window>((p) => { return p == null ? false : true; }, (p) =>
            {
                LoginScreen screen = new LoginScreen();
                screen.Show();
                p.Close();
            }
            );
        }

    }
}
