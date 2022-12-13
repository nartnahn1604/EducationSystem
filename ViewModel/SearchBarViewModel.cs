using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Input;
using System.Diagnostics;
using IT008_UIT.UserControlGym;

namespace IT008_UIT.ViewModel
{
    class SearchBarViewModel : BaseViewModel
    {
        public ICommand SearchCommand { get; set; }
        private String _searchString;
        public String SearchingContent
        { 
            get => _searchString; 
            set { _searchString = value; OnPropertyChanged () ;}
        }
        private KhachHangViewModel kh;
        public SearchBarViewModel()
        {
            SearchCommand = new RelayCommand<UserControl>((p) => { return p == null ? false : true; }, (p) =>
            {
                FrameworkElement usercontrol = GetWindowParent(p);
                var w = usercontrol as UserControl;
                if (w != null)
                {
                    var KHsearch = w.DataContext as BaseViewModel;
                    KHsearch.SearchData(SearchingContent);
                }
            }
            );
        }
        FrameworkElement GetWindowParent(UserControl p)
        {
            FrameworkElement parent = p;
            while (parent.Parent != null)
            {
                parent = parent.Parent as FrameworkElement;
            }
            return parent;
        }

    }
}