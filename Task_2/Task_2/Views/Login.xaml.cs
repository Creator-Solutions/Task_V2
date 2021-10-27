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
using Task_2.Scripts.Controllers;
using Task_2.Scripts.External;

namespace Task_2.Views
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Page
    {
        private LoginInfo login_info = new LoginInfo();
        private LoginController login_ctrl = new LoginController();

        public Login()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Main Login Logic
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void tbLogin_MouseDown(object sender, MouseButtonEventArgs e)
        {                      
            //Assign Field Values To Property
            login_info.Student_Email = txtstd_Email.Text;
            login_info.Student_Password = txtstd_Password.Password;

            if (login_ctrl.Valid_Email(login_info.Student_Email, login_info.Student_Password))
            {
               login_info.Student_Exists = await login_ctrl.Login(login_info.Student_Email, login_info.Student_Password);
                
            } else
            {
                _ = MessageBox.Show("Please fill in all fields", "Failed To Login", MessageBoxButton.OKCancel, MessageBoxImage.Error);
                _ = txtstd_Email.Focus();
            }
          


            //Wait For Task To Complete

            if (login_info.Student_Exists)
            {
                //For loop to access MainWindow Control
                foreach (Window window in Application.Current.Windows)
                {
                    if (window.GetType() == typeof(MainWindow))
                    {
                        //Change Frame Source
                        (window as MainWindow).frmView.Source = new Uri("Views/Navigation.xaml", UriKind.Relative);
                    }
                }
            }
        }


        /// <summary>
        /// Change Frame Contents To Register Page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tbLink_MouseDown(object sender, MouseButtonEventArgs e)
        {
            //For loop to access MainWindow Control
            foreach (Window window in Application.Current.Windows)
            {
                if (window.GetType() == typeof(MainWindow))
                {
                    
                    (window as MainWindow).frmView.Source = new Uri("Views/Register.xaml", UriKind.Relative);
                }
            }
        }    
    }
}
