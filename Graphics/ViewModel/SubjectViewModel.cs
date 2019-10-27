using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimetableDesigner.Model;

namespace TimetableDesigner.Graphics.ViewModel
{
    public class SubjectViewModel : ViewModelBase
    {
        private Subject model;
        public Subject Model
        {
            get
            {
                return model;
            }
            set
            {
                model = value;
                _name = model.Name;
            }
        }
        private string _name;
        public string Name { 
            get
            {
                return _name;
            }
            set
            {
                Model.Name = value;
                OnPropertyChanged();
            }
        }
    }
}
