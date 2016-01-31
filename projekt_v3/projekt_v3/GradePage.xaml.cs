using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace projekt_v3
{
    /// <summary>
    /// Interaction logic for GradePage.xaml
    /// </summary>
    public partial class GradePage : UserControl
    {
        public GradePage()
        {
            InitializeComponent();
            LoadItemTemplate();
        }

        private void LoadItemTemplate()
        {
            using (var subjects = new GradedbEntities1())
            {
                SubjectList.ItemsSource = subjects.Subjects.ToList();
            }
        }

        private void SubjectList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            using (var subjects = new GradedbEntities1())
            {
                SubjectName.Content = subjects.Subjects.FirstOrDefault(p => p.Id == ((Subject)SubjectList.SelectedItem).Id).SubjectName;
                //subjects.Subjects.FirstOrDefault(p => p.Id == ((Subject)SubjectList.SelectedItem).Id).UpdateAverage();
                SubjAvg.Content = subjects.Subjects.FirstOrDefault(p => p.Id == ((Subject)SubjectList.SelectedItem).Id).Avg;

            }


        }

        private void OpenSubject_Click(object sender, RoutedEventArgs e)
        {
            using (var subjects = new GradedbEntities1())
            {
                if (SubjectList.SelectedItem != null)
                {
                    NavigationService.GetNavigationService(this).Navigate(new ViewPages.GradeView(subjects.Subjects.FirstOrDefault(p => p.Id == ((Subject)SubjectList.SelectedItem).Id)));
                }
            }
        }

        private void AddSubject_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GetNavigationService(this).Navigate(new ViewPages.AddSubjectView());
        }

        private void DelSubject_Click(object sender, RoutedEventArgs e)
        {
            if (SubjectList.SelectedItem != null)
            {
                using(var db = new GradedbEntities1())
                {
                    var sub = (Subject)SubjectList.SelectedItem;
                    foreach(var col in db.Columns.Where(p=>p.SubjectId==sub.Id))
                    {
                            db.Grades.RemoveRange(db.Grades.Where(p => p.ColumnId == col.Id));
                    }
                    db.Columns.RemoveRange(db.Columns.Where(p => p.SubjectId == sub.Id));
                    
                    if (db.Plans.FirstOrDefault(p => p.SubjectId == sub.Id) != null)
                    {
                        db.Plans.RemoveRange(db.Plans.Where(p => p.SubjectId == sub.Id));
                    }
                    db.Subjects.Remove(db.Subjects.FirstOrDefault(p => p.Id == sub.Id));
                    db.SaveChanges();
                }
                NavigationService.GetNavigationService(this).Navigate(new GradePage());
            }
        }
    }
}
