using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;

namespace Task_2.Scripts.Controllers
{
    public class ModuleController
    {


        const string connString = "Data Source = (localdb)\\MSSQLLocalDB;Initial Catalog = Course_Management; Integrated Security = True; Connect Timeout = 30; Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        /// <summary>
        /// Check if module already exists before 
        /// adding to database
        ///</summary>
        public async Task<bool> Module_Exists(string module_code, string student_number)
        {
            bool result = false;

            string sql = "SELECT Student_Number, ModuleCode FROM Module_Data WHERE Student_Number = @num AND ModuleCode=@code";

            using (var conn = new SqlConnection(connString))
            using (var cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.Add("@num", SqlDbType.VarChar, 55).Value = student_number;
                cmd.Parameters.Add("@code", SqlDbType.VarChar, 20).Value = module_code;

                await conn.OpenAsync();

                result = await cmd.ExecuteScalarAsync() != null;

            }

            return result;
        }

        /// <summary>
        /// Main Logic To Add Module 
        /// To The Database in Background
        /// </summary>
        /// <param name="student_number"></param>
        /// <param name="semesterLength"></param>
        /// <param name="semesterStart"></param>
        /// <param name="module_code"></param>
        /// <param name="module_name"></param>
        /// <param name="mod_Credits"></param>
        /// <param name="classHours"></param>
        /// <param name="studyhours"></param>
        /// <returns></returns>
        public async Task Add_Module(string student_number, int semesterLength, string semesterStart, string module_code, string module_name, int mod_Credits, int classHours, int studyhours)
        {

            string sql = "INSERT INTO Module_Data (Student_Number, Semester_Start_Date, Semester_Week_Length, ModuleCode, ModuleName, Module_Credit, Module_Class_Hours, selfStudyHours)" +
                         "VALUES (@stdNumber, @semStartDate, @semLength, @modCode, @modName, @modCredit, @classhours, @selfstudyhrs)";

            using (var conn = new SqlConnection(connString))
            using (var cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.Add("@stdNumber", SqlDbType.VarChar, 55).Value = student_number;
                cmd.Parameters.Add("@semLength", SqlDbType.Int, 8).Value = semesterLength;
                cmd.Parameters.Add("@semStartDate", SqlDbType.VarChar, 20).Value = semesterStart;
                cmd.Parameters.Add("@modCode", SqlDbType.VarChar, 20).Value = module_code;
                cmd.Parameters.Add("@modName", SqlDbType.VarChar, 255).Value = module_name;
                cmd.Parameters.Add("@modCredit", SqlDbType.Int, 8).Value = mod_Credits;
                cmd.Parameters.Add("@classhours", SqlDbType.Int, 8).Value = classHours;
                cmd.Parameters.Add("@selfstudyhrs", SqlDbType.Int, 8).Value = studyhours;

                await conn.OpenAsync();

                await cmd.ExecuteNonQueryAsync();
            }
        }

        /// <summary>
        /// Get The Hours Studied From 
        /// Database
        /// </summary>
        /// <param name="modCode"></param>
        /// <param name="stdNumber"></param>
        /// <returns></returns>
        public async Task<int> Get_Studied_Hours(string modCode, string stdNumber)
        {
            int hours = 0;
            string sql = "SELECT selfStudyHours FROM Module_Data WHERE Student_Number=@num AND ModuleCode=@code";

            using (var conn = new SqlConnection(connString))
            using (var cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.Add("@num", SqlDbType.VarChar, 55).Value = stdNumber;
                cmd.Parameters.Add("@code", SqlDbType.VarChar, 20).Value = modCode;

                await conn.OpenAsync();

                hours = (cmd.ExecuteScalar() as int?) ?? 0;
            }

            return hours;

        }

        /// <summary>
        /// Subtract Database study time 
        /// and custom study time
        /// </summary>
        /// <param name="custom_hours"></param>
        /// <param name="studied_hours"></param>
        /// <returns></returns>
        public int Subtract_Hours(int custom_hours, int studied_hours)
        {
            return studied_hours - custom_hours;
        }

        /// <summary>
        /// Update New Time To Database
        /// => Module and Student Number 
        /// respectively
        /// </summary>
        /// <param name="modCode"></param>
        /// <param name="stdNumber"></param>
        /// <param name="hours"></param>
        /// <returns></returns>
        public async Task Update_Module_Hours(string modCode, string stdNumber, int hours)
        {
            string sql = "UPDATE Module_Data SET selfStudyHours=@hours WHERE ModuleCode=@code AND Student_Number=@std";

            using (var conn = new SqlConnection(connString))
            using (var cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.Add("@code", SqlDbType.VarChar, 20).Value = modCode;
                cmd.Parameters.Add("@hours", SqlDbType.Int, 8).Value = hours;
                cmd.Parameters.Add("@std", SqlDbType.VarChar, 55).Value = stdNumber;

                await conn.OpenAsync();

                await cmd.ExecuteNonQueryAsync();
            }
        }
 
    }
}
