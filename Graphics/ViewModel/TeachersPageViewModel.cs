using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimetableDesigner.Graphics.Commands;
using TimetableDesigner.Graphics.Data;
using Windows.UI.Xaml.Controls;

namespace TimetableDesigner.Graphics.ViewModel
{
    public class TeachersPageViewModel : ViewModelBase
    {
        public ITeacherManager TeacherManager { get; set; } = DataManager.Instance;

        public ObservableCollection<TeacherViewModel> Teachers
        {
            get
            {
                return TeacherManager.Teachers;
            }
        }

        public TeachersPageViewModel()
        {
            NewTeacherCmd = new CommandBase((o) => { var item = TeacherManager.CreateTeacher(); Selected = item; }, (o) => true);
            RemoveTeacherCmd = new CommandBase((o) => { TeacherManager.RemoveTeacher(selected); }, IsSelectedNotNull);
            SaveChangesCmd = new CommandBase((o) => Selected.Save(), (o) => IsSelectedNotNull(o) && Selected.IsChanged());
            DropChangesCmd = new CommandBase((o) => Selected.Drop(), (o) => IsSelectedNotNull(o) && !Selected.IsChanged());

        }

        private TeacherViewModel selected;
        private TeacherViewModel previous =null;
        public TeacherViewModel Selected
        {
            get
            {
                return selected;
            }
            set
            {
                if (selected != value)
                {
                    previous = selected;
                    selected = value;
                    OnPropertyChanged();
                }
            }
        }

        private bool IsSelectedNotNull(object o)
        {
            return !(selected is null);
        }

        public async void SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("Mi a rosebb "+previous);
            if (previous != null && previous.IsChanged())
            {
                bool res = await SaveOrDropDialog();
                if (res)
                    previous.Save();
                else
                    previous.Drop();
            }
                
        }

        public async Task<bool> SaveOrDropDialog()
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
                return true;
            return false;    
        }

        public CommandBase NewTeacherCmd { get; }

        public CommandBase AddSubjectCmd { get; private set; }
        public CommandBase RemoveSubjectCmd { get; private set; }
        public CommandBase RemoveTeacherCmd { get; }
        public CommandBase SaveChangesCmd { get; }
        public CommandBase DropChangesCmd { get; }
    }
}
