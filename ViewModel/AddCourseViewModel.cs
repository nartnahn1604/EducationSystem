using IT008_UIT.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IT008_UIT.ViewModel
{
    public class AddCourseViewModel : BaseViewModel
    {
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

        private string _courseName;

        public string CourseName
        {
            get { return  _courseName; }
            set {  _courseName = value; OnPropertyChanged(); }
        }
        private string _price;

        public string Price
        {
            get { return _price; }
            set 
            { 
                _price = value;
                
                OnPropertyChanged(); 
            }
        }
        private string _duration;

        public string Duration
        {
            get { return _duration; }
            set { _duration = value; OnPropertyChanged(); }
        }
        private string _email;

        public string Email
        {
            get { return _email; }
            set { _email = value; OnPropertyChanged(); }
        }
        private string _description;

       

        public string Description
        {
            get { return _description; }
            set { _description = value; OnPropertyChanged(); }
        }

        private GymDbContext Context { get; set; }

       

        public void AddNewCourse()
        {
            _ = AddNewCourseAsync();
        }

        public ObservableCollection<Course> Sync()
        {
            return CourseContext;
        }

        public Task AddNewCourseAsync()
        {
            return Task.Factory.StartNew(() =>
            {
                using (Context = new GymDbContext())
                {
                    if (CourseName != null && Price != null && Duration != null)
                    {
                        Course course = new Course();
                        course.Name = CourseName;
                        course.Price = Int32.Parse(Price);
                        course.Duration = Int32.Parse(Duration);
                        course.Description = Description;
                        course.Active = true;

                        Context.Add<Course>(course);
                        Context.SaveChanges();
                        CourseContext.Add(course);
                    }
                    else
                    {
                        Flag = false;
                    }
                    
                }
            });
        }

        private ObservableCollection<Course> CourseContext;
        public AddCourseViewModel(ObservableCollection<Course> _courseContext)
        {
            CourseContext = _courseContext;
        }
    }
}
