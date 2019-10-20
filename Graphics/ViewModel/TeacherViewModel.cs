using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimetableDesigner.Model;

namespace TimetableDesigner.Graphics.ViewModel
{
    public class TeacherViewModel
    {
        public Teacher Model { get; set; }

        public String Name
        {
            get
            {
                return Model.Name;
            }
        }

    }
}
