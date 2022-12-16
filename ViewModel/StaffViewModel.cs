using IT008_UIT.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

namespace IT008_UIT.ViewModel
{
    public class StaffViewModel : BaseViewModel
    {
        //private StaffId _selectedCustomer { get; set; }
        //public StaffId SelectedCustomer
        //{
        //    get => _selectedCustomer;
        //    set { _selectedCustomer = value; OnPropertyChanged(); }
        //}
        //private GymDbContext Context { get; set; }

        //private ObservableCollection<StaffId> _customerContext { get; set; }

        //public ObservableCollection<StaffId> CustomerContext
        //{
        //    get => _customerContext;
        //    set { _customerContext = value; OnPropertyChanged(); }
        //}
        //public ICommand DeleteCommand { get; set; }
        //public ICommand GridChangeCommand { get; set; }

        //private object _lockMutex = new object();
        //private Task LoadDataAsync()
        //{
        //    return Task.Factory.StartNew(() =>
        //    {
        //        using (Context = new GymDbContext())
        //        {
        //            foreach (StaffId cus in Context.StaffIDs.ToList())
        //            {
        //                _customerContext.Add(cus);
        //            }
        //        }
        //    });
        //}
        //public async Task LoadData()
        //{
        //    await Task.Delay(1000);
        //    _ = LoadDataAsync();
        //}
        //private Task SearchDataAsync(String content)
        //{
        //    return Task.Factory.StartNew(() =>
        //    {
        //        using (Context = new GymDbContext())
        //        {
        //            //var customer = Context.Customers.Where(s => s.Name == content
        //            //    || s.IdentityNumber == content).ToList();
        //            //_customerContext.Clear();
        //            //foreach (StaffId cus in customer)
        //            //{
        //            //    _customerContext.Add(cus);
        //            //}

        //            //var stringProperties = typeof(StaffId).GetProperties().Where(prop =>
        //            //prop.PropertyType == content.GetType());
        //            //List<StaffId> List = Context.Customers.Where(customer => stringProperties.Any(prop =>
        //            //((prop.GetValue(customer, null) == null) ? "" :
        //            //prop.GetValue(customer, null).ToString().ToLower()) == content)).ToList();


        //            //_customerContext.Clear();
        //            //if (List != null)
        //            //{
        //            //    Debug.WriteLine("!= null");
        //            //    foreach (StaffId cus in List)
        //            //    {
        //            //        _customerContext.Add(cus);
        //            //    }
        //            //}
        //        }
        //    });
        //}
        //public override void SearchData(String content)
        //{
        //    _ = SearchDataAsync(content);
        //    Debug.WriteLine($"In search... for {content}");
        //}

        //private async Task Update(StaffId replace)
        //{
        //    using (Context = new GymDbContext())
        //    {
        //        try
        //        {
        //            StaffId current = Context.StaffIds.Where(t => t.StaffId1.Equals(replace.StaffId1)).FirstOrDefault();
        //            if (current != null)
        //            {
        //                if (!(current.Name == replace.Name &&
        //                    current.IdentityNumber == replace.IdentityNumber &&
        //                    current.Gender == replace.Gender &&
        //                    current.Phone == replace.Phone &&
        //                    current.Email == replace.Email &&
        //                    current.Birthday == replace.Birthday &&
        //                    current.Address == replace.Address &&
        //                    current.Active == replace.Active))
        //                {
        //                    current.Name = replace.Name;
        //                    current.IdentityNumber = replace.IdentityNumber;
        //                    current.Gender = replace.Gender;
        //                    current.Phone = replace.Phone;
        //                    current.Email = replace.Email;
        //                    current.Birthday = replace.Birthday;
        //                    current.Address = replace.Address;
        //                    current.Active = replace.Active;
        //                    Context.SaveChanges();
        //                    //_customerContext.Remove(current);
        //                    //_customerContext.Add(replace);
        //                    for (int i = 0; i < _customerContext.Count; i++)
        //                    {
        //                        if (_customerContext[i].StaffId1 == replace.StaffId1)
        //                            _customerContext[i] = replace;
        //                    }
        //                    //_customerContext.Clear();
        //                    //await LoadData(Flag);
        //                }
        //            }
        //            else await Add(replace);
        //        }
        //        catch (Exception)
        //        {
        //            throw;
        //        }

        //    }
        //}
        //private async Task Add(StaffId replace)
        //{
        //    using (Context = new GymDbContext())
        //    {
        //        Context.Add<StaffId>(replace);
        //        Context.SaveChanges();
        //        _customerContext.Add(replace);
        //    }
        //}

        //private async Task Delete(StaffId delete)
        //{
        //    using (Context = new GymDbContext())
        //    {
        //        Context.Remove<StaffId>(delete);
        //        Context.SaveChanges();
        //        _customerContext.Remove(delete);
        //    }
        //}
        public StaffViewModel()
        {
            //_customerContext = new ObservableCollection<StaffId>();
            //Debug.WriteLine("In Viewmodel...");
            //BindingOperations.EnableCollectionSynchronization(CustomerContext, _lockMutex);
            //LoadData();
            //GridChangeCommand = new RelayCommand<DataGrid>((p) => { return p == null ? false : true; }, (p) =>
            //{
            //    Debug.WriteLine("Here");
            //    StaffId replace = SelectedCustomer;
            //    try
            //    {
            //        Update(replace);
            //    }
            //    catch (Exception)
            //    {
            //        throw;
            //    }
            //});
            //DeleteCommand = new RelayCommand<object>((p) => { return p == null ? false : true; }, (p) =>
            //{
            //    var Result = MessageBox.Show("Are you sure?", "Delete StaffId", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            //    if (Result == MessageBoxResult.Yes)
            //    {
            //        StaffId delete = SelectedCustomer;
            //        try
            //        {
            //            Delete(delete);
            //        }
            //        catch (Exception)
            //        {
            //            throw;
            //        }
            //    }
            //}
            //);

        }


    }
}
