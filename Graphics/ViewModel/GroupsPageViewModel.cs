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
    public class GroupsPageViewModel : ViewModelBase
    {
        public IGroupManager GroupManager { get; set; } = DataManager.Instance;

        public GroupsPageViewModel()
        {
            NewGroupCmd = new CommandBase(o => { var group = GroupManager.CreateGroup(); Selected = group; }, o => true);
            RemoveGroupCmd = new CommandBase(o=> RemoveGroup(), o=> selected!=null);
        }

        public ObservableCollection<GroupViewModel> Groups
        {
            get
            {
                return GroupManager.Groups;
            }
        }

        private GroupViewModel selected, previous = null;
        public GroupViewModel Selected
        {
            get
            {
                return selected;
            }
            set
            {
                if (value != selected)
                {
                    previous = selected;
                    selected = value;
                    OnPropertyChanged();
                    RemoveGroupCmd.ExecutionChanged();
                }
            }
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

        private async void RemoveGroup()
        {
            ContentDialog RemoveDialog = new ContentDialog
            {
                Title = "Reference removal",
                Content = String.Format("This group has {0} courses, these courses will be deleted," +
                " and after this command, they can not be accessed. Are you sure to continue?", Selected.Model.Courses.Count),
                CloseButtonText = "No",
                PrimaryButtonText = "Yes"
            };

            ContentDialogResult result = await RemoveDialog.ShowAsync();
            if (result == ContentDialogResult.Primary)
                GroupManager.RemoveGroup(selected);
        }



        public CommandBase NewGroupCmd { get; }
        public CommandBase RemoveGroupCmd { get; }
    }
}
