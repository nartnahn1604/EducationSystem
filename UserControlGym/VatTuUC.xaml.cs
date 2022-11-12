﻿using IT008_UIT.ViewModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace IT008_UIT.UserControlGym
{
    /// <summary>
    /// Interaction logic for VatTuUC.xaml
    /// </summary>
    public partial class VatTuUC : UserControl
    {
        public VatTuViewModel ViewModel { get; set; }
        public VatTuUC()
        {
            InitializeComponent();
            this.DataContext = ViewModel = new VatTuViewModel();
        }
    }
}