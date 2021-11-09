using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Task_2.Scripts.Controllers;
using Task_2.Scripts.External;

namespace Task_2.Views
{
    /// <summary>
    /// Interaction logic for Register.xaml
    /// </summary>
    public partial class Register : Page
    {

        //Class Objects
        Student student = new Student();
        StudentController studentController = new StudentController();

        public Register()
        {
            InitializeComponent();
        }


        /// <summary>
        /// Add TextDecorations to the TextBlocks    
        /// " => " Signifies shorthand method for readability
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TbRegister_MouseLeave(object sender, MouseEventArgs e) => TbRegister.TextDecorations = null;

        private void TbLogin_MouseEnter(object sender, MouseEventArgs e) => TbLogin.TextDecorations = TextDecorations.Underline;

        private void TbLogin_MouseLeave(object sender, MouseEventArgs e) => TbLogin.TextDecorations = null;


        private void TbRegister_MouseEnter(object sender, MouseEventArgs e) => TbRegister.TextDecorations = TextDecorations.Underline;

        /// <summary>
        /// TextBlock Mouse Down events => "Click Event Logic"
        /// Register User On Application
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void TbRegister_MouseDown(object sender, MouseButtonEventArgs e)
        {
            student.Student_FullName = txtFullName.Text;
            student.Student_Number = txtStudentNumber.Text;
            student.Student_Email = txtStudentEmail.Text;
            student.Password = txtStudentPassword.Password;
            student.Student_Confirm_Password = txtConfirmPassword.Password;

            student.Valid_Credentials = studentController.Valid_Credentials
            (
                student.Student_FullName,
                student.Student_Number,
                student.Student_Email,
                student.Password,
                student.Student_Confirm_Password
            );

            //wait To Check if user already exists
            student.Student_Exists = await studentController.User_Exists(student.Student_Number, student.Student_Email);

            //Display error if exists
            if (student.Student_Exists)
            {
                _ = MessageBox.Show("A student with these credentials already exists", "An Error Has Occurred", MessageBoxButton.OKCancel, MessageBoxImage.Error);
            } else
            {
                //Wait for register task to complete
                student.Registration_Successful = await studentController.Create_Student(student.Student_FullName, student.Student_Number, student.Student_Email, student.Password);

                //If Registration successful Change Navigation
                if (student.Registration_Successful)
                {
                    Application.Current.Properties["Student_Number"] = student.Student_Number;
                    Application.Current.Properties["Student_Email"] = student.Student_Email;
                    foreach (Window window in Application.Current.Windows)
                    {
                        if(window.GetType() == typeof(MainWindow))
                        {
                            (window as MainWindow).frmView.Source = new Uri("Views/Navigation.xaml", UriKind.Relative);
                        }
                    }
                }
            }

        }


        /// <summary>
        /// TextBlock Mouse Down events => "Click Event Logic"
        /// Change To Login Page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TbLogin_MouseDown(object sender, MouseButtonEventArgs e)
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
