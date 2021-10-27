using System;
using System.Collections.Generic;
using System.Text;

namespace Task_2.Scripts.External
{
    public class Modules
    {

        private string moduleName;
        private string moduleCode;
        private string semesterStartDate;
        private string studentNumber;

        private int moduleCredits;
        private int classHours;
        private int semester_length;

        private int selfStudyHours;

        private bool moduleExists;

        public bool Module_Exists
        {
            get => moduleExists;
            set => moduleExists = value;
        }

        public string Semester_Start_Date
        {
            get => semesterStartDate;
            set => semesterStartDate = value;
        }

        public string StudentNumber
        {
            get => studentNumber;
            set => studentNumber = value;
        }

        public string ModulesName
        {
            get => moduleName;
            set => moduleName = value;
        }

        public string ModuleCode
        {
            get => moduleCode;
            set => moduleCode = value;
        }

        public int Self_Study_Hours
        {
            get => selfStudyHours;
            set => selfStudyHours = value;
        }

        public int ModuleCredits
        {
            get => moduleCredits;
            set => moduleCredits = value;
        }

        public int ClassHours
        {
            get => classHours;
            set => classHours = value;
        }

        public int SemesterLength
        {
            get => semester_length;
            set => semester_length = value;
        }
    }

    public class CustomModuleHours
    {

        private string modCode;
        private string student_Number;

        private int hours_studied;

        public string Module_Code
        {
            get => modCode;
            set => modCode = value;
        }

        public string Student_Number
        {
            get => student_Number;
            set => student_Number = value;
        }

        public int Hours_Studied
        {
            get => hours_studied;
            set => hours_studied = value;
        }


    }
}
