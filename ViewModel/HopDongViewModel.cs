using IT008_UIT.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace IT008_UIT.ViewModel
{
    public class HopDongViewModel : BaseViewModel
    {
        public ICommand TestCommand { get; set; }
        public HopDongViewModel() 
        {
            TestCommand = new RelayCommand<object>((p) => { return p == null ? false : true; }, (p) =>
            {
                Process.Start("explorer.exe");

            });
        }

    }
}
