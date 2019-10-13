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
        public TimetableView timetable { set; get; }
        public CourseViewModel ViewModel { get; set; }

        public static readonly DependencyProperty viewModelProperty =
       DependencyProperty.Register("ViewModel", typeof(CourseViewModel), typeof(CourseEditView), null);

        public static readonly DependencyProperty textblockProperty =
       DependencyProperty.Register("timetable", typeof(TimetableView), typeof(CourseEditView), null);
    }
}
