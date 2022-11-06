using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Drawing;
using System.Windows.Media;

namespace IT008_UIT
{
    /// <summary>
    /// Interaction logic for HopDongCoBanPage.xaml
    /// </summary>
    /// 

    //Global variables
    public partial class HopDongCoBanPage : Page
    {
        private string[,] GoiTap = new string[2, 5] { { "1 tháng", "4 tháng", "6 tháng", "9 tháng", "1 năm" }, {"red", "green", "blue", "purple", "yellow" } }; 
        public HopDongCoBanPage()
        {
            InitializeComponent();
            Goi0.planTxt.Text = GoiTap[0, 0];
            Goi0.GoiTapC.Background = new SolidColorBrush(cvColor(System.Drawing.Color.FromName(GoiTap[0, 0])));
            Goi1.planTxt.Text = GoiTap[0, 1];
            Goi1.GoiTapC.Background = new SolidColorBrush(cvColor(System.Drawing.Color.FromName(GoiTap[0, 1])));
            Goi2.planTxt.Text = GoiTap[0, 2];
            Goi2.GoiTapC.Background = new SolidColorBrush(cvColor(System.Drawing.Color.FromName(GoiTap[0, 2])));
        }

        public System.Windows.Media.Color cvColor(System.Drawing.Color oldColor)
        {
            return System.Windows.Media.Color.FromArgb(oldColor.A, oldColor.R, oldColor.G, oldColor.B);
        }
        private void PrevBtn_Click(object sender, RoutedEventArgs e)
        {
            int curInd = Array.IndexOf(GoiTap, Goi0.planTxt.Text);
            Debug.WriteLine(curInd);
            if(curInd == GoiTap.Length - 3)
            {
                curInd++;
                Goi0.planTxt.Text = GoiTap[0, curInd];
                Goi0.GoiTapC.Background = new SolidColorBrush(cvColor(System.Drawing.Color.FromName(GoiTap[0, curInd])));
                Goi1.planTxt.Text = GoiTap[0, curInd + 1];
                Goi1.GoiTapC.Background = new SolidColorBrush(cvColor(System.Drawing.Color.FromName(GoiTap[0, curInd + 1])));
                Goi2.planTxt.Text = GoiTap[0, 0];
                Goi2.GoiTapC.Background = new SolidColorBrush(cvColor(System.Drawing.Color.FromName(GoiTap[0, 0])));
            }
            else
                if(curInd == GoiTap.Length - 2)
                {
                    curInd++;
                    Goi0.planTxt.Text = GoiTap[0, curInd];
                    Goi0.GoiTapC.Background = new SolidColorBrush(cvColor(System.Drawing.Color.FromName(GoiTap[0, curInd])));
                    Goi1.planTxt.Text = GoiTap[0, 0];
                    Goi1.GoiTapC.Background = new SolidColorBrush(cvColor(System.Drawing.Color.FromName(GoiTap[0, 0])));
                    Goi2.planTxt.Text = GoiTap[0, 1];
                    Goi2.GoiTapC.Background = new SolidColorBrush(cvColor(System.Drawing.Color.FromName(GoiTap[0, 1])));
                }
                else
                {
                    if (curInd == GoiTap.Length - 1)
                    {
                        curInd = 0;
                    }
                    else
                        curInd++;
                    Goi0.planTxt.Text = GoiTap[0, curInd];
                    Goi0.GoiTapC.Background = new SolidColorBrush(cvColor(System.Drawing.Color.FromName(GoiTap[0, curInd])));
                    Goi1.planTxt.Text = GoiTap[0, curInd + 1];
                    Goi1.GoiTapC.Background = new SolidColorBrush(cvColor(System.Drawing.Color.FromName(GoiTap[0, curInd + 1])));
                    Goi2.planTxt.Text = GoiTap[0, curInd + 2];
                    Goi2.GoiTapC.Background = new SolidColorBrush(cvColor(System.Drawing.Color.FromName(GoiTap[0, curInd + 2])));
                }
        }

        private void NextBtn_Click(object sender, RoutedEventArgs e)
        {
            int curInd = Array.IndexOf(GoiTap, Goi2.planTxt.Text);
            int countGoi = GoiTap.Length;
            Debug.WriteLine(curInd);
            if (curInd == 2)
            {
                curInd--;
                Goi2.planTxt.Text = GoiTap[0, curInd];
                Goi1.planTxt.Text = GoiTap[0, curInd - 1];
                Goi0.planTxt.Text = GoiTap[0, countGoi - 1];
            }
            else
                if (curInd == 1)
            {
                curInd--;
                Goi2.planTxt.Text = GoiTap[0, curInd];
                Goi1.planTxt.Text = GoiTap[0, countGoi - 1];
                Goi0.planTxt.Text = GoiTap[0, countGoi - 2];
            }
            else
            {
                if (curInd == 0)
                {
                    curInd = countGoi - 1;
                }
                else
                    curInd--;
                Goi2.planTxt.Text = GoiTap[0, curInd];
                Goi1.planTxt.Text = GoiTap[0, curInd - 1];
                Goi0.planTxt.Text = GoiTap[0, curInd - 2];
            }
        }
    }
}
