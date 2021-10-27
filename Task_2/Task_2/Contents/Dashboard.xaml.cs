using LiveCharts;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Task_2.Scripts.Controllers;
using Task_2.Scripts.External;

namespace Task_2.Views
{
    /// <summary>
    /// Interaction logic for Dashboard.xaml
    /// </summary>
    public partial class Dashboard : Page
    {
        private readonly string connString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Course_Management;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        DashboardController dash_contr = new DashboardController();

        public Dashboard()
        {
            DataContext = dash_contr;
            InitializeComponent();
        }

        private async void Border_Loaded(object sender, RoutedEventArgs e)
        {
            await Get_SemesterData();
            await Highest_Time();
            await LowestTime();
        }

        private async Task Get_SemesterData()
        {
            string student_number = Application.Current.Properties["Student_Number"].ToString();

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

        private async Task Highest_Time()
        {
            string student_number = Application.Current.Properties["Student_Number"].ToString();
            string sql = "SELECT Top(1) selfStudyHours, Required_Hours, ModuleCode FROM Module_Data WHERE Required_Hours > selfStudyHours AND Student_Number=@num";

            using (var conn = new SqlConnection(connString))
            using (var cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.Add("@num", SqlDbType.VarChar, 55).Value = student_number;

                await conn.OpenAsync();

                SqlDataReader reader = await cmd.ExecuteReaderAsync();

                if (reader.Read())
                {
                    dash_contr.Highest_Module = reader["ModuleCode"].ToString();
                    dash_contr.Highest_Time = int.Parse(reader["Required_Hours"].ToString());
                    dash_contr.Highest_Required_Time = int.Parse(reader["selfStudyHours"].ToString());
                }
            }
        }

        private async Task LowestTime()
        {
            string student_number = Application.Current.Properties["Student_Number"].ToString();
            string sql = "SELECT Top(1) selfStudyHours, Required_Hours, ModuleCode FROM Module_Data WHERE selfStudyHours > Required_Hours  AND Student_Number=@num";

            using (var conn = new SqlConnection(connString))
            using (var cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.Add("@num", SqlDbType.VarChar, 55).Value = student_number;

                await conn.OpenAsync();

                SqlDataReader reader = await cmd.ExecuteReaderAsync();

                if (reader.Read())
                {
                    dash_contr.Lowest_Module = reader["ModuleCode"].ToString();
                    dash_contr.Lowest_Time = int.Parse(reader["Required_Hours"].ToString());
                    dash_contr.Lowest_Required_Time = int.Parse(reader["selfStudyHours"].ToString());
                }
            }
        }

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

        private void btnViewModules_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void btnRemoveModules_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
