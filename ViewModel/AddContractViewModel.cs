using IT008_UIT.Models;
using IT008_UIT.UserControlGym;
using MaterialDesignThemes.Wpf;
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
    public class AddContractViewModel : BaseViewModel
    {
        
        private DateTime _createDate { get; set; }

        public DateTime CreateDate 
        {
            get { return _createDate; }
            set { _createDate = value; OnPropertyChanged(); }
        }
        private DateTime _birthDay { get; set; }

        public DateTime BirthDay
        {
            get { return _birthDay; }
            set { _birthDay = value; OnPropertyChanged(); }
        }

        private DateTime _finishDate { get; set; }

        public DateTime FinishDate
        {
            get { return _finishDate; }
            set 
            {
                _finishDate = value;
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

        private string _selectedPt { get; set; }
        public string SelectedPt
        {
            get => _selectedPt;
            set { _selectedPt = value; OnPropertyChanged(); }
        }


        private List<string> _courseList { get; set; }


        public List<string> CourseList
        {
            get => _courseList;
            set { _courseList = value; OnPropertyChanged(); }
        }

       

        private List<string> _ptCourseList { get; set; }

        public List<string> PtCourseList
        {
            get => _ptCourseList;
            set { _ptCourseList = value; OnPropertyChanged(); }
        }

        private List<string> _normalcourseList { get; set; }
        public List<string> NormalCourseList
        {
            get => _normalcourseList;
            set { _normalcourseList = value; OnPropertyChanged(); }
        }

        private List<string> _ptList { get; set; }

        public List<string> PtList
        {
            get => _ptList;
            set { _ptList = value; OnPropertyChanged(); }
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

        private string _gender;

        public string Gender
        {
            get { return _gender; }
            set { _gender = value; OnPropertyChanged(); }
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

        private bool _isPtContract;

        public bool IsPtContract
        {
            get { return _isPtContract; }
            set { _isPtContract = value;
                OnPropertyChanged(nameof(IsPtContract)); }
        }

        private string _defaultContractValue;
        public string DefaultContractValue
        {
            get { return _defaultContractValue; }
            set 
            { 
                _defaultContractValue = value; 
                if(value == ptcontract)
                {
                    CourseList = _ptCourseList;
                    IsPtContract = true;
                } else
                {
                    CourseList = _normalcourseList;
                    IsPtContract = false;
                }
                OnPropertyChanged(); 
            }
        }

        
        public List<string> ContractList { get; }
       
        private GymDbContext Context { get; set; }

       
        private Customer CreateNewCustomer(GymDbContext Context)
        {
            // Add new customer
            Customer newcustomer = new Customer();
            newcustomer.Name = CustomerName;
            newcustomer.IdentityNumber = IdentityNumber;
            newcustomer.Phone = Phone;
            newcustomer.Email = Email;
            newcustomer.Gender = Gender;
            newcustomer.Birthday = BirthDay;
            newcustomer.Address = Address;
            newcustomer.Active = true;

            Context.Add<Customer>(newcustomer);
            Context.SaveChanges();

            return newcustomer;
        }

        private void CreateNormalContract(GymDbContext Context, Course course, Customer customer)
        {
            // Add new contract
            Contract contract = new Contract();
            contract.Description = Description;
            contract.CustomerId = customer.CustomerId;
            contract.CourseId = course.CourseId;
            contract.Customer = customer;
            contract.Active = true;

            // Auto generated Date
            CreateDate = DateTime.Now;
            contract.CreateDate = CreateDate;
            contract.FinishDate = CreateDate.AddDays((double)course.Duration);

            Context.Add<Contract>(contract);
            Context.SaveChanges();
            ContractContext.Add(contract);
            // Add a new contract to Customer Table
        }

        private void CreatePtContract(GymDbContext Context, Ptcourse ptcourse, Pt pt, Customer customer)
        {
            // Add new contract
            Ptcontract contract = new Ptcontract();
            contract.Description = Description;
            contract.CustomerId = customer.CustomerId;
            contract.PtcourseId = ptcourse.PtcourseId;
            contract.Customer = customer;
            contract.Ptid = pt.Ptid;
            contract.Active = true;

            // Auto generated Date
            CreateDate = DateTime.Now;
            contract.CreateDate = CreateDate;
            contract.FinishDate = CreateDate.AddDays((double)ptcourse.Duration);

            Context.Add<Ptcontract>(contract);
            Context.SaveChanges();
            PtContractContext.Add(contract);
        }
        

        public void AddNewContract()
        {
             _ =  AddNewContractAsync();
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

        public Task AddNewContractAsync()
        {
            return Task.Factory.StartNew(() =>
            {
                using (Context = new GymDbContext())
                {


                    // validate existed customer
                    var oldcustomer = Context.Customers.Where(s => s.Name == CustomerName
                                            && s.Phone == Phone).FirstOrDefault();

                    // this is for a normal contract
                    if (DefaultContractValue == normalcontract)
                    {

                        var course = Context.Courses.Where(s => s.Name == SelectedCourse).FirstOrDefault();


                        if (course != null && CustomerName != null && Phone != null)
                        {
                            // cutomer existed
                            if (oldcustomer != null)
                            {
                                CreateNormalContract(Context, course, oldcustomer);
                            }

                            // new customer
                            else
                            {
                                if (Email != null && IdentityNumber != null)
                                {
                                    // Create new customer
                                    var newcustomer = CreateNewCustomer(Context);

                                    // Add new normal contract
                                    CreateNormalContract(Context, course, newcustomer);
                                    
                                }
                                else
                                {
                                    Flag = false;

                                }
                            }
                        }
                        else
                        {
                            Flag = false;
                        }
                    }

                    // this is a pt contract

                    else if (DefaultContractValue == ptcontract)
                    {
                        var course = Context.Ptcourses.Where(s => s.Name == SelectedCourse).FirstOrDefault();
                        var pt = Context.Pts.Where(s => s.Name == SelectedPt).FirstOrDefault();

                        if (course != null && pt != null && CustomerName != null && Phone != null)
                        {
                            // cutomer existed
                            if (oldcustomer != null)
                            {
                                CreatePtContract(Context, course, pt, oldcustomer);
                            }

                            // new customer
                            else
                            {
                                if (Email != null && IdentityNumber != null)
                                {

                                    var newcustomer = CreateNewCustomer(Context);

                                    // Add new normal contract
                                    CreatePtContract(Context, course, pt, newcustomer);
                                }
                                else
                                {
                                    Flag = false;
                                }
                            }
                        }
                    }
                }
                
            });
        }

        private Task LoadDataAsync(List<string> normalcouresList, List<string> ptcourseList, List<string> ptList)
        {
            return Task.Factory.StartNew(() =>
            {
                if(DefaultContractValue == normalcontract)
                {
                    _courseList = normalcouresList;
                } 
                else if(DefaultContractValue == ptcontract)
                {
                    _courseList = ptcourseList;
                }
                _normalcourseList = normalcouresList;
                _ptList = ptList;
                _ptCourseList = ptcourseList;
            });
        }

        public async Task LoadData(List<string> normalcouresList, List<string> ptcourseList, List<string> ptList)
        {
            _ = LoadDataAsync(normalcouresList, ptcourseList, ptList);
        }

        public ObservableCollection<Contract> NormalSync()
        {
            return ContractContext;
        }
        public ObservableCollection<Ptcontract> PtSync()
        {
            return PtContractContext;
        }


        private const string ptcontract = "Hợp đồng PT";
        private const string normalcontract = "Hợp đồng cơ bản";
        private ObservableCollection<Contract> ContractContext;
        private ObservableCollection<Ptcontract> PtContractContext;
        public AddContractViewModel(ObservableCollection<Contract> _contractContext, ObservableCollection<Ptcontract> _ptcontractContext, List<string> normalcouresList, List<string>  ptcourseList, List<string>  ptList)
        {
            PtContractContext = _ptcontractContext;
            ContractContext = _contractContext;
            ContractList = new List<string>()
            {
               normalcontract, ptcontract
            };

            DefaultContractValue = ContractList.First();
            IsPtContract = false;
            LoadData(normalcouresList, ptcourseList, ptList);
            
        }
    }
}
