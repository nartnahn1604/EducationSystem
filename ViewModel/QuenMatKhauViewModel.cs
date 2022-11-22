using IT008_UIT.Utils;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace IT008_UIT.ViewModel
{
    public class QuenMatKhauViewModel : BaseViewModel
    {
        private string _email;
        public string Email
        {
            get
            {
                return _email;
            }
            set
            {
                _email = value;
                OnPropertyChanged(nameof(Email));
            }
        }
        public ICommand QuenMatKhauCommand { get; set; }
        public ICommand BacktoLoginCommand { get; set; }

        public QuenMatKhauViewModel()
        {
            QuenMatKhauCommand = new RelayCommand<object> ((p) => { return p == null ? false : true; }, async (p) =>
            {
                Debug.WriteLine(Email);
                var isSent = await FirebaseHelper.resetPassword(Email);
                if (isSent)
                {
                    MessageBox.Show("Check your email to reset password");
                    
                    
                }
                else
                    MessageBox.Show("Check your information again!");
            }
            );
            BacktoLoginCommand = new RelayCommand<Window>((p) => { return p == null ? false : true; }, (p) =>
            {

            }
            );
        }

    }
}
