using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using Task_2.Scripts.External;

namespace Task_2.Scripts.Controllers
{
    /// <summary>
    /// Student Controller Class
    /// => Handles
    /// {
    ///     Validation => Regular Expressions,
    ///     User Exist => bool
    ///     Registering User => Async Task
    /// }
    /// </summary>
    public class StudentController
    {
        //Database connection string
        const string connString = "Data Source = (localdb)\\MSSQLLocalDB;Initial Catalog = Course_Management; Integrated Security = True; Connect Timeout = 30; Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        //Property For Response Control
        private bool User_Exist { get; set; }

        /// <summary>
        /// Validate Student Name
        /// </summary>
        /// <param name="name"></param>
        /// <returns>bool</returns>
        public bool Validate_FullName(string name)
        {
            if (Regex.IsMatch(name, @"^[a-zA-Z\s]+$"))
            {
                return true;
            }
            else
            {
                //Return Message Based on Condition
                _ = MessageBox.Show("Invalid name specified", "Name can only contain letters");
                return false;
            }

        }

        /// <summary>
        /// Validate Student Email
        /// </summary>
        /// <param name="email"></param>
        /// <returns>bool</returns>
        public bool Validate_Email(string email)
        {
            if (Regex.IsMatch(email, @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$"))
            {
                return true;
            }
            else
            {
                //Return Message Based on Condition
                _ = MessageBox.Show("Please enter a correct email", "Invalid email specified");
                return false;
            }
        }

        /// <summary>
        /// Validate Student Password
        /// </summary>
        /// <param name="password"></param>
        /// <returns>bool</returns>
        public bool Validate_Password(string password)
        {
            if (Regex.IsMatch(password, @"^.*(?=.{8,})(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[!*@#$%^&+=]).*$"))
            {
                return true;
            }
            else
            {
                //Return Message Based on Condition
                _ = MessageBox.Show("Password is not valid", "Password Invalid");
                return false;
            }
        }

        /// <summary>
        /// Confirm Password Matches Before registering
        /// </summary>
        /// <param name="password"></param>
        /// <param name="cnfm_password"></param>
        /// <returns>bool</returns>
        public bool IsMatched(string password, string cnfm_password)
        {
            return cnfm_password.Equals(password);
        }

        /// <summary>
        /// Validate Student Number
        /// </summary>
        /// <param name="student_number"></param>
        /// <returns>bool</returns>
        public bool Valid_StudentNumber(string student_number)
        {
            foreach (char c in student_number.ToCharArray())
            {
                if (char.IsDigit(c))
                {
                    return true;
                }
            }
            //Return Message Based on Condition
            _ = MessageBox.Show("Student Number can only contain digits", "Student Number Invalid");
            return false;

        }

        /// <summary>
        /// Check if Fields Are Empty
        /// => Validate All Fields
        /// </summary>
        /// <param name="name"></param>
        /// <param name="studentNumber"></param>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <param name="confirm_password"></param>
        /// <returns>bool</returns>
        public bool Valid_Credentials(string name, string studentNumber, string email, string password, string confirm_password)
        {
            if (name == string.Empty || studentNumber.ToString() == string.Empty || email == string.Empty || password == string.Empty || confirm_password == string.Empty)
            {
                _ = MessageBox.Show("Please fill in all fields", "An Error Occurred", MessageBoxButton.OKCancel, MessageBoxImage.Error);
                return false;
            }

            return Validate_FullName(name) && Validate_Email(email) && Valid_StudentNumber(studentNumber) && Validate_Password(password) && IsMatched(password, confirm_password);
        }

        /// <summary>
        /// if Fields Valid
        /// => Register Student
        /// </summary>
        /// <param name="name"></param>
        /// <param name="studentNumber"></param>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <returns>bool</returns>
        public async Task<bool> Create_Student(string name, string studentNumber, string email, string password)
        {
            var security = new Security();

            if (!User_Exist)
            {
                string sql = "INSERT INTO Student_Data (Student_Number, Student_FullName, Student_Email, Student_Password)"
                                + "VALUES (@num, @name, @email, @pass)";

                using (var conn = new SqlConnection(connString))
                using (var cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.Add("@num", SqlDbType.VarChar, 55).Value = studentNumber;
                    cmd.Parameters.Add("@name", SqlDbType.VarChar, 55).Value = name;
                    cmd.Parameters.Add("@email", SqlDbType.VarChar, 55).Value = email;
                    cmd.Parameters.Add("@pass", SqlDbType.VarChar, 55).Value = security.HashPassword(password);

                    await conn.OpenAsync();

                    try
                    {
                        await cmd.ExecuteNonQueryAsync();
                        return true;
                    }catch (Exception ex)
                    {
                        //Return message with exception if caught
                        _ = MessageBox.Show($"Could not register an account at this time\n Please try again Later {ex.Message}", "An Error Occurred", MessageBoxButton.OKCancel, MessageBoxImage.Error);
                    }
                    
                }
            }

            return false;
        }

        /// <summary>
        /// Check if Student Credentials
        /// Already exists
        /// </summary>
        /// <param name="student_number"></param>
        /// <param name="email"></param>
        /// <returns>bool</returns>
        public async Task<bool> User_Exists(string student_number, string email)
        {
            string sql = "SELECT Student_Number, Student_Email FROM Student_Data WHERE Student_Number=@num OR Student_Email=@email";

            using (var conn = new SqlConnection(connString))
            using (var cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.Add("@num", SqlDbType.VarChar, 55).Value = student_number;
                cmd.Parameters.Add("@email", SqlDbType.VarChar, 20).Value = email;

                await conn.OpenAsync();

                User_Exist = await cmd.ExecuteScalarAsync() != null;
            }

            return User_Exist;

        }
    }
}
