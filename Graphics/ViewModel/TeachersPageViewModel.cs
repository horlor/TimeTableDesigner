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
            RemoveTeacherCmd = new CommandBase((o) => RemoveTeacher(),o => IsSelectedNotNull());
            SaveChangesCmd = new CommandBase((o) => Selected.Save(), (o) => IsSelectedNotNull() && Selected.IsChanged());
            DropChangesCmd = new CommandBase((o) => Selected.Drop(), (o) => IsSelectedNotNull() && !Selected.IsChanged());

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
                    RemoveTeacherCmd.ExecutionChanged();
                }
            }
        }

        private bool IsSelectedNotNull()
        {
            return !(selected is null);
        }

        public async void SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
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

        private async void RemoveTeacher()
        {
            ContentDialog RemoveDialog = new ContentDialog
            {
                Title = "Reference problems",
                Content = String.Format("This teacher teaches in {0} courses, these references will be dropped," +
                " and will not be undoable. Are you sure to continue", Selected.Model.Courses.Count),
                CloseButtonText = "No",
                PrimaryButtonText = "Yes"
            };
            var result = await RemoveDialog.ShowAsync();
            if(result == ContentDialogResult.Primary)
            {
                TeacherManager.RemoveTeacher(Selected);
            }
        }

        public CommandBase NewTeacherCmd { get; }
        public CommandBase RemoveTeacherCmd { get; }
        public CommandBase SaveChangesCmd { get; }
        public CommandBase DropChangesCmd { get; }
    }
}
