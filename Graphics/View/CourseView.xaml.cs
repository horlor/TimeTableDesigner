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
    public sealed partial class CourseView : UserControl
    {
        public CourseViewModel Course { get; set; }

        public static readonly DependencyProperty CourseProperty =
           DependencyProperty.Register("Course", typeof(CourseViewModel), typeof(CourseView), null);

        public CourseView()
        {
            this.InitializeComponent();
        }
    }
}
