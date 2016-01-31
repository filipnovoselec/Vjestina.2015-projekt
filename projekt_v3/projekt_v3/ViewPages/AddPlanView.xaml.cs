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

namespace projekt_v3.ViewPages
{
    /// <summary>
    /// Interaction logic for AddPlanView.xaml
    /// </summary>
    public partial class AddPlanView : Page
    {
        private bool _ready1 = false;
        private bool _ready2 = false;
        public float WAvg;
        public int n;
        public AddPlanView()
        {
            InitializeComponent();
            LoadItemTemplate();
        }

        public void LoadItemTemplate()
        {
            using (var db = new GradedbEntities1())
            {
                PlanSub.ItemsSource = db.Subjects.ToList();
            }
        }

        private void avg_TextChanged(object sender, TextChangedEventArgs e)
        {
            string c = avg.Text;
            if (!float.TryParse(c, out WAvg) && avg.Text.Length != 0)
            {
                avg.Background = new SolidColorBrush(Colors.Red);
                _ready1 = false;
            }
            else
            {
                if (WAvg > 5)
                {
                    avg.Background = new SolidColorBrush(Colors.Red);
                    _ready1 = false;
                }
                else {
                    avg.Background = new SolidColorBrush(Colors.Transparent);
                    _ready1 = true;
                }
            }
        }

        private void Num_TextChanged(object sender, TextChangedEventArgs e)
        {
            string c = Num.Text;
            if (!int.TryParse(c, out n) && Num.Text.Length != 0)
            {
                Num.Background = new SolidColorBrush(Colors.Red);
                _ready2 = false;
            }
            else
            {
                Num.Background = new SolidColorBrush(Colors.Transparent);
                _ready2 = true;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GetNavigationService(this).GoBack();
        }

        private void AddPlan_Click(object sender, RoutedEventArgs e)
        {
            if (_ready1 && _ready2 && (Subject)PlanSub.SelectedItem != null)
            {
                using (var db = new GradedbEntities1())
                {
                    var plan = new Plan();
                    plan.Subject = ((Subject)PlanSub.SelectedItem).SubjectName;
                    plan.SubjectId = ((Subject)PlanSub.SelectedItem).Id;
                    plan.CurrentAvg = (float)((Subject)PlanSub.SelectedItem).Avg;
                    plan.WantedAvg = Convert.ToDouble(avg.Text);
                    plan.Mode = PlanMode.SelectedIndex + 1;
                    plan.Pcolumn = ((Column)pColumn.SelectedItem).Id;
                    plan.nGrades = Convert.ToInt32(Num.Text);
                    plan.ColName = ((Column)pColumn.SelectedItem).ColumnName;
                    plan.CreatePlan();
                    //plan.NeededGrades = "nem";

                    db.Plans.Add(plan);
                    db.SaveChanges();
                }
                NavigationService.GetNavigationService(this).Navigate(new PlanPage());
            }
        }

        private string CreatePlan(int nGrades, int subjectId, int pcolumn, double wantedAvg)
        {
            throw new NotImplementedException();
        }

        private void PlanSub_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            using (var db = new GradedbEntities1())
            {

                pColumn.ItemsSource = db.Columns.Where(p => p.SubjectId == ((Subject)PlanSub.SelectedItem).Id).ToList();
            }
        }
    }
}
