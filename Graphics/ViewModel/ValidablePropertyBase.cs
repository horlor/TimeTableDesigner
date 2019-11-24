using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimetableDesigner.Graphics.ViewModel
{
    public class ValidablePropertyBase<T> : ViewModelBase
    {
        private T value;
        public T Value
        {
            get { return value; }
            set
            {
                if(ReferenceEquals(this.value,null) || !this.value.Equals(value))
                {
                    this.value = value;
                    OnPropertyChanged();
                }
            }
        }

        private bool valid=true;
        public bool Validity
        {
            get
            {
                return valid;
            }
            set
            {
                if(valid != value)
                {
                    valid = value;
                    OnPropertyChanged();
                }
            }
        }
    }
}
