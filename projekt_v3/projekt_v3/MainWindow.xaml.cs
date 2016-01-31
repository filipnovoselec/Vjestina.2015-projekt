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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.MainFrame.Navigate(new HomePage());
        }

        private void HomeMenuButton_Click(object sender, RoutedEventArgs e)
        {
            this.MainFrame.Navigate(new HomePage());
        }

        private void GradeMenuButton_Click(object sender, RoutedEventArgs e)
        {
            this.MainFrame.Navigate(new GradePage());
        }

        private void PlanMenuButton_Click(object sender, RoutedEventArgs e)
        {
            this.MainFrame.Navigate(new PlanPage());
        }

        private void CalendarMenuButton_Click(object sender, RoutedEventArgs e)
        {
            this.MainFrame.Navigate(new CalendarPage());
        }
    }
}
