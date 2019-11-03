using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimetableDesigner.Graphics.Commands;
using TimetableDesigner.Model;
using Windows.UI.Xaml;

namespace TimetableDesigner.Graphics.ViewModel
{
    public class TeacherViewModel : ViewModelBase
    {
        public Teacher Model { get; set; }

        public TeacherViewModel(Teacher teacher)
        {
            Model = teacher;
            name = Model.Name;
            AddSubjectCmd = new CommandBase(AddNewSubject, (o) => true);
            RemoveSubjectCmd = new CommandBase(RemoveSubject, (o) => true);
            SaveChangesCmd = new CommandBase((o) => Save(), (o) => IsChanged());
        }

        private string name;
        public String Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
                SaveChangesCmd.ExecutionChanged();
                OnPropertyChanged();
            }
        }

        public void Save()
        {
            Model.Name = name;
        }



        public CommandBase AddSubjectCmd { get; }
        public CommandBase RemoveSubjectCmd { get; }
        public CommandBase AddNewTeacherCmd { get; }     
        public CommandBase RemoveTeacherCmd { get; }
        public CommandBase SaveChangesCmd { get; }
        public CommandBase DropChangesCmd { get; }
        public void Drop()
        {
            name = Model.Name;
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

        public void AddNewSubject(object o)
        {
            System.Diagnostics.Debug.WriteLine("o: "+o.GetType().FullName+"\n");
            if (o is SubjectViewModel svm)
            {
                Model.AddSubject(svm.Model);
                OnPropertyChanged("Subjects");
            }

        }

        public void RemoveSubject(object o)
        {
            if (o is Subject s)
            {
                Model.RemoveSubject(s);
                OnPropertyChanged("Subjects");
            }
        }

        public override string ToString()
        {
            return this.Name;
        }

        private bool IsChanged()
        {
            return name == Model.Name;
        }

    }
}
