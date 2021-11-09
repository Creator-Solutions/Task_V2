using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Data;
using System.Windows;
using Task_2.Scripts.External;

namespace Task_2.Scripts.Controllers
{
    /// <summary>
    /// 
    /// Class For Logical Methods
    /// 
    /// -> DB Queries
    /// -> Validation
    /// </summary>
    public class LoginController
    {
        private string connString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Course_Management;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        public async Task<bool> Login( string email, string password)
        {
            bool result = false; 
            //Security Class object to compare hash password;  
            var sec = new Security();

            string sql = "SELECT Student_Email, Student_Password, Student_Number FROM Student_Data WHERE Student_Email = @mail AND Student_Password = @pass";

            using (var conn = new SqlConnection(connString))
            using (var cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.Add("@mail", SqlDbType.VarChar, 55).Value = email;
                cmd.Parameters.Add("@pass", SqlDbType.VarChar, 55).Value =  sec.HashPassword(password);

                await conn.OpenAsync();
                SqlDataReader reader = await cmd.ExecuteReaderAsync();

                if (reader.Read())
                {
                    Properties.Settings.Default.Student_Number = reader["Student_Number"].ToString();
                    result = true;
                } else
                {
                    _ = MessageBox.Show("Incorrect Email Or Password", "Failed To Login", MessageBoxButton.OKCancel, MessageBoxImage.Error);
                }
            }

            return result;
        }


        public bool Valid_Inputs(string email, string password)
        {
            if (email.Equals(string.Empty) || password.Equals(string.Empty))
            {
                return false;
            } else
            {
                return true;
            }
        }
    }
}
