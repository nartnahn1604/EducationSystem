using IT008_UIT.Models;
using IT008_UIT.UserControlGym;
using IT008_UIT.Utils;
using LiveChartsCore.Geo;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
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
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Navigation;
using System.Xml.Linq;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace IT008_UIT.ViewModel
{
    public class CustomerViewModel : BaseViewModel
    {
        private GymDbContext Context { get; set; }
        private Customer _selectedCustomer { get; set;}
        public Customer SelectedCustomer
        {
            get => _selectedCustomer;
            set { _selectedCustomer = value; OnPropertyChanged(); }
        }

        private bool _isLoading;

        public bool IsLoading
        {
            get { return _isLoading; }
            set { _isLoading = value; OnPropertyChanged(nameof(IsLoading)); }
        }

        private ObservableCollection<Customer> _customerContext { get; set; }

        public ObservableCollection<Customer> CustomerContext
        {
            get => _customerContext;
            set { _customerContext = value; OnPropertyChanged(); }
        }
        public ICommand DeleteCommand { get; set; }
        public ICommand GridChangeCommand { get; set; }

        private object _lockMutex = new object();
        private Task LoadDataAsync()
        {
            return Task.Factory.StartNew(() =>
            {
                using (Context = new GymDbContext())
                {
                    foreach (Customer cus in Context.Customers.ToList())
                    {
                        _customerContext.Add(cus);
                    }
                }
            });
        }
        public async Task LoadData()
        {
            IsLoading= true;
            _ = LoadDataAsync();
            await Task.Delay(3500);
            IsLoading = false;
        }
        private Task SearchDataAsync(String content)
        {
            return Task.Factory.StartNew(() =>
            {
                using (Context = new GymDbContext())
                {
                    var customer = Context.Customers.Where(s => s.Name.Contains(content)
                        || s.IdentityNumber.Contains(content) || s.Gender.Contains(content) ||
                        s.Phone.Contains(content) || s.Email.Contains(content) ||
                        s.Address == content || s.Birthday.ToString().Contains(content)).ToList();

                    _customerContext.Clear();

                    foreach (Customer cus in customer)
                    {
                        Debug.WriteLine(cus.Birthday.ToString());
                        _customerContext.Add(cus);
                    }

                    //var stringProperties = typeof(Customer).GetProperties().Where(prop =>
                    //    prop.PropertyType == content.GetType());
                    //var customer = Context.Customers.Where(customer => 
                    //    stringProperties.Any(prop => 
                    //        prop.GetValue(customer, null) == content)).ToList();
                    //_customerContext.Clear();
                    //foreach (Customer cus in customer)
                    //{
                    //    _customerContext.Add(cus);
                    //}
                    //List<Customer> List = Context.Customers.Where(customer => stringProperties.Any(prop =>
                    //((prop.GetValue(customer, null) == null) ? "" :
                    //prop.GetValue(customer, null).ToString().ToLower()) == content)).ToList();


                    //_customerContext.Clear();
                    //if (List != null)
                    //{
                    //    Debug.WriteLine("!= null");
                    //    foreach (Customer cus in List)
                    //    {
                    //        _customerContext.Add(cus);
                    //    }
                    //}
                }
            });
        }
        public override void SearchData(String content)
        {
            _ = SearchDataAsync(content);
            Debug.WriteLine($"In search... for {content}");
        }

        private async Task Update(Customer replace)
        {
            using (Context = new GymDbContext())
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
                            current.Birthday == replace.Birthday &&
                            current.Address == replace.Address &&
                            current.Active == replace.Active))
                        {
                            current.Name = replace.Name;
                            current.IdentityNumber = replace.IdentityNumber;
                            current.Gender = replace.Gender;
                            current.Phone = replace.Phone;
                            current.Email = replace.Email;
                            current.Birthday = replace.Birthday;
                            current.Address = replace.Address;
                            current.Active = replace.Active;
                            Context.SaveChanges();
                            //_customerContext.Remove(current);
                            //_customerContext.Add(replace);
                            for (int i = 0; i < _customerContext.Count; i++)
                            {
                                if (_customerContext[i].CustomerId == replace.CustomerId)
                                    _customerContext[i] = replace;
                            }
                            //_customerContext.Clear();
                            //await LoadData(Flag);
                        }
                    }
                    else await Add(replace);
                }
                catch (Exception)
                {
                    throw;
                }

            }
        }
        private async Task Add(Customer replace)
        {
            using (Context = new GymDbContext())
            {
                Context.Add<Customer>(replace);
                Context.SaveChanges();
                _customerContext.Add(replace);
            }
        }

        private async Task Delete(Customer delete)
        {
            using (Context = new GymDbContext())
            {
                Context.Remove<Customer>(delete);
                Context.SaveChanges();
                _customerContext.Remove(delete);
            }
        }
        public CustomerViewModel()
        {
            _customerContext = new ObservableCollection<Customer>();
            Debug.WriteLine("In Viewmodel...");
            BindingOperations.EnableCollectionSynchronization(CustomerContext, _lockMutex);
            LoadData();
            GridChangeCommand = new RelayCommand<DataGrid>((p) => { return p == null ? false : true; }, (p) =>
            {
                Debug.WriteLine("Here");
                Customer replace = SelectedCustomer;
                try
                {
                    Update(replace);
                }
                catch (Exception)
                {
                    throw;
                }
            });
            DeleteCommand = new RelayCommand<object>((p) => { return p == null ? false : true; }, (p) =>
            {
                var Result = MessageBox.Show("Are you sure?", "Delete Customer", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (Result == MessageBoxResult.Yes)
                {
                    Customer delete = SelectedCustomer;
                    try
                    {
                        Delete(delete);
                    }
                    catch (Exception)
                    {
                        throw;
                    }
                }
            }
            );

        }


    }
}

