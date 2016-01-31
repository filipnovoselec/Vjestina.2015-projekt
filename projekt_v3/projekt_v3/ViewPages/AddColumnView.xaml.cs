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
    /// Interaction logic for AddColumnView.xaml
    /// </summary>
    public partial class AddColumnView : Page
    {
        private bool _ready = true;
        public Subject Sub;
        public AddColumnView(Subject Sub)
        {
            this.Sub = Sub;
            InitializeComponent();
        }

        private void Mode_TextChanged(object sender, TextChangedEventArgs e)
        {
            string c = Mode.Text;
            int n;
            if(!int.TryParse(c, out n) && Mode.Text.Length !=0)
            {
                Mode.Background = new SolidColorBrush( Colors.Red);
                _ready = false;
            }
            else
            {
                Mode.Background = new SolidColorBrush(Colors.Transparent);
                _ready = true;
            }
           
        }

        private void SubMode_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(((ComboBoxItem)ColMode.SelectedItem).Tag.ToString() == "coef")
            {
                Mode.IsEnabled = true;
                ModeDesc.Content = "Koeficijent rubrike:";
                _ready = false;
            }
            else if (((ComboBoxItem)ColMode.SelectedItem).Tag.ToString() == "grup")
            {
                Mode.IsEnabled = true;
                ModeDesc.Content = "Broj članova grupe:";
                _ready = false;
            }
            else
            {
                if (Mode != null)
                {
                    Mode.IsEnabled = false;
                    ModeDesc.Content = null;
                    _ready = true;
                }
            }

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (_ready && ColName.Text.Length !=0)
            {
                using (var db = new GradedbEntities1())
                {
                    Column Col = new Column();
                    Col.SubjectId = Sub.Id;
                    Col.ColumnName = ColName.Text;
                    Col.Rule = ColMode.SelectedIndex + 1;

                    if (Mode.Text.Length == 0)
                    {
                        Col.Coeficient = null;
                    }
                    else
                    {
                        Col.Coeficient = Convert.ToInt32(Mode.Text);
                    }

                    db.Columns.Add(Col);

                    db.SaveChanges();
                }

                NavigationService.GetNavigationService(this).Navigate(new ViewPages.GradeView(Sub));
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            NavigationService.GetNavigationService(this).GoBack();
        }
    }
}
