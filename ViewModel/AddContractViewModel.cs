using IT008_UIT.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Xml.Linq;

namespace IT008_UIT.ViewModel
{
    internal class AddContractViewModel : BaseViewModel
    {
        
        private DateTime _createDate { get; set; }

        public DateTime CreateDate 
        {
            get { return _createDate; }
            set { _createDate = value; OnPropertyChanged(); }
        }

        private DateTime _finishDate { get; set; }

        public DateTime FinishDate
        {
            get { return _finishDate; }
            set 
            {
                _finishDate = value;
                //_finishDate.AddDays(Course.Duration);
                OnPropertyChanged(); 
            }
        }


        private string _customername;

        public string CustomerName
        {
            get { return _customername; }
            set { _customername = value; OnPropertyChanged(); }
        }

        private string _coursename;

        public string CourseName
        {
            get { return _coursename; }
            set { _coursename = value; OnPropertyChanged(); }
        }


        private string _selectedCourse { get; set; }
        public string SelectedCourse
        {
            get => _selectedCourse;
            set { _selectedCourse = value; OnPropertyChanged(); }
        }

        private ObservableCollection<Course> _courseContext { get; set; }

        public ObservableCollection<Course> CourseContext
        {
            get => _courseContext;
            set { _courseContext = value; OnPropertyChanged(); }
        }

        private List<string> _courseList { get; set; }

        public List<string> CourseList
        {
            get => _courseList;
            set { _courseList = value; OnPropertyChanged(); }
        }

        private string _phone;

        public string Phone
        {
            get { return _phone; }
            set { _phone = value; OnPropertyChanged(); }
        }


        private string _address;

        public string Address
        {
            get { return _address; }
            set { _address = value; OnPropertyChanged(); }
        }

        private string _email;

        public string Email
        {
            get { return _email; }
            set { _email = value; OnPropertyChanged(); }
        }

        private string _identitynumber;

        public string IdentityNumber
        {
            get { return _identitynumber; }
            set { _identitynumber = value; OnPropertyChanged(); }
        }

        private string _description;

        public string Description
        {
            get { return _description; }
            set { _description = value; OnPropertyChanged(); }
        }

        private bool _active;

        public bool Active
        {
            get { return _active; }
            set { _active = true; OnPropertyChanged(); }
        }
        private ObservableCollection<Customer> _customerContext { get; set; }

        public ObservableCollection<Customer> CustomerContext
        {
            get => _customerContext;
            set { _customerContext = value; OnPropertyChanged(); }
        }

        private ObservableCollection<Contract> _contractContext { get; set; }

        public ObservableCollection<Contract> ContractContext
        {
            get => _contractContext;
            set { _contractContext = value; OnPropertyChanged(); }
        }
        private GymDbContext Context { get; set; }
        public Contract GetData()
        {
            using(Context = new GymDbContext())
            {
                Customer customer = new Customer();
                customer.Name = CustomerName;
                customer.IdentityNumber = IdentityNumber;
                customer.Phone = Phone;
                customer.Email = Email;
                customer.Address = Address;
                customer.Active = true;

                Context.Add<Customer>(customer);
                Context.SaveChanges();
                _customerContext.Add(customer);

                var course = Context.Courses.Where(s => s.Name == SelectedCourse).FirstOrDefault();
                
                Contract contract = new Contract();
                contract.Description = Description;
                contract.Course = course;
                contract.CourseId = course.CourseId;
                contract.Customer = customer;

                CreateDate = DateTime.Now;

                contract.CreateDate = CreateDate;
                contract.FinishDate = CreateDate.AddDays((double)contract.Course.Duration);
                contract.Active = true;

                Context.Add<Contract>(contract);
                Context.SaveChanges();
                _contractContext.Add(contract);


                return contract;
            }
        }
        private Task LoadDataAsync()
        {
            return Task.Factory.StartNew(() =>
            {
                using (Context = new GymDbContext())
                {
                    foreach (Course cor in Context.Courses.ToList())
                    {
                        _courseContext.Add(cor);
                        _courseList.Add(cor.Name);
                    }
                }
            });
        }
        public async Task LoadData()
        {
            _ = LoadDataAsync();
           
        }
        private object _lockMutex = new object();
        public AddContractViewModel()
        {
            _courseContext = new ObservableCollection<Course>();
            _customerContext = new ObservableCollection<Customer>();
            _contractContext = new ObservableCollection<Contract>();
            Debug.WriteLine("In HopDongViewmodel...");
            BindingOperations.EnableCollectionSynchronization(CourseContext, _lockMutex);
            LoadData();
            _courseList = new List<string>();
        }
    }
}
