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
    class BookingViewModel : BaseViewModel
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
        private Ptcontract _selectedPtContract { get; set; }
        public Ptcontract SelectedPtContract
        {
            get => _selectedPtContract;
            set { _selectedPtContract = value; OnPropertyChanged(); }
        }
        public async Task LoadData(Pt Pt)
        {
            _ = LoadDataAsync(Pt);
        }

        public void AddNewBooking()
        {
            _ = AddNewBookingAsync();
        }

        private bool _flag = true;
        public bool Flag
        {
            get
            {
                return _flag;
            }
            set
            {
                _flag = value;
                OnPropertyChanged();
            }
        }
        public Task AddNewBookingAsync()
        {
            return Task.Factory.StartNew(() =>
            {
                using (Context = new GymDbContext())
                {
                    Ptcontract ptcontract = SelectedPtContract;
                    if(ptcontract!=null)
                    {
                        Booking booking = new()
                        {
                            Ptid= ptcontract.Ptid,
                            CustomerId=ptcontract.CustomerId,
                            PtcontractId = ptcontract.PtcontractId,
                            CreateDate= DateTime.Now
                        };

                        Context.Add<Booking>(booking);
                        Context.SaveChanges();
                    }
                    else
                    {
                        Flag = false;
                    }
                    //if (CourseName != null && Price != null && Duration != null)
                    //{
                    //    Course course = new Course();
                    //    course.Name = CourseName;
                    //    course.Price = Int32.Parse(Price);
                    //    course.Duration = Int32.Parse(Duration);
                    //    course.Description = Description;
                    //    course.Active = true;

                    //    Context.Add<Course>(course);
                    //    Context.SaveChanges();
                    //}
                    //else
                    //{
                    //    // Show a dialog
                    //}

                }
            });
        }

        private Task LoadDataAsync(Pt Pt)
        {
            return Task.Factory.StartNew(() =>
            {
                using (Context = new GymDbContext())
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
                    }
                    
            });
        }
        public BookingViewModel(Pt Pt)
        {
            _ptContractList = new ObservableCollection<Ptcontract>();
            _bookingList = new ObservableCollection<Booking>();
            BindingOperations.EnableCollectionSynchronization(PtContractList, _lockMutex);
            LoadData(Pt);
        }
    }
}

