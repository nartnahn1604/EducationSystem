using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
﻿using IT008_UIT.ViewModel;
using System.Windows;

namespace IT008_UIT
{
    /// <summary>
    /// Interaction logic for LoginScreen.xaml
    /// </summary>
    public partial class LoginScreen : Window
    {
        public LoginViewModel Viewmodel { get; set; }
        public LoginScreen()
        {
            InitializeComponent();
            this.DataContext = Viewmodel = new LoginViewModel();

        }
    }
}
