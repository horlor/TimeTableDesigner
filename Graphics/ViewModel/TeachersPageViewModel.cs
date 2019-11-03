using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimetableDesigner.Graphics.Commands;
using TimetableDesigner.Graphics.Data;

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
                    selected = value;
                    OnPropertyChanged();
                }
            }
        }

        private bool IsSelectedNotNull(object o)
        {
            return !(selected is null);
        }

        public CommandBase NewTeacherCmd { get; }

        public CommandBase AddSubjectCmd { get; private set; }
        public CommandBase RemoveSubjectCmd { get; private set; }
        public CommandBase RemoveTeacherCmd { get; }
        public CommandBase SaveChangesCmd { get; }
        public CommandBase DropChangesCmd { get; }
    }
}
