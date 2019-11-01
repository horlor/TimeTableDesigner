using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimetableDesigner.Graphics.ViewModel;
using TimetableDesigner.Model;
using TimetableDesigner.Persistence;

namespace TimetableDesigner.Graphics.Data
{
    public class DataManager
    {
        public JsonController dataController;

        public ObservableCollection<TeacherViewModel> Teachers { get; private set; }
            = new ObservableCollection<TeacherViewModel>();

        private DataManager()
        {
            dataController = new JsonController
            {
                Path = "data.json"
            };
            dataController.Load();

            foreach (Teacher item in dataController.TeacherRepo.GetList())
            {
                Teachers.Add(new TeacherViewModel(item));
            }
        }

        private void LoadTeacherViewModels()
        {

        }

        private static DataManager instance;
        public static DataManager Instance
        {
            get
            {
                if (instance == null)
                    instance = new DataManager();
                return instance;
            }
        }
    }
}
