using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace FeeManagement.Controllers
{
    public class AddStudentController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Volunter()
        {
            //return View(new VolunteerModel());
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Voluntersubmitquery()
        {
            try
            {
                string firstName = Request.Form["inputFname"];
                string middleName = Request.Form["inputMname"];
                string lastName = Request.Form["inputLname"];
                string mobile = Request.Form["inputNumber"];
                string email = Request.Form["inputEmail"];

                //string connectionlink = Src.DataHelper.Root.DataBaseConnection;                                
                string getSqlCon = Src.DataHelper.Root.DataBaseConnection;

                using (SqlConnection SqlCon = new SqlConnection(getSqlCon))
                {
                    SqlCon.Open();
                    //string query = "INSERT into dbo.SystemUser Values(@Firstname,@Middlename,@Lastname,@Mothername,@Fathername,@DateofBirth,@Gender, @ResidentInIndia, @Religion, @Qualification, @Profession, @City, @State, @Pincode, @Telephone, @Mobile,@Email, @Hobli, @Taluk, @District, @Municipality, @WardNumberandName, @AssemblyConstitunecy, @LokSabhaConstituency, @FaceBookProfile, @TwitterProfile )";
                    //string query = "INSERT into SystemUser Values(@Firstname,@Middlename,@Lastname, @Mobile,@Email)";
                    //string query = "INSERT into SystemUser ([Firstname], [Middlename], [Lastname], [Mothername], [Fathername], [DateofBirth], [Gender], [ResidentInIndia], [Religion], [Qualification], [Profession], [City], [State], [Pincode], [Telephone], [Mobile], [Hobli], [Taluk], [District], [Municipality], [WardNumberandName], [AssemblyConstituency], [LokSabhaConstituency], [FaceBookProfile], [TwitterProfile]) VALUES(@Firstname ,@Middlename, @Lastname, @Mothername, @Fathername, @DateofBirth, @Gender, @ResidentInIndia, @Religion, @Qualification, @Profession, @City, @State, @Pincode, @Telephone, @Mobile, @Email, @Hobli, @Taluk, @District, @Municipality, @WardNumberandName, @AssemblyConstituency, @LokSabhaConstituency, @FaceBookProfile, @TwitterProfile)";
                    string query = "INSERT into FeeManagement ([Firstname], [Middlename], [Lastname],[Mobile], [Email]) VALUES(@Firstname ,@Middlename, @Lastname, @Mobile, @Email)";
                    //string query = "INSERT into SystemUser ([Firstname], [Middlename], [Lastname], [Mobile], [Email]) VALUES(@Firstname ,@Middlename, @Lastname, @Mobile, @Email)";
                    SqlCommand SqlCmd = new SqlCommand(query, SqlCon);

                    //This is for required field
                    SqlCmd.Parameters.AddWithValue("@Firstname", firstName);
                    SqlCmd.Parameters.AddWithValue("@Lastname", lastName);
                    SqlCmd.Parameters.AddWithValue("@Mobile", mobile);
                    SqlCmd.Parameters.AddWithValue("@Email", email);

                    //This is for not required field
                    //pass field name and value to AddSqlValue function which fields are not mandatory
                    //AddSqlValue(SqlCmd, "@DateofBirth", dob);
                    //AddSqlValue(SqlCmd, "@Mothername", motherName);
                    //AddSqlValue(SqlCmd, "@Fathername", fatherName);
                    //AddSqlValue(SqlCmd, "@Middlename", middleName);
                    //AddSqlValue(SqlCmd, "@Gender", gender);
                    //AddSqlValue(SqlCmd, "@ResidentInIndia", residentInIndia);
                    //AddSqlValue(SqlCmd, "@Religion", religion);

                    SqlCmd.ExecuteNonQuery();
                    //SqlCon.Close();
                }

                return View("VolunteroSuccessMessage");
            }
            catch (Exception ex)
            {
                return Ok(" " + ex.Message + " " + ex.StackTrace);
            }
        }
        //Function to check and pass null values to sql database for those fields which contains null values or empty.
        //This function is for not required field
        private void AddSqlValue(SqlCommand sqlCommand, string fieldName, string value)
        {
            SqlParameter sqlParameter;
            if (value != null)
            {
                sqlParameter = new SqlParameter(fieldName, value);
            }
            else
            {
                sqlParameter = new SqlParameter(fieldName, DBNull.Value);
            }

            sqlCommand.Parameters.Add(sqlParameter);
        }

    }
}
