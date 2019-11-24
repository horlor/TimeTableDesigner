using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimetableDesigner.Graphics.Commands;
using TimetableDesigner.Model;
using Windows.UI.Xaml.Controls;

namespace TimetableDesigner.Graphics.ViewModel
{
    public class SubjectViewModel : ViewModelBase
    {
        public SubjectViewModel(Subject subject)
        {
            Model = subject;
            name = subject.Name;
            SaveChangesCmd = new CommandBase((o) => { Save(); }, (o) => IsChanged());
            DropChangesCmd = new CommandBase((o) => { Drop(); }, (o) => IsChanged());

        }

        public Subject Model { get; }

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

        public void Drop()
        {
            name = Model.Name;
            OnPropertyChanged(String.Empty);
        }

        public CommandBase SaveChangesCmd { get; }
        public CommandBase DropChangesCmd { get; }

        public bool IsChanged()
        {
            return name != Model.Name;
        }
    }
}
