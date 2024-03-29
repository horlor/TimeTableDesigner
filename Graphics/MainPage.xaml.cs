﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using TimetableDesigner.Graphics.View;
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
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Graphics
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {

        public MainPage()
        {
            InitializeComponent();
        }
        public string Title
        {
            get
            {
                return GetValue(TitleProperty) as string;
            }
            set
            {
                SetValue(TitleProperty, value);
            }
        }
        public static readonly DependencyProperty TitleProperty =
        DependencyProperty.Register("Title", typeof(string), typeof(MainPage), null);

        // List of ValueTuple holding the Navigation Tag and the relative Navigation Page
        private readonly List<(string Tag, Type Page, string Title)> pages = new List<(string Tag, Type Page, string Title)>
        {
            ("course", typeof(CoursesPage), "Courses"),
            ("teacher",typeof(TeachersPage), "Teachers"),
            ("group",typeof(GroupsPage), "Groups"),
            ("subject",typeof(SubjectsPage),"Subjects")
        };


        private void NavigationMenu_SelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
        {
            if(args.SelectedItemContainer != null)
            {
                string tag = args.SelectedItemContainer.Tag.ToString();
                Navigate(tag, args.RecommendedNavigationTransitionInfo);
            }
        }

        private void Navigate(string tag, NavigationTransitionInfo transitionInfo)
        {
            Type page = null;
            if(tag == "settings")
            {

            }
            else
            {
                var item = pages.FirstOrDefault(p => p.Tag.Equals(tag));
                page = item.Page;
                Title = item.Title;
            }

            var preNavPageType = ContentFrame.CurrentSourcePageType;

            if (!(page is null) && !Type.Equals(preNavPageType, page))
            {
                ContentFrame.Navigate(page, null, transitionInfo);
            }
        }
    }
}
