using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Data;
using Task_2.Scripts.Controllers;
using Task_2.Scripts.External;
using System;
using System.Windows.Threading;
using System.Threading;

namespace Task_2.Views
{
    /// <summary>
    /// Interaction logic for ViewModules.xaml
    /// </summary>
    public partial class ViewModules : Page
    {

        private const string connString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Course_Management;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        ModuleModel module_model;
        private ModuleInfo module_info = new ModuleInfo();
        DispatcherTimer scheduler;


        private int Counter { get; set; }
        private string Student_Number { get; set; }

        public ViewModules()
        {
            InitializeComponent();
            Student_Number = Application.Current.Properties["Student_Number"].ToString();
            module_model = new ModuleModel();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            scheduler = new DispatcherTimer();
            scheduler.Tick += new EventHandler(scheduler_tick);
            scheduler.Interval = new TimeSpan(0, 0, 60);
            scheduler.Start();
        }

        private async void scheduler_tick(object sender, EventArgs e)
        {

            if (pgLoading.Value < pgLoading.Maximum)
            {
                pgLoading.Value += 10;

            } else
            {
                pgLoading.Visibility = Visibility.Hidden;
            }

            Task<List<ModuleInfo>> module_data = Get_Modules();
            dgData.ItemsSource = await module_data;

            Task<int> count_modules = Count_Modules(Student_Number);
            module_info.Module_Count = await count_modules;

            Task<int> count_credits = Count_Credits(Student_Number);
            module_info.Total_Credits = await count_credits;

            Task<int> average_hours = Average_Hours(Student_Number);
            module_info.Average_Time = await average_hours;

            DataContext = module_info;
        }

        /// <summary>
        /// Main Logic To Delete Modules
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void btnView_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            ModuleInfo selected_row = button.DataContext as ModuleInfo;

            string moduleCode = selected_row.ModueCode;


            string sql = "DELETE FROM Module_Data WHERE ModuleCode=@code AND Student_Number=@stdNumber";

            using (var conn = new SqlConnection(connString))
            using (var cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.Add("@stdNumber", SqlDbType.VarChar, 55).Value = Student_Number;
                cmd.Parameters.Add("@code", SqlDbType.VarChar, 20).Value = moduleCode;

                await conn.OpenAsync();
                await cmd.ExecuteNonQueryAsync();
            }

        }

        /// <summary>
        /// Main Logic To Search For a Module
        /// in the database
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void tbSearch_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            module_model.Clear();
            module_info.ModueCode = txtModuleCode.Text;
            dgData.ItemsSource = null;           

            string sql = "SELECT Student_Number, ModuleCode, ModuleName, Module_Credit, selfStudyHours FROM Module_Data WHERE Student_Number=@num AND ModuleCode=@code";

            using (var conn = new SqlConnection(connString))
            using (var cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.Add("@num", SqlDbType.VarChar, 55).Value = Student_Number;
                cmd.Parameters.Add("@code", SqlDbType.VarChar, 20).Value = module_info.ModueCode;
                await conn.OpenAsync();

                SqlDataReader reader = await cmd.ExecuteReaderAsync();

                if (reader.Read())
                {
                    module_model.Add(new ModuleInfo
                    {
                        Student_Number = reader["Student_Number"].ToString(),
                        ModueCode = reader["ModuleCode"].ToString(),
                        ModuleName = reader["ModuleName"].ToString(),
                        Module_Credit = int.Parse(reader["Module_Credit"].ToString()),
                        Study_Hours_Remaining = int.Parse(reader["selfStudyHours"].ToString())
                    });
                }

                dgData.ItemsSource = module_model;                
            }
        }

        private async Task<List<ModuleInfo>> Get_Modules()
        {
            await Task.Run(async () =>
            {
                string sql = "SELECT Student_Number, ModuleCode, ModuleName, Module_Credit, selfStudyHours FROM Module_Data WHERE Student_Number=@num";

                using (var conn = new SqlConnection(connString))
                using (var cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.Add("@num", SqlDbType.VarChar, 55).Value = Student_Number;

                    await conn.OpenAsync();

                    SqlDataReader reader = await cmd.ExecuteReaderAsync();

                    module_model = new ModuleModel();

                    while (reader.Read())
                    {
                        module_model.Add(new ModuleInfo
                        {
                            Student_Number = reader["Student_Number"].ToString(),
                            ModueCode = reader["ModuleCode"].ToString(),
                            ModuleName = reader["ModuleName"].ToString(),
                            Module_Credit = int.Parse(reader["Module_Credit"].ToString()),
                            Study_Hours_Remaining = int.Parse(reader["selfStudyHours"].ToString())
                        });
                    }
                }
            });
            return module_model;
        }

        private async Task<int> Count_Modules(string student_number)
        {
            int count = 0;
            string sql = "SELECT COUNT(*) AS Total_Modules FROM Module_Data WHERE Student_Number=@num";

            using SqlConnection conn = new SqlConnection(connString);
            using SqlCommand cmd = new SqlCommand(sql, conn);

            cmd.Parameters.Add("@num", SqlDbType.VarChar, 55).Value = student_number;
            await conn.OpenAsync();

            count = int.Parse(cmd.ExecuteScalar().ToString());

            return count;
        }

        private async Task<int> Count_Credits(string student_number)
        {
            int count = 0;
            string sql = "SELECT SUM(Module_Credit) AS TOTAL_CREDITS FROM Module_Data WHERE Student_Number=@num";

            using (var conn = new SqlConnection(connString))
            using (var cmd = new SqlCommand(sql, conn))
            {

                cmd.Parameters.Add("@num", SqlDbType.VarChar, 55).Value = student_number;
                await conn.OpenAsync();

                count = int.Parse(cmd.ExecuteScalar().ToString());
            }

            return count;
        }

        private async Task<int> Average_Hours(string student_Number)
        {
            int average = 0;
            string sql = "SELECT AVG(selfStudyHours) AS TOTAL_CREDITS FROM Module_Data WHERE Student_Number=@std";

            using (var conn = new SqlConnection(connString))
            using (var cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.Add("@std", SqlDbType.VarChar, 55).Value = student_Number;

                await conn.OpenAsync();

                average = int.Parse(cmd.ExecuteScalar().ToString());
            }

            return average;
        }       
    }
}
