using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using TimetableDesigner.Graphics.Data;
using TimetableDesigner.Graphics.ViewModel;
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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace TimetableDesigner.Graphics.View
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class CoursesPage : Page
    {
        public CourseViewModel Course = new CourseViewModel();

        public TimetableViewModel ViewModel = new TimetableViewModel();

        public CoursesPage()
        {
            this.InitializeComponent();
            //IDataController controller = new MockController();
            /*JsonController controller = new JsonController
            {
                Path = "data.json"
            };
            controller.Load();*/
            ViewModel.Courses = DataManager.Instance.dataController.CourseRepo.GetList();
            Timetable.SelectionChanged += (sender, e) =>
            {

                text.Text = Timetable.Selected.ToString();
                edit.ViewModel = Timetable.Selected;
            };
        }

    }
}
