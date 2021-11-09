using LiveCharts;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using Task_2.Scripts.Controllers;
using Task_2.Scripts.External;

namespace Task_2.Views
{
    /// <summary>
    /// Interaction logic for Dashboard.xaml
    /// </summary>
    public partial class Dashboard : Page
    {
        //connection string to database
        private const string connString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Course_Management;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        DashboardController dash_contr = new DashboardController();

        public Dashboard()
        {
            //Set datacontext for data binding
            DataContext = dash_contr;
            InitializeComponent();
        }

        /// <summary>
        /// Scheduler to execute Async Tasks
        /// Every X seconds
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Border_Loaded(object sender, RoutedEventArgs e)
        {
            DispatcherTimer scheduler = new DispatcherTimer();
            scheduler.Tick += new EventHandler(scheduler_Tick);
            scheduler.Interval = new TimeSpan(0, 0, 5);
            scheduler.Start();
        }


        /// <summary>
        /// Call Async Tasks Every 5 Tics
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void scheduler_Tick(object sender, EventArgs e)
        {

            if (pgLoading.Value < pgLoading.Maximum)
            {
                pgLoading.Value += 10;

            }
            else
            {
                pgLoading.Visibility = Visibility.Collapsed;
            }

            await Get_SemesterData();
            await Highest_Time();
            await LowestTime();

        }

        /// <summary>
        /// Async Task To Retrieve
        /// Semester Information From 
        /// the Database
        /// </summary>
        /// <returns></returns>
        private async Task Get_SemesterData()
        {
            string student_number = Properties.Settings.Default.Student_Number;

            DateTime time = DateTime.Now;
            dash_contr.Current_Year = time.Year;

            string sql = "SELECT Semester_Start_Date, Semester_Week_Length FROM Module_Data WHERE Student_Number=@num";

            using (var conn = new SqlConnection(connString))
            using (var cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.Add("@num", SqlDbType.VarChar, 55).Value = student_number;

                await conn.OpenAsync();

                SqlDataReader reader = await cmd.ExecuteReaderAsync();

                if (reader.Read())
                {
                    dash_contr.Semester_Length = int.Parse(reader["Semester_Week_Length"].ToString());
                    dash_contr.Semester_Start_Date = reader["Semester_Start_Date"].ToString();
                }
            }

        }

        /// <summary>
        /// Async Method That returns The Highest
        /// Studied Time
        /// </summary>
        /// <returns>
        /// String: Module Code
        /// int: Time
        /// </returns>
        private async Task Highest_Time()
        {
            string student_number = Properties.Settings.Default.Student_Number;
            string sql = "SELECT TOP(1) MAX(selfStudyHours) as HIGHEST, ModuleCode FROM Module_Data WHERE Student_Number=@num GROUP BY ModuleCode, selfStudyHours ORDER BY selfStudyHours DESC";

            using (var conn = new SqlConnection(connString))
            using (var cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.Add("@num", SqlDbType.VarChar, 55).Value = student_number;

                await conn.OpenAsync();

                SqlDataReader reader = await cmd.ExecuteReaderAsync();

                if (reader.Read())
                {
                    dash_contr.Highest_Module = reader["ModuleCode"].ToString();
                    dash_contr.Highest_Time = int.Parse(reader["Highest"].ToString());
                }
            }
        }


        /// <summary>
        /// Async Method That returns The Lowest
        /// Studied Time
        /// </summary>
        /// <returns>
        /// String: Module Code
        /// int: Time
        /// </returns>
        private async Task LowestTime()
        {
            // "SELECT TOP(1) MIN(selfStudyHours) as LOWEST, ModuleCode FROM Module_Data WHERE Student_Number=@num GROUP BY ModuleCode, selfStudyHours ORDER BY selfStudyHours ASC";
            string student_number = Properties.Settings.Default.Student_Number;
            string sql = "SELECT TOP(1) MIN(selfStudyHours) as LOWEST, ModuleCode FROM Module_Data WHERE Student_Number=@num GROUP BY ModuleCode, selfStudyHours ORDER BY selfStudyHours ASC";

            using (var conn = new SqlConnection(connString))
            using (var cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.Add("@num", SqlDbType.VarChar, 55).Value = student_number;

                await conn.OpenAsync();

                SqlDataReader reader = await cmd.ExecuteReaderAsync();

                if (reader.Read())
                {
                    dash_contr.Lowest_Module = reader["ModuleCode"].ToString();
                    dash_contr.Lowest_Time = int.Parse(reader["LOWEST"].ToString());
                }
            }
        }


       /// <summary>
       /// Change Navigation To AddModule Page
       /// </summary>
       /// <param name="sender"></param>
       /// <param name="e"></param>
        private void btnModules_Click(object sender, RoutedEventArgs e)
        {
            foreach (Window window in Application.Current.Windows)
            {
                if (window.GetType() == typeof(MainWindow))
                {
                    (window as MainWindow).frmView.Source = new Uri("Contents/AddModule.xaml", UriKind.Relative);
                }
            }
        }


        /// <summary>
        /// Change Navigation From Quick Links
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tbModulesLink_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Navigation nav = new Navigation();
            ViewModules view_modules = new ViewModules();
            foreach (Window window in Application.Current.Windows)
            {
                if (window.GetType() == typeof(MainWindow))
                {
                    (window as MainWindow).frmView.Navigate(nav);
                    nav.frmView.Navigate(view_modules);
                }
            }
        }

        /// <summary>
        /// Change Navigation From Quick Links
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tbAddModuleLink_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Navigation nav = new Navigation();
            AddModule add_modules = new AddModule();
            foreach (Window window in Application.Current.Windows)
            {
                if (window.GetType() == typeof(MainWindow))
                {
                    (window as MainWindow).frmView.Navigate(nav);
                    nav.frmView.Navigate(add_modules);
                }
            }
        }
    }
}
