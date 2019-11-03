using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimetableDesigner.Model;

namespace TimetableDesigner.Graphics.ViewModel
{
    public class GroupViewModel : ViewModelBase
    {
        public Group Model { get; }

        public GroupViewModel(Group g)
        {
            Model = g;
        }
        private string name;
        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
                OnPropertyChanged();
            }
        }

        public void Save()
        {
            Model.Name = Name;
        }

        public void Drop()
        {
            Name = Model.Name;
        }
    }
}
