using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimetableDesigner.Graphics.ViewModel;
using System.Collections.ObjectModel;

namespace TimetableDesigner.Graphics.Data
{
    interface IGroupManager
    {
        GroupViewModel CreateGroup();
        void RemoveGroup(GroupViewModel model);
        ObservableCollection<GroupViewModel> Groups { get; }
    }
}
