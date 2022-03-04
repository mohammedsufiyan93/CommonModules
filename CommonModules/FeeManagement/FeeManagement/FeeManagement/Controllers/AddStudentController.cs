using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;
using FeeManagement.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

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
                //string middleName = Request.Form["inputMname"];
                string lastName = Request.Form["inputLname"];
                string mobile = Request.Form["inputNumber"];
                string email = Request.Form["inputEmail"];
                string fathername = Request.Form["inputFathername"];
                string dob = Request.Form["inputDob"];
                string state = Request.Form["inputState"];
                string city = Request.Form["inputCity"];
                //string pincode = Request.Form["inputPincode"];
                string address = Request.Form["inputAddress"];

                string constr = "Data Source=DESKTOP-U9C38Q8;Initial Catalog=asoft_DB;Persist Security Info=True;User ID=Dev_asoft;Password=Dkb4o]zdem+luWhpanf8yEi%sgq7tc;";
                //string constr = "Data Source=198.38.83.200;Initial Catalog=amrullah_asofttestdb;Persist Security Info=True;User ID=amrullah_asofttest;Password=ygp0vmaouwfhibdtkcsn;";
                //string connectionlink = Src.DataHelper.Root.DataBaseConnection;
                //string getSqlCon = Src.DataHelper.Root.DataBaseConnection;

                using (SqlConnection SqlCon = new SqlConnection(constr))
                {
                    SqlCon.Open();
                    string query = "INSERT into asoft_DB.dbo.student ([FirstName],[LastName],[Email],[Mobile],[FatherName],[State],[City],[Address]) Values(@Firstname,@Lastname,@Email,@Mobile,@Fathername,@State,@City,@Address)";

                    SqlCommand SqlCmd = new SqlCommand(query, SqlCon);

                    //This is for required field
                    SqlCmd.Parameters.AddWithValue("@Firstname", firstName);
                    //SqlCmd.Parameters.AddWithValue("@Middlename", middleName);
                    SqlCmd.Parameters.AddWithValue("@Lastname", lastName);
                    SqlCmd.Parameters.AddWithValue("@Mobile", mobile);
                    SqlCmd.Parameters.AddWithValue("@Email", email);
                    SqlCmd.Parameters.AddWithValue("@Fathername", fathername);
                   // SqlCmd.Parameters.AddWithValue("@Dob", dob);
                    SqlCmd.Parameters.AddWithValue("@State", state);
                    SqlCmd.Parameters.AddWithValue("@City", city);
                    //SqlCmd.Parameters.AddWithValue("@Pincode", pincode);
                    SqlCmd.Parameters.AddWithValue("@Address", address);

                    // This is for not required field
                    //pass field name and value to AddSqlValue function which fields are not mandatory
                    //AddSqlValue(SqlCmd, "@DateofBirth", dob);
                    //AddSqlValue(SqlCmd, "@Mothername", motherName);
                    //AddSqlValue(SqlCmd, "@Fathername", fatherName);
                    //AddSqlValue(SqlCmd, "@Middlename", middleName);
                    //AddSqlValue(SqlCmd, "@Gender", gender);
                    //AddSqlValue(SqlCmd, "@ResidentInIndia", residentInIndia);
                    //AddSqlValue(SqlCmd, "@Religion", religion);

                    SqlCmd.ExecuteNonQuery();
                    SqlCon.Close();
                }

                return View("Successmsg");
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

        public ActionResult Successmsg()
        {
            return View();
        }
        //public ActionResult TestDropdown()
        //{
        //    SubjectModel model = new SubjectModel();
        //    model.SubjectList.Add(new SelectListItem { Text = "Physics", Value = "1" });
        //    model.SubjectList.Add(new SelectListItem { Text = "Chemistry", Value = "2" });
        //    model.SubjectList.Add(new SelectListItem { Text = "Mathematics", Value = "3" });
        //    return View();
        //}
    }
}
