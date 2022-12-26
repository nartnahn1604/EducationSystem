using IT008_UIT.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace IT008_UIT.ViewModel
{
    class ViewPtDetailInfoViewModel : BaseViewModel
    {
        private GymDbContext Context { get; set; }

        private object _lockMutex = new object();

        
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

        public async Task LoadData(Pt Pt)
        {
            _ = LoadDataAsync(Pt);
        }
        private Task LoadDataAsync(Pt Pt)
        {
            return Task.Factory.StartNew(() =>
            {
                using (Context = new GymDbContext())
                {
                    try
                    {
                        foreach (Ptcontract con in Pt.Ptcontracts.Where(s => s.Ptid == Pt.Ptid).ToList())
                        {
                            var course = Context.Ptcourses.Where(s => s.PtcourseId == con.PtcourseId).FirstOrDefault();
                            var customer = Context.Customers.Where(s => s.CustomerId == con.CustomerId).FirstOrDefault();
                            con.Ptcourse = course;
                            con.Customer = customer;
                            _ptContractList.Add(con);
                            Pt.Ptcontracts.Add(con);
                        }
                    }catch(Exception ex) 
                    {

                    }
                }
            });
        }
        public ViewPtDetailInfoViewModel(Pt Pt)
        {
            _ptContractList = new ObservableCollection<Ptcontract>();
            _bookingList = new ObservableCollection<Booking>();
            BindingOperations.EnableCollectionSynchronization(PtContractList, _lockMutex);
            LoadData(Pt);
        }
    }
}
