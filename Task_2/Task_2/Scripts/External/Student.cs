namespace Task_2.Scripts.External
{
    /// <summary>
    /// Class For Properties To Assign Fields
    /// </summary>
    public class Student
    {
        private string student_Name;
        private string student_Email;
        private string student_password;
        private string student_confirm_pass;
        private string student_number;

        private bool valid_credentials;
        private bool student_exists;
        private bool registration_successfull;

        public string Student_FullName
        {
            get => student_Name;
            set => student_Name = value;
        }

        public string Student_Email
        {
            get => student_Email;
            set => student_Email = value;
        }

        public string Password
        {
            get => student_password;
            set => student_password = value;
        }

        public string Student_Confirm_Password
        {
            get => student_confirm_pass;
            set => student_confirm_pass = value;
        }

        public string Student_Number
        {
            get => student_number;
            set => student_number = value;
        }

        public bool Valid_Credentials
        {
            get => valid_credentials;
            set => valid_credentials = value;
        }

        public bool Student_Exists
        {
            get => student_exists;
            set => student_exists = value;
        }

        public bool Registration_Successful
        {
            get => registration_successfull;
            set => registration_successfull = value;
        }
    }
}
