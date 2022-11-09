using System.Windows;
using System.Windows.Input;

namespace IT008_UIT.ViewModel
{
    public class DoiMatKhauViewModel
    {
        public ICommand BacktoQuenMatKhauCommand { get; set; }
        public DoiMatKhauViewModel()
        {
            BacktoQuenMatKhauCommand = new RelayCommand<Window>((p) => { return p == null ? false : true; }, (p) =>
            {

            }
            );
        }
    }
}
