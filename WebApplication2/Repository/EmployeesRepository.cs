using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using WebApplication2.Models;

namespace WebApplication2.Repository
{
    public class EmployeesRepository
    {
        private SqlConnection sqlConn;

        public Employees GetEmployeesByUsername(string username)
        {
            Employees emp = new Employees();
            try
            {
                using (sqlConn = new SqlConnection(ConfigurationManager.ConnectionStrings["PROD"].ToString()))
                {
                    SqlCommand sqlComm = new SqlCommand("usp_GetEmployeesByUsername", sqlConn)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    sqlComm.Parameters.AddWithValue("@userName", username);
                    SqlDataAdapter da = new SqlDataAdapter(sqlComm);
                    DataTable dt = new DataTable();
                    sqlConn.Open();
                    da.Fill(dt);
                    if (dt.Rows.Count == 1)
                    {
                        emp.EmpId = Convert.ToInt32(dt.Rows[0]["EmpId"]);
                        emp.EmpFName = dt.Rows[0]["EmpFName"].ToString();
                        emp.EmpLName = dt.Rows[0]["EmpLName"].ToString();
                        emp.UserName = dt.Rows[0]["UserName"].ToString();
                        emp.Password = dt.Rows[0]["Password"].ToString();
                        emp.Role = dt.Rows[0]["Role"].ToString();
                        emp.ManagerId = dt.Rows[0]["ManagerId"] == DBNull.Value ? 0 : Convert.ToInt32(dt.Rows[0]["ManagerId"]);
                        emp.LeaveBalance = dt.Rows[0]["LeaveBalance"] == DBNull.Value ? 0 : Convert.ToInt32(dt.Rows[0]["LeaveBalance"]);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return emp;
        }
    }
}