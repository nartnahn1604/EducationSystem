using IT008_UIT.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IT008_UIT.ViewModel
{
    public class AddPtcourseViewModel : BaseViewModel
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
        private string _ptcourseName;

        public string PtcourseName
        {
            get { return _ptcourseName; }
            set { _ptcourseName = value; OnPropertyChanged(); }
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
        
        private string _numberOfSession;



        public string NumberOfSession
        {
            get { return _numberOfSession; }
            set { _numberOfSession = value; OnPropertyChanged(); }
        }

        private GymDbContext Context { get; set; }



        public void AddNewPtcourse()
        {
            _ = AddNewPtcourseAsync();
        }

        public Task AddNewPtcourseAsync()
        {
            return Task.Factory.StartNew(() =>
            {
                using (Context = new GymDbContext())
                {
                    if (PtcourseName != null && Price != null 
                        && Duration != null && NumberOfSession!=null)
                    {
                        Ptcourse ptcourse = new Ptcourse();
                        ptcourse.Name = PtcourseName;
                        ptcourse.Price = Int32.Parse(Price);
                        ptcourse.Duration = Int32.Parse(Duration);
                        ptcourse.NumberOfSession = Int32.Parse(NumberOfSession);
                        ptcourse.Active = true;

                        Context.Add<Ptcourse>(ptcourse);
                        Context.SaveChanges();
                        PtcourseContext.Add(ptcourse);
                    }
                    else
                    {
                        Flag = false;
                    }

                }
            });
        }
        public ObservableCollection<Ptcourse> Sync()
        {
            return PtcourseContext;
        }

        private ObservableCollection<Ptcourse> PtcourseContext;
        public AddPtcourseViewModel(ObservableCollection<Ptcourse> _ptcourseContext)
        {
            PtcourseContext = _ptcourseContext;
        }
    }
}
