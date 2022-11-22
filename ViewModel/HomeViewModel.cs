using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IT008_UIT.ViewModel
{
    public class HomeViewModel : BaseViewModel
    {
        public class NhanVien
        {
            public string Name { get; set; }
            public string Role { get; set; }
        }

        public List<NhanVien> NhanVienList { get; set; }
        public HomeViewModel() 
        {
            NhanVienList = new List<NhanVien>();
            NhanVienList.Add(new NhanVien() { Name = "Tran Thanh Nhan", Role = "Quan Ly" });
            NhanVienList.Add(new NhanVien() { Name = "Tran Van Nhan", Role = "Nhan vien" });
        }


        
    }
}
