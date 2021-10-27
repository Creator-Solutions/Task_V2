using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Task_2.Views
{
    /// <summary>
    /// Interaction logic for Navigation.xaml
    /// </summary>
    public partial class Navigation : Page
    {
        public Navigation()
        {
           
            InitializeComponent();
            frmView.Source = new Uri("../Contents/Dashboard.xaml", UriKind.Relative);
        }



        private void tbDashboard_MouseEnter(object sender, MouseEventArgs e)
        {
            tbDashboard.TextDecorations = TextDecorations.Underline;
        }

        private void tbDashboard_MouseLeave(object sender, MouseEventArgs e)
        {
            tbDashboard.TextDecorations = null;
        }

        private void tbViewModules_MouseEnter(object sender, MouseEventArgs e)
        {
            tbViewModules.TextDecorations = TextDecorations.Underline;
        }

        private void tbViewModules_MouseLeave(object sender, MouseEventArgs e)
        {
            tbViewModules.TextDecorations = null;
        }

        private void tbAddModule_MouseEnter(object sender, MouseEventArgs e)
        {
            tbAddModule.TextDecorations = TextDecorations.Underline;
        }

        private void tbAddModule_MouseLeave(object sender, MouseEventArgs e)
        {
            tbAddModule.TextDecorations = null;
        }

        private void tbDashboard_MouseDown(object sender, MouseButtonEventArgs e)
        {
           frmView.Source = new Uri("../Contents/Dashboard.xaml", UriKind.Relative);
        }

        private void tbAddModule_MouseDown(object sender, MouseButtonEventArgs e)
        {
           frmView.Source = new Uri("../Contents/AddModule.xaml", UriKind.Relative);
        }

        private void tbViewModules_MouseDown(object sender, MouseButtonEventArgs e)
        {
            frmView.Source = new Uri("../Contents/ViewModules.xaml", UriKind.Relative);
        }
    }
}
