﻿using System;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace IT008_UIT.UserControlGym
{
    /// <summary>
    /// Interaction logic for HopDongUC.xaml
    /// </summary>
    public partial class HopDongUC : UserControl
    {
        public HopDongUC ViewModel { get; set; }
        public HopDongUC()
        {
            InitializeComponent();
            this.DataContext = ViewModel = new HopDongUC();
        }
    }
}
