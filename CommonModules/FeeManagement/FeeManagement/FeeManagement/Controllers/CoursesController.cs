using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace FeeManagement.Controllers
{
    public class CoursesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult AddStudent()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Courcesubmitquery()
        {
            try
            {

                string coursename = Request.Form["inputCname"];
                string coursecost = Request.Form["inputCcost"];

                string constr = "Data Source=DESKTOP-U9C38Q8;Initial Catalog=asoft_DB;Persist Security Info=True;User ID=Dev_asoft;Password=Dkb4o]zdem+luWhpanf8yEi%sgq7tc;";

                using (SqlConnection SqlCon = new SqlConnection(constr))
                {
                    SqlCon.Open();
                    string query = "INSERT into asoft_DB.dbo.course ([Course_Name],[Course_Cost]) Values(@Coursename,@Coursecost)";

                    SqlCommand SqlCmd = new SqlCommand(query, SqlCon);

                    //This is for required field
                    SqlCmd.Parameters.AddWithValue("@Coursename", coursename);
                    SqlCmd.Parameters.AddWithValue("@Coursecost", coursecost);

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
                return View("Coursesuccess");
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
        public ActionResult Coursesuccess()
        {
            return View();
        }
        public ActionResult AddCource()
        {
            return View();
        }
    }
}
