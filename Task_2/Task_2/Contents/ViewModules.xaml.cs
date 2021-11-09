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
        //Database connection String
        private const string connString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Course_Management;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        /// <summary>
        /// Class Declarations
        /// => Only Initialized Where Required
        /// </summary>
        ModuleModel module_model;
        private ModuleInfo module_info = new ModuleInfo();
        DispatcherTimer scheduler;

        private string Student_Number { get; set; }

        public ViewModules()
        {
            InitializeComponent();
            //Initialize Module Model Class
            module_model = new ModuleModel();
        }


        /// <summary>
        /// On Page Load
        /// Start Scheduler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            Student_Number = Properties.Settings.Default.Student_Number;
            scheduler = new DispatcherTimer();
            scheduler.Tick += new EventHandler(scheduler_tick);
            scheduler.Interval = new TimeSpan(0, 0, 10);
            scheduler.Start();
        }

        /// <summary>
        /// Calls Async Method To Update 
        /// UI Every x Seconds 
        /// If Necessary
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void scheduler_tick(object sender, EventArgs e)
        {

            if (pgLoading.Value < pgLoading.Maximum)
            {
                pgLoading.Value += 10;

            }
            else
            {
                pgLoading.Visibility = Visibility.Hidden;
            }

            Task<List<ModuleInfo>> module_data = Get_Modules();
            dgData.ItemsSource = await module_data;

            int count_modules = await Count_Modules(Student_Number);
            module_info.Module_Count = count_modules;

            int count_credits = await Count_Credits(Student_Number);
            module_info.Total_Credits = count_credits;

            int average_hours = await Average_Hours(Student_Number);
            module_info.Average_Time = average_hours;

            //Set DataContext For Data Binding
            DataContext = module_info;
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
            module_info.ModuleCode = txtModuleCode.Text;
            dgData.ItemsSource = null;

            string sql = "SELECT Student_Number, ModuleCode, ModuleName, Module_Credit, selfStudyHours FROM Module_Data WHERE Student_Number=@num AND ModuleCode=@code";

            using (var conn = new SqlConnection(connString))
            using (var cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.Add("@num", SqlDbType.VarChar, 55).Value = Student_Number;
                cmd.Parameters.Add("@code", SqlDbType.VarChar, 20).Value = module_info.ModuleCode;
                await conn.OpenAsync();

                SqlDataReader reader = await cmd.ExecuteReaderAsync();

                if (reader.Read())
                {
                    module_model.Add(new ModuleInfo
                    {
                        Student_Number = reader["Student_Number"].ToString(),
                        ModuleCode = reader["ModuleCode"].ToString(),
                        ModuleName = reader["ModuleName"].ToString(),
                        Module_Credit = int.Parse(reader["Module_Credit"].ToString()),
                        Study_Hours_Remaining = int.Parse(reader["selfStudyHours"].ToString())
                    });
                }

                //Set The Data source for the Datagrid Table
                dgData.ItemsSource = module_model;
            }
        }

        /// <summary>
        /// Return All Modules in Database
        /// => respectively by Student Number
        /// </summary>
        /// <returns></returns>
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
                            ModuleCode = reader["ModuleCode"].ToString(),
                            ModuleName = reader["ModuleName"].ToString(),
                            Module_Credit = int.Parse(reader["Module_Credit"].ToString()),
                            Study_Hours_Remaining = int.Parse(reader["selfStudyHours"].ToString())
                        });
                    }
                }
            });
            return module_model;
        }

        /// <summary>
        /// Async Task To Count All Modules For Each User
        /// => respectively From Student Number
        /// </summary>
        /// <param name="student_number"></param>
        /// <returns></returns>
        private async Task<int> Count_Modules(string student_number)
        {
            int count = 0;
            string sql = "SELECT COUNT(*) AS Total_Modules FROM Module_Data WHERE Student_Number=@num";

            using SqlConnection conn = new SqlConnection(connString);
            using SqlCommand cmd = new SqlCommand(sql, conn);

            cmd.Parameters.Add("@num", SqlDbType.VarChar, 55).Value = student_number;
            await conn.OpenAsync();

            count = (cmd.ExecuteScalar() as int?) ?? 0;

            return count;
        }

        /// <summary>
        /// Async Method To Count All Module Credits
        /// For each module in database
        /// => respectively by student Number
        /// </summary>
        /// <param name="student_number"></param>
        /// <returns></returns>
        private async Task<int> Count_Credits(string student_number)
        {
            int count = 0;
            string sql = "SELECT SUM(Module_Credit) AS TOTAL_CREDITS FROM Module_Data WHERE Student_Number=@num";

            using (var conn = new SqlConnection(connString))
            using (var cmd = new SqlCommand(sql, conn))
            {

                cmd.Parameters.Add("@num", SqlDbType.VarChar, 55).Value = student_number;
                await conn.OpenAsync();

                count = (cmd.ExecuteScalar() as int?) ?? 0;
            }

            return count;
        }

        /// <summary>
        /// Async Method To Return Average Studied Time
        /// </summary>
        /// <param name="student_Number"></param>
        /// <returns></returns>
        private async Task<int> Average_Hours(string student_Number)
        {
            int average = 0;
            string sql = "SELECT AVG(selfStudyHours) AS TOTAL_CREDITS FROM Module_Data WHERE Student_Number=@std";

            using (var conn = new SqlConnection(connString))
            using (var cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.Add("@std", SqlDbType.VarChar, 55).Value = student_Number;

                await conn.OpenAsync();

                average = (cmd.ExecuteScalar() as int?) ?? 0;
            }

            return average;
        }

        /// <summary>
        /// Add Styling To TextBlcok 
        /// in Datagrid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tbRemoveModule_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            TextBlock tbremove = sender as TextBlock;
            tbremove.TextDecorations = TextDecorations.Underline;
        }

        private void tbRemoveModule_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            TextBlock tbremove = sender as TextBlock;
            tbremove.TextDecorations = null;
        }


        /// <summary>
        /// Logic To Remove Module From Database 
        /// => TextBlock created in Datagrid
        /// for each module respectively
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void tbRemoveModule_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            TextBlock tbRemove = sender as TextBlock;
            ModuleInfo selected_row = tbRemove.DataContext as ModuleInfo;

            string moduleCode = selected_row.ModuleCode;


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
    }
}
