using IT008_UIT.Models;
using IT008_UIT.UserControlGym;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace IT008_UIT.ViewModel
{
    public class KhachHangViewModel : BaseViewModel
    {

        public dbGymContext Context { get; set; }
     
        private List<Customer> _customerContext;

        public List<Customer> CustomerContext { 
            get => _customerContext; 
            set {  _customerContext = value;  OnPropertyChanged(); } 
        }

        public ICommand TestCommand { get; set; }
        //public ObservableCollection<Customer> Cus;
        private  void Loaded()
        {
           _customerContext = new List<Customer>(Context.Customers.ToList());
            Debug.WriteLine("Private: " + _customerContext.ToString());
            Debug.WriteLine("Binding: "+ CustomerContext.ToString());
        }

        public void Search(String content)
        {
            CustomerContext = Context.Customers.Where(s => s.Phone == content).ToList();
        }
        public void Added()
        {
            var customer = new Customer()
            {
                Name = "Võ Nhật Thanh",
                IdentityNumber = "09031311112",
                Gender = "Nam",
                Phone = "0919835312",
                Email = "nhatthanh@gmail.com",
                Address = "Ca Mau tan cung Viet Nam",
                Avatar = null,
                Active = false
            };
            Context.Add<Customer>(customer);
            Context.SaveChanges();
            CustomerContext = Context.Customers.ToList();
        }


        public KhachHangViewModel()
        {
            Context = new dbGymContext();
            Loaded();
            TestCommand = new RelayCommand<UserControl>((p) => { return p == null ? false : true; }, (p) =>
            {
                Search("0902344835");
                Debug.WriteLine("BTN CLick");
            }
            );
        }
    }
}

