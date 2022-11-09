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

            }
            );
            BacktoLoginCommand = new RelayCommand<Window>((p) => { return p == null ? false : true; }, (p) =>
            {

            }
            );
        }

    }
}
