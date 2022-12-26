using IT008_UIT.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace IT008_UIT.ViewModel
{
    public class AddStaffViewModel : BaseViewModel
    {
        private GymDbContext Context { get; set; }
        private List<string> _roleList { get; set; }

        public List<string> RoleList
        {
            get => _roleList;
            set { _roleList = value; OnPropertyChanged(); }
        }
        private string _selectedRole { get; set; }
        public string SelectedRole
        {
            get => _selectedRole;
            set { _selectedRole = value; OnPropertyChanged(); }
        }

        private DateTime _birthDay { get; set; }

        public DateTime BirthDay
        {
            get { return _birthDay; }
            set { _birthDay = value; OnPropertyChanged(); }
        }

        private string _staffname;

        public string StaffName
        {
            get { return _staffname; }
            set { _staffname = value; OnPropertyChanged(); }
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

        private bool _active;

        public bool Active
        {
            get { return _active; }
            set { _active = true; OnPropertyChanged(); }
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



        public void AddNewStaff()
        {
            _ = AddNewStaffAsync();
        }

        public Task AddNewStaffAsync()
        {
            return Task.Factory.StartNew(() =>
            {
                using (Context = new GymDbContext())
                {
                    var Role = Context.Roles.Where(s => s.Name == SelectedRole).FirstOrDefault();

                    if (Role != null)
                    {
                        // This will validate whether a Staff have exists ?




                        //// Add new staff
                        Staff staff = new()
                        {
                            Name = StaffName,
                            Role = Role,
                            RoleId = Role.RoleId,
                            IdentityNumber = IdentityNumber,
                            Phone = Phone,
                            Email = Email,
                            Gender = Gender,
                            Birthday = BirthDay,
                            Address = Address,
                            Active = true,
                        };
                        

                        Context.Add<Staff>(staff);
                        Context.SaveChanges();
                        StaffContext.Add(staff);
                    }
                    else
                    {
                        Flag = false;
                    }
                }
            });
        }

        private Task LoadDataAsync(List<string> roleList)
        {
            return Task.Factory.StartNew(() =>
            {
                _roleList = roleList;
            });
        }

        public async Task LoadData(List<string> roleList)
        {
            _ = LoadDataAsync(roleList);
        }

        public ObservableCollection<Staff> Sync()
        {
            return StaffContext;
        }

        private ObservableCollection<Staff> StaffContext;
        public AddStaffViewModel(ObservableCollection<Staff> _staffContext,List<string> roleList) 
        {
            StaffContext = _staffContext;
            LoadData(roleList);
        }
    }
}
