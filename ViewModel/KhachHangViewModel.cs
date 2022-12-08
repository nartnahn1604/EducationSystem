using IT008_UIT.Models;
using IT008_UIT.UserControlGym;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Navigation;

namespace IT008_UIT.ViewModel
{
    public class KhachHangViewModel : BaseViewModel
    {
        private Customer _selectedCustomer { get; set; }    
        public Customer SelectedCustomer 
        { 
            get => _selectedCustomer;
            set { _selectedCustomer = value; OnPropertyChanged(); }
        }
        private dbGymContext Context { get; set; }

        private List<Customer> _customerContext;

        public List<Customer> CustomerContext {
            get => _customerContext;
            set { _customerContext = value; OnPropertyChanged(); }
        }

        

        public ICommand DeleteCommand { get; set; }
        public ICommand GridChangeCommand { get; set; }
       
        private void Loaded()
        {
             using (Context = new dbGymContext())
            {
                _customerContext =  new List<Customer>(Context.Customers.ToList());
            }
        }

        public void Search(String content)
        {
            CustomerContext = Context.Customers.Where(s => s.Phone == content).ToList();
        }
        private void Update(Customer replace)
        {
            using (Context = new dbGymContext())
            {
                try
                {
                    Customer current = Context.Customers.Where(t => t.CustomerId.Equals(replace.CustomerId)).FirstOrDefault();
                    if (current != null)
                    {
                        if (!(current.Name == replace.Name &&
                            current.IdentityNumber == replace.IdentityNumber &&
                            current.Gender == replace.Gender &&
                            current.Phone == replace.Phone &&
                            current.Email == replace.Email &&
                            current.Address == replace.Address &&
                            current.Active == replace.Active))
                            {
                                current.Name = replace.Name;
                                current.IdentityNumber = replace.IdentityNumber;
                                current.Gender = replace.Gender;
                                current.Phone = replace.Phone;
                                current.Email = replace.Email;
                                current.Address = replace.Address;
                                current.Active = replace.Active;
                                Context.SaveChanges();
                                CustomerContext = Context.Customers.ToList();
                            }
                    }
                    else Add(replace);
                }
                catch (Exception)
                {
                    throw;
                }

            }
        }
        private void Add(Customer replace)
        {  
            using (Context = new dbGymContext())
            {
                Context.Add<Customer>(replace);
                Context.SaveChanges();
                CustomerContext = Context.Customers.ToList();
            }
        }

       
        public  KhachHangViewModel()
        {
            Loaded();
            GridChangeCommand = new RelayCommand<DataGrid>((p) => { return p == null ? false : true; }, (p) =>
            {
                Customer replace = SelectedCustomer ;
                if (replace != null)
                {
                    Update(replace);
                    //p.Items.Refresh();
                }
                else Debug.WriteLine("object == null");
            });
            DeleteCommand = new RelayCommand<object>((p) => { return p == null ? false : true; }, (p) =>
            {
                var Result = MessageBox.Show("Are you sure?", "Delete Customer",MessageBoxButton.YesNo,MessageBoxImage.Warning);
            }
            );
            
        }

        
    }
}

