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
        private string[,] GoiTap = new string[2, 5] { { "1 tháng", "4 tháng", "6 tháng", "9 tháng", "1 năm" }, { "red", "green", "blue", "purple", "brown" } };
        public HopDongCoBanPage()
        {
            InitializeComponent();
            changeSlider(new int[3]{ 0, 1, 2});
        }

        public void changeSlider(int[] curInd)
        {
            Goi0.planTxt.Text = GoiTap[0, curInd[0]];
            Goi0.GoiTapC.Background = new SolidColorBrush(cvColor(System.Drawing.Color.FromName(GoiTap[1, curInd[0]])));
            Goi1.planTxt.Text = GoiTap[0, curInd[1]];
            Goi1.GoiTapC.Background = new SolidColorBrush(cvColor(System.Drawing.Color.FromName(GoiTap[1, curInd[1]])));
            Goi2.planTxt.Text = GoiTap[0, curInd[2]];
            Goi2.GoiTapC.Background = new SolidColorBrush(cvColor(System.Drawing.Color.FromName(GoiTap[1, curInd[2]])));
        }
        public System.Windows.Media.Color cvColor(System.Drawing.Color oldColor)
        {
            return System.Windows.Media.Color.FromArgb(oldColor.A, oldColor.R, oldColor.G, oldColor.B);
        }
        private void PrevBtn_Click(object sender, RoutedEventArgs e)
        {
            int[] curInd = new int[3] { 0, 1, 2 };
            for(int i = 0; i < GoiTap.GetLength(1); i++)
                if(GoiTap[0, i] == Goi0.planTxt.Text)
                {
                    Debug.WriteLine(curInd[0]);
                    curInd[0] = i;
                    break;
                }
            if (curInd[0] == GoiTap.GetLength(1) - 3)
            {
                curInd[0]++;
                curInd[1] = curInd[0] + 1;
                curInd[2] = 0;
            }
            else
                if (curInd[0] == GoiTap.GetLength(1) - 2)
            {
                curInd[0]++;
                curInd[1] = 0;
                curInd[2] = 1;
            }
            else
            {
                if (curInd[0] == GoiTap.GetLength(1) - 1)
                {
                    curInd[0] = 0;
                }
                else
                    curInd[0]++;
                curInd[1] = curInd[0] + 1;
                curInd[2] = curInd[0] + 2;
            }
            changeSlider(curInd);
        }

        private void NextBtn_Click(object sender, RoutedEventArgs e)
        {
            int[] curInd = new int[3] { 0, 1, 2 };
            for (int i = 0; i < GoiTap.GetLength(1); i++)
                if (GoiTap[0, i] == Goi0.planTxt.Text)
                {
                    Debug.WriteLine(curInd[0]);
                    curInd[0] = i;
                    break;
                }
            int countGoi = GoiTap.GetLength(1);
            if (curInd[0] == 2)
            {
                curInd[0]--;
                curInd[1] = curInd[0] - 1;
                curInd[2] = countGoi - 1;
            }
            else
                if (curInd[0] == 1)
            {
                curInd[0]--;
                curInd[1] = countGoi - 1;
                curInd[2] = countGoi - 2;
            }
            else
            {
                if (curInd[0] == 0)
                {
                    curInd[0] = countGoi - 1;
                }
                else
                    curInd[0]--;
                curInd[1] = curInd[0] - 1;
                curInd[2] = curInd[0] - 2;
            }
            changeSlider(curInd);
        }
    }
}
