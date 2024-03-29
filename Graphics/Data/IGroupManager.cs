﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimetableDesigner.Graphics.ViewModel;
using System.Collections.ObjectModel;
using TimetableDesigner.Model;

namespace TimetableDesigner.Graphics.Data
{
    public interface IGroupManager
    {
        GroupViewModel CreateGroup();
        void RemoveGroup(GroupViewModel model);
        ObservableCollection<GroupViewModel> Groups { get; }
        GroupViewModel FindGroupByModel(Group group);
    }
}
