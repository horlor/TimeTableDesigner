using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.WindowsRuntime;
using TimetableDesigner.Graphics.ViewModel;
using TimetableDesigner.Model;
using TimetableDesigner.Persistence;
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
    public sealed partial class TimetableView : UserControl, INotifyPropertyChanged
    {
        public TimetableViewModel ViewModel
        {
            get
            {
                return GetValue(ViewModelProperty) as TimetableViewModel;
            }
            set
            {
                SetValue(ViewModelProperty, value);
            }
        }
        public static readonly DependencyProperty ViewModelProperty =
        DependencyProperty.Register("ViewModel", typeof(TimetableViewModel), typeof(TimetableView), null);
        public TimetableView()
        {
            this.InitializeComponent();
            MondayView.SelectionChanged += SelectionHandler;
            TuesdayView.SelectionChanged += SelectionHandler;
            WednesdayView.SelectionChanged += SelectionHandler;
            ThursdayView.SelectionChanged += SelectionHandler;
            FridayView.SelectionChanged += SelectionHandler;
        }

        private void SelectionHandler(object sender, SelectionChangedEventArgs e)
        {
            if (sender != MondayView) MondayView.Selected = null;
            if (sender != TuesdayView) TuesdayView.Selected = null;
            if (sender != WednesdayView) WednesdayView.Selected = null;
            if (sender != ThursdayView) ThursdayView.Selected = null;
            if (sender != FridayView) FridayView.Selected = null;
            if (e.AddedItems.Count != 0)
            {
                Selected = e.AddedItems[0] as CourseViewModel;
                SelectionChanged?.Invoke(this, e);
            }
            
        }

        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        public event SelectionChangedEventHandler SelectionChanged;

        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            System.Diagnostics.Debug.WriteLine("Refreshed_" + propertyName);
        }

        public static readonly DependencyProperty SelectedProperty =
        DependencyProperty.Register("Selected", typeof(CourseViewModel), typeof(TimetableView), null);

        public CourseViewModel Selected
        {
            get
            {
                return GetValue(SelectedProperty) as CourseViewModel;
            }
            set
            {
                SetValue(SelectedProperty, value);
                //OnPropertyChanged();
            }
        }
        public CourseViewModel GetSelected()
        {
            return Selected;
        }
    }
}
