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
    /// Interaction logic for PlanPage.xaml
    /// </summary>
    public partial class PlanPage : UserControl
    {
        public PlanPage()
        {
            InitializeComponent();
            LoadItemTemplate();
        }

        private void LoadItemTemplate()
        {
            using (var db = new GradedbEntities1())
            {
                PlanList.ItemsSource = db.Plans.ToList();
            }
        }

        private void AddPlan_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GetNavigationService(this).Navigate(new ViewPages.AddPlanView());
        }

        private void PlanList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            PlanName.Content = ((Plan)PlanList.SelectedItem).Subject;
            WantedAvg.Content = ((Plan)PlanList.SelectedItem).WantedAvg.ToString();
            
            using (var db = new GradedbEntities1())
            {
                CurrentAvg.Content = db.Subjects.FirstOrDefault(p => p.Id == ((Plan)PlanList.SelectedItem).SubjectId).Avg.ToString();
            }
        }

        private void DelPlan_Click(object sender, RoutedEventArgs e)
        {
            if (PlanList.SelectedItem != null)
            {
                using (var db = new GradedbEntities1())
                {
                    db.Plans.Remove(db.Plans.FirstOrDefault(p => p.Id == ((Plan)PlanList.SelectedItem).Id));
                    db.SaveChanges();
                }
                NavigationService.GetNavigationService(this).Navigate(new PlanPage());
            }
        }
    }
}
