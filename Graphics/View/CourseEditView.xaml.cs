using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using TimetableDesigner.Graphics.ViewModel;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace TimetableDesigner.Graphics.View
{
    public sealed partial class CourseEditView : UserControl
    {
        public CourseEditView()
        {
            this.InitializeComponent();
        }

        public static readonly DependencyProperty TimetableProperty =
       DependencyProperty.Register("Timetable", typeof(TimetableView), typeof(CourseEditView), null);
        public TimetableView Timetable {
            set
            {
                SetValue(TimetableProperty, value);
            }
            get 
            {
                return GetValue(TimetableProperty) as TimetableView;
            } 
        }

        public static readonly DependencyProperty viewModelProperty =
       DependencyProperty.Register("ViewModel", typeof(CourseViewModel), typeof(CourseEditView), null);
        public CourseViewModel ViewModel {
            get {
                return (CourseViewModel)GetValue(viewModelProperty);
            }
            set
            {
                SetValue(viewModelProperty, value);
            } 
        
        }

        

        

    }
}
