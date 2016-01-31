using System;
using System.Collections.Generic;
using System.Data;
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

namespace projekt_v3.ViewPages
{
    /// <summary>
    /// Interaction logic for GradeView.xaml
    /// </summary>
    public partial class GradeView : Page
    {
        public Subject Sub;
        public GradeView(Subject Sub)
        {
            this.Sub = Sub;
            InitializeComponent();
            LoadItemTemplate();
            populateDatagrid();
        }

        private void LoadItemTemplate()
        {
            GradedbEntities1 col = new GradedbEntities1();

        }

        public void populateDatagrid()
        {

            DataTable GradeTable = new DataTable();
            GradeTable.Columns.Add("Rubrika", typeof(string));
            GradeTable.Columns.Add("IX", typeof(string));
            GradeTable.Columns.Add("X", typeof(string));
            GradeTable.Columns.Add("XI", typeof(string));
            GradeTable.Columns.Add("XII", typeof(string));
            GradeTable.Columns.Add("I", typeof(string));
            GradeTable.Columns.Add("II", typeof(string));
            GradeTable.Columns.Add("III", typeof(string));
            GradeTable.Columns.Add("IV", typeof(string));
            GradeTable.Columns.Add("V", typeof(string));
            GradeTable.Columns.Add("VI", typeof(string));

            using (var subject = new GradedbEntities1())
            {
                var columns = subject.Columns.Where(p => p.SubjectId == Sub.Id).ToList();
                foreach (var ele in columns)
                {
                    GradeTable.Rows.Add(ele.ColumnName,
                        string.Join(",", subject.Grades.Where(p => p.ColumnId == ele.Id && p.Date.Month == 9).Select(p => p.GradeValue).ToList()),
                        string.Join(",", subject.Grades.Where(p => p.ColumnId == ele.Id && p.Date.Month == 10).Select(p => p.GradeValue).ToList()),
                        string.Join(",", subject.Grades.Where(p => p.ColumnId == ele.Id && p.Date.Month == 11).Select(p => p.GradeValue).ToList()),
                        string.Join(",", subject.Grades.Where(p => p.ColumnId == ele.Id && p.Date.Month == 12).Select(p => p.GradeValue).ToList()),
                        string.Join(",", subject.Grades.Where(p => p.ColumnId == ele.Id && p.Date.Month == 1).Select(p => p.GradeValue).ToList()),
                        string.Join(",", subject.Grades.Where(p => p.ColumnId == ele.Id && p.Date.Month == 2).Select(p => p.GradeValue).ToList()),
                        string.Join(",", subject.Grades.Where(p => p.ColumnId == ele.Id && p.Date.Month == 3).Select(p => p.GradeValue).ToList()),
                        string.Join(",", subject.Grades.Where(p => p.ColumnId == ele.Id && p.Date.Month == 4).Select(p => p.GradeValue).ToList()),
                        string.Join(",", subject.Grades.Where(p => p.ColumnId == ele.Id && p.Date.Month == 5).Select(p => p.GradeValue).ToList()),
                        string.Join(",", subject.Grades.Where(p => p.ColumnId == ele.Id && p.Date.Month == 6).Select(p => p.GradeValue).ToList()));
                }

                SubjAvg.Content = subject.Subjects.FirstOrDefault(p => p.Id == Sub.Id).Avg.ToString();
            }
            GradeGrid.ItemsSource = GradeTable.DefaultView;

            SubjectName.Content = Sub.SubjectName;

            //SubjAvg.Content = Sub.Avg.ToString();
            
        }

        private void AddColumn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GetNavigationService(this).Navigate(new ViewPages.AddColumnView(this.Sub));
        }

        private void AddGrade_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GetNavigationService(this).Navigate(new ViewPages.AddGradeView(Sub));
        }
    }
}
