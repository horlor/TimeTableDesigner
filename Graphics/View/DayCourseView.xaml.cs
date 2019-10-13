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
    public sealed partial class DayCourseView : UserControl
    {
        public DayCourseViewModel Model { get; set; } = new DayCourseViewModel();

        public static readonly DependencyProperty textblockProperty =
        DependencyProperty.Register("Model", typeof(DayCourseViewModel), typeof(DayCourseView), null);
        public DayCourseView()
        {
            this.InitializeComponent();
            ListPanel.SelectionChanged += (object sender, SelectionChangedEventArgs args) =>
            {
                this.SelectionChanged(this, args);
            };
        }

        public CourseViewModel Selected
        {
            get => ListPanel.SelectedItem as CourseViewModel;

            set => ListPanel.SelectedItem = value;

        }


        public event SelectionChangedEventHandler SelectionChanged;

    }
}
