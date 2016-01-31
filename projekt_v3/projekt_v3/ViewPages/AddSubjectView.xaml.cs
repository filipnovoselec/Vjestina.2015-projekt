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
    /// Interaction logic for AddSubjectView.xaml
    /// </summary>
    public partial class AddSubjectView : Page
    {
        public AddSubjectView()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (SubName.Text.Length != 0)
            {
                using (var db = new GradedbEntities1())
                {

                    var Sub = new Subject();
                    Sub.SubjectName = SubName.Text;
                    Sub.Rule = SubMode.SelectedIndex+1;

                    db.Subjects.Add(Sub);
                    db.SaveChanges();
                }

                NavigationService.GetNavigationService(this).Navigate(new GradePage());
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            NavigationService.GetNavigationService(this).GoBack();
        }
    }
}
