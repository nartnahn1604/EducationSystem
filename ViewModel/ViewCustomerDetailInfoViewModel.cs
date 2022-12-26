using IT008_UIT.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace IT008_UIT.ViewModel
{
    class ViewCustomerDetailInfoViewModel : BaseViewModel
    {
        private GymDbContext Context { get; set; }

        private object _lockMutex = new object();

        private ObservableCollection<Contract> _contractList { get; set; }

        public ObservableCollection<Contract> ContractList
        {
            get => _contractList;
            set { _contractList = value; OnPropertyChanged(); }
        }
        private ObservableCollection<Booking> _bookingList { get; set; }

        public ObservableCollection<Booking> BookingList
        {
            get => _bookingList;
            set { _bookingList = value; OnPropertyChanged(); }
        }

        private ObservableCollection<Ptcontract> _ptContractList { get; set; }

        public ObservableCollection<Ptcontract> PtContractList
        {
            get => _ptContractList;
            set { _ptContractList = value; OnPropertyChanged(); }
        }

        public async Task LoadData(Customer customer)
        {
            _ = LoadDataAsync(customer);
        }
        private Task LoadDataAsync(Customer customer)
        {
            return Task.Factory.StartNew(() =>
            {
                using(Context = new GymDbContext())
                {
                    foreach(Contract contract in customer.Contracts.ToList()) 
                    {
                        var course = Context.Courses.Where(s => s.CourseId == contract.CourseId).FirstOrDefault();
                        contract.Course = course;
                        _contractList.Add(contract);
                    }

                    foreach (Ptcontract con in customer.Ptcontracts.Where(s => s.CustomerId == customer.CustomerId).ToList())
                    {
                        var course = Context.Ptcourses.Where(s => s.PtcourseId == con.PtcourseId).FirstOrDefault();
                        var pt = Context.Pts.Where(s => s.Ptid == con.Ptid).FirstOrDefault();
                        con.Ptcourse = course;
                        con.Pt = pt;
                        _ptContractList.Add(con);
                        customer.Ptcontracts.Add(con);
                    }

                }
            });
        }
        public ViewCustomerDetailInfoViewModel(Customer customer)
        {
            _contractList = new ObservableCollection<Contract>();
            _ptContractList = new ObservableCollection<Ptcontract>();
            _bookingList = new ObservableCollection<Booking>();
            BindingOperations.EnableCollectionSynchronization(ContractList, _lockMutex);
            LoadData(customer);
        }
    }
}
