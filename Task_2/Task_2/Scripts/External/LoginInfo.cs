namespace Task_2.Scripts.External
{
    public class LoginInfo
    {
        /// <summary>
        /// Property Variables For Fields
        /// </summary>
        private string student_Email;
        private string student_number;
        private string userPassword;

        private bool student_Exists;


        /// <summary>
        /// Property For Login Values
        /// </summary>
        public string Student_Email
        {
            get => student_Email;
            set => student_Email = value;
        }

        public string Student_Password
        {
            get => userPassword;
            set => userPassword = value;
        }

        public string Student_Number
        {
            get => student_number;
            set => student_number = value;
        }

        public bool Student_Exists
        {
            get => student_Exists;
            set => student_Exists = value;
        }
    }
}
