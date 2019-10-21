﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimetableDesigner.Model;

namespace TimetableDesigner.Graphics.ViewModel
{
    public class TeacherViewModel : ViewModelBase
    {
        public Teacher Model { get; }

        public TeacherViewModel(Teacher teacher)
        {
            Model = teacher;
        }

        public String Name
        {
            get
            {
                return Model.Name;
            }
        }


    }
}
