using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace TimetableDesigner.Graphics.ViewModel
{
    public abstract class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
                System.Diagnostics.Debug.WriteLine(propertyName);
                this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            
        }
    }
}
