using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimetableDesigner.Model;

namespace TimetableDesigner.Graphics.ViewModel
{
    public class TeacherViewModel : ViewModelBase
    {
        public Teacher Model { get; set; }

        public TeacherViewModel(Teacher teacher)
        {
            Model = teacher;
            settedname = Model.Name;
        }

        private string settedname;
        public String Name
        {
            get
            {
                return settedname;
            }
            set
            {
                settedname = value;
            }
        }

        public void Save()
        {
            Model.Name = settedname;
        }

        public void Drop()
        {
            settedname = Model.Name;
            //Notify the change on all property
            OnPropertyChanged(String.Empty);
        }

        public IReadOnlyList<Subject> Subjects
        {
            get
            {
                return Model.TeachedSubjects;
            }
        }

        public void AddNewSubject()
        {

        }


    }
}
