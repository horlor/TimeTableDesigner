using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimetableDesigner.Graphics.ViewModel;

namespace TimetableDesigner.Graphics.Data
{
    public interface ISubjectManager
    {
        SubjectViewModel CreateSubject();
        void RemoveSubject(SubjectViewModel model);
        ObservableCollection<SubjectViewModel> Subjects { get; }
    }
}
