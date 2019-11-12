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
    public class SubjectsPageViewModel : ViewModelBase
    {
        public ISubjectManager SubjectManager { get; set; } = DataManager.Instance;

        public SubjectsPageViewModel()
        {
            NewSubjectCmd = new CommandBase(o => { var subject = SubjectManager.CreateSubject(); Selected = subject; }, o => true);
            RemoveSubjectCmd = new CommandBase(o=>RemoveSubject(), o => selected!=null);
        }

        public ObservableCollection<SubjectViewModel> Subjects
        {
            get
            {
                return SubjectManager.Subjects;
            }
        }

        private SubjectViewModel selected, previous = null;
        public SubjectViewModel Selected
        {
            get
            {
                return selected;
            }
            set
            {
                if(value != selected)
                {
                    previous = selected;
                    selected = value;
                    OnPropertyChanged();
                    RemoveSubjectCmd.ExecutionChanged();
                }
            }
        }

        public async void SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if ( previous!= null && previous.IsChanged())
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

        public async void RemoveSubject()
        {
            ContentDialog RemoveDialog = new ContentDialog
            {
                Title = "Reference removal",
                Content = "There can be many references to this Subject in courses, these references will be dropped, and can not be undone. Are you sure to continue?",
                CloseButtonText = "No",
                PrimaryButtonText = "Yes"
            };

            ContentDialogResult result = await RemoveDialog.ShowAsync();
            if (result == ContentDialogResult.Primary)
                SubjectManager.RemoveSubject(selected);

        }



        public CommandBase NewSubjectCmd { get; }
        public CommandBase RemoveSubjectCmd { get; }


    }
}
