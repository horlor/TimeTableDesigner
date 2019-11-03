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
            NewTeacherCmd = new CommandBase((o) => TeacherManager.CreateTeacher(), (o) => true);
            
            
        }

        public CommandBase NewTeacherCmd { get; }

        public CommandBase AddSubjectCmd { get; }
        public CommandBase RemoveSubjectCmd { get; }
        public CommandBase RemoveTeacherCmd { get; }
        public CommandBase SaveChangesCmd { get; }
        public CommandBase DropChangesCmd { get; }
    }
}
