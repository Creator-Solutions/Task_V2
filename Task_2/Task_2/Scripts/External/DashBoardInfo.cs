using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace Task_2.Scripts.External
{
    /// <summary>
    /// Dashboard class for binding
    /// </summary>
    public class DashBoardInfo : INotifyPropertyChanged
    {
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

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
