using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace Task_2.Scripts.External
{
    /// <summary>
    /// Module Class For Property Values 
    /// -> Binding => OnProperty Changed
    /// </summary>
    public class ModuleInfo : INotifyPropertyChanged
    {

        private string student_number;
        private string moduleCode;
        private string moduleName;

        private int moduleCredit;
        private int studyHoursRemaining;
        private int modulecount;
        private int total_credits;
        private int average_time;

        public string Student_Number
        {
            get => student_number;
            set => student_number = value;
        }

        public string ModueCode
        {
            get => moduleCode;
            set => moduleCode = value;
        }

        public string ModuleName
        {
            get => moduleName;
            set => moduleName = value;
        }

        public int Module_Credit
        {
            get => moduleCredit;
            set => moduleCredit = value;
        }

        public int Module_Count
        {
            get => modulecount;
            set
            {
                modulecount = value;
                OnPropertyChanged();
            }
        }

        public int Total_Credits
        {
            get => total_credits;
            set
            {
                total_credits = value;
                OnPropertyChanged();
            }
        }

        public int Average_Time
        {
            get => average_time;
            set
            {
                average_time = value;
                OnPropertyChanged();
            }
        }


        public int Study_Hours_Remaining
        {
            get => studyHoursRemaining;
            set => studyHoursRemaining = value;
        }

        /// <summary>
        /// On Property Changed Event
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
