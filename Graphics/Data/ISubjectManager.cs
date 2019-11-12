using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimetableDesigner.Graphics.ViewModel;
using TimetableDesigner.Model;

namespace TimetableDesigner.Graphics.Data
{
    public interface ISubjectManager
    {
        SubjectViewModel CreateSubject();
        void RemoveSubject(SubjectViewModel model);
        ObservableCollection<SubjectViewModel> Subjects { get; }
        SubjectViewModel FindSubjectByModel(Subject s);
    }
}
