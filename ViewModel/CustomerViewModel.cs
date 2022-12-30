using IT008_UIT.Models;
using IT008_UIT.UserControlGym;
using IT008_UIT.Utils;
using LiveChartsCore.Geo;
using MaterialDesignThemes.Wpf;
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
        public ICommand ViewCommand { get; set; }

        private object _lockMutex = new object();
        

        private Task LoadDataAsync()
        {
            return Task.Factory.StartNew(() =>
            {
                using (Context = new GymDbContext())
                {
                    foreach (Customer cus in Context.Customers.Include(s => s.Contracts)
                    .Include(s=>s.Bookings).Include(s => s.Ptcontracts).ToList())
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

        public override async void AddData()
        {
            if (IsLoading == false)
            {
                await ShowErrorDialog();
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

        private bool DeleteFlag;
        private async Task Delete(Customer delete)
        {
            using (Context = new GymDbContext())
            {
                var v = Context.Contracts.Where(s => s.CustomerId == delete.CustomerId).FirstOrDefault();
                if (v == null)
                {
                    DeleteFlag = true;
                    Context.Remove<Customer>(delete);
                    Context.SaveChanges();
                    _customerContext.Remove(delete);
                }
                else
                {
                    DeleteFlag = false;
                }
                
            }
        }

        private void ExtendedOpenedEventHandler(object sender, DialogOpenedEventArgs eventArgs)
          => Debug.WriteLine("You could intercept the open and affect the dialog using eventArgs.Session.");

        private void ExtendedClosedEventHandler(object sender, DialogClosedEventArgs eventArgs)
            => Debug.WriteLine("You could intercept the closed event here (2).");

        private async void ExtendedClosingEventHandler(object sender, DialogClosingEventArgs eventArgs)
        {
            Debug.WriteLine("You can intercept the closing event, cancel it, and do our own close after a little while.");
            if (eventArgs.Parameter is bool parameter &&
                parameter == false) return;


            var view = eventArgs.Session.Content as ViewCustomerDetailInfoUC;
            if (view == null)
            {
                var view1 = eventArgs.Session.Content as SampleConfirmDialog;
                if(view1 != null)
                {
                    eventArgs.Cancel();
                    Customer delete = SelectedCustomer;
                    try
                    {
                        await Delete(delete);
                    }
                    catch (Exception)
                    {
                        throw;
                    }
                    if (DeleteFlag == false)
                    {
                        eventArgs.Session.UpdateContent(new SampleErrorDialog());
                    }
                    else
                    {
                        Task.Delay(TimeSpan.FromSeconds(0))
                           .ContinueWith((t, _) => eventArgs.Session.Close(false), null,
                               TaskScheduler.FromCurrentSynchronizationContext());
                    }
                }
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
                var view = new SampleConfirmDialog();
                var result = DialogHost.Show(view, "RootDialog", ExtendedClosingEventHandler);
                //if (result.)
                //{
                //    var Result = MessageBox.Show("Are you sure?", "Delete Customer", MessageBoxButton.YesNo, MessageBoxImage.Warning);

                //}
                //var Result = MessageBox.Show("Are you sure?", "Delete Customer", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                //if (Result == MessageBoxResult.Yes)
                //{
                //    Customer delete = SelectedCustomer;
                //    try
                //    {
                //        Delete(delete);
                //    }
                //    catch (Exception)
                //    {
                //        throw;
                //    }
                //}
            }
            );
            ViewCommand = new RelayCommand<object>((p) => { return p == null ? false : true; }, async (p) =>
            {
                Customer customer = SelectedCustomer;
                var view = new ViewCustomerDetailInfoUC
                {
                    DataContext = new ViewCustomerDetailInfoViewModel(customer)
                };
                var result = await DialogHost.Show(view, "RootDialog", ExtendedOpenedEventHandler, ExtendedClosingEventHandler, ExtendedClosedEventHandler);

            }
           );

        }


    }
}

