using LiveCharts;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using Task_2.Scripts.External;
using System.Windows;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Task_2.Scripts.Controllers
{
    public class DashboardController: INotifyPropertyChanged
    {
       
        const string connString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Course_Management;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";


        private int currentYear;
        private int highest_time;
        private int highest_required_time;
        private int lowest_time;
        private int lowest_required_time;


        private int semester_length;
        private string semesterStartDate;
        private string highest_module;
        private string lowest_module;

        public int Current_Year
        {
            get => currentYear;
            set
            {
                currentYear = value;
                OnPropertyChanged();
            }
        }

        public int Highest_Time
        {
            get => highest_time;
            set
            {
                highest_time = value;
                OnPropertyChanged();
            }
        }

        public int Highest_Required_Time
        {
            get => highest_required_time;
            set
            {
                highest_required_time = value;
                OnPropertyChanged();
            }
        }

        public int Lowest_Time
        {
            get => lowest_time;
            set
            {
                lowest_time = value;
                OnPropertyChanged();
            }
        }

        public int Lowest_Required_Time
        {
            get => lowest_required_time;
            set
            {
                lowest_required_time = value;
                OnPropertyChanged();
            }
        }

        public string Highest_Module
        {
            get => highest_module;
            set
            {
                highest_module = value;
                OnPropertyChanged();
            }

        }

        public string Lowest_Module
        {
            get => lowest_module;
            set
            {
                lowest_module = value;
                OnPropertyChanged();
            }
        }

        public int Semester_Length
        {
            get => semester_length;
            set
            {
                semester_length = value;
                OnPropertyChanged();
            }

        }

        public string Semester_Start_Date
        {
            get => semesterStartDate;
            set
            {
                semesterStartDate = value;
                OnPropertyChanged();
            }

        }

       
        public DashboardController()
        {

            Labels = new List<string>();
            Study_Hours = new List<int>();
            string sql = "SELECT ModuleCode, selfStudyHours FROM Module_Data WHERE Student_Number=@num";

            using (var conn = new SqlConnection(connString))
            using (var cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.Add("@num", SqlDbType.VarChar, 55).Value = Application.Current.Properties["Student_Number"];

                conn.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Labels.Add(reader["ModuleCode"].ToString());
                    Study_Hours.Add(int.Parse(reader["selfStudyHours"].ToString()));                    
                }
            }


            SeriesCollection = new SeriesCollection
            {
                new ColumnSeries
                {
                    Title = "Hours Required",
                    Values = new ChartValues<int> (Study_Hours)
                },             
            };

            Formatter = value => value.ToString("N");

            PointLabel = chartPoint =>
               string.Format("{0} ({1:P})", chartPoint.Y, chartPoint.Participation);
        }        

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        /// <summary>
        /// Lists for Axis Labels and Values
        /// </summary>
        public List<string> Labels { get; set; }
        public List<int> Study_Hours { get; set; }

        //Series Collection For The Column Chart
        public SeriesCollection SeriesCollection { get; set; }
        public Func<ChartPoint, string> PointLabel { get; set; }
        public Func<double, string> Formatter { get; set; }
    }
}
