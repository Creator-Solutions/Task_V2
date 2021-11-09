using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Task_2.Views
{
    /// <summary>
    /// Interaction logic for Navigation.xaml
    /// </summary>
    public partial class Navigation : Page
    {

        public string Student_Email { get; set; }

        public Navigation()
        {

            InitializeComponent();
            frmView.Source = new Uri("../Contents/Dashboard.xaml", UriKind.Relative);
        }

        /// <summary>
        /// Add Styling For TextBlocks
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tbDashboard_MouseEnter(object sender, MouseEventArgs e) => tbDashboard.TextDecorations = TextDecorations.Underline;

        private void tbDashboard_MouseLeave(object sender, MouseEventArgs e) => tbDashboard.TextDecorations = null;

        private void tbViewModules_MouseEnter(object sender, MouseEventArgs e) => tbViewModules.TextDecorations = TextDecorations.Underline;

        private void tbViewModules_MouseLeave(object sender, MouseEventArgs e) => tbViewModules.TextDecorations = null;

        private void tbAddModule_MouseEnter(object sender, MouseEventArgs e) => tbAddModule.TextDecorations = TextDecorations.Underline;

        private void tbAddModule_MouseLeave(object sender, MouseEventArgs e) => tbAddModule.TextDecorations = null;

        /// <summary>
        /// Navigation For TextBlocks
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void tbDashboard_MouseDown(object sender, MouseButtonEventArgs e) => frmView.Source = new Uri("../Contents/Dashboard.xaml", UriKind.Relative);

        private void tbAddModule_MouseDown(object sender, MouseButtonEventArgs e) => frmView.Source = new Uri("../Contents/AddModule.xaml", UriKind.Relative);

        private void tbViewModules_MouseDown(object sender, MouseButtonEventArgs e) => frmView.Source = new Uri("../Contents/ViewModules.xaml", UriKind.Relative);


        /// <summary>
        ///  Assign Student Email From Global Property
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Border_Loaded(object sender, RoutedEventArgs e)
        {
            Student_Email = Application.Current.Properties["Student_Email"].ToString();
            DataContext = this;
        }

        /// <summary>
        /// Log out of Account
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lblUser_MouseDown(object sender, MouseButtonEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Are you sure you wish to log out of your account?", "Log Out", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                foreach (Window window in Application.Current.Windows)
                {
                    if (window.GetType() == typeof(MainWindow))
                    {
                        (window as MainWindow).frmView.Source = new Uri("Views/Login.xaml", UriKind.Relative);
                    }
                }
            }
        }
    }
}
