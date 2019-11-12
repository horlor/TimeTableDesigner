using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimetableDesigner.Graphics.Commands;
using TimetableDesigner.Model;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

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
            SaveChangesCmd = new CommandBase((o) => { Save(); }, (o) => IsChanged());
            DropChangesCmd = new CommandBase((o) => { Drop(); }, (o) => IsChanged());
        }
        public async Task SaveOrDropDialog()
        {
            ContentDialog SaveDialog = new ContentDialog
            {
                Title = "Not saved modifications",
                Content = "There are unsaved modifications on the selected item. Do you want to save them?",
                CloseButtonText = "No",
                PrimaryButtonText = "Yes"
            };

            ContentDialogResult result = await SaveDialog.ShowAsync();
            if (result == ContentDialogResult.Primary)
                this.Save();
            else
                this.Drop();
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
                DropChangesCmd.ExecutionChanged();
                OnPropertyChanged();
            }
        }

        public void Save()
        {
            Model.Name = name;
        }



        public CommandBase AddSubjectCmd { get; }
        public CommandBase RemoveSubjectCmd { get; }
        public CommandBase SaveChangesCmd { get; }
        public CommandBase DropChangesCmd { get; }

        public void Drop()
        {
            Name = Model.Name;
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

        public bool IsChanged()
        {
            return name != Model.Name;
        }

    }
}
