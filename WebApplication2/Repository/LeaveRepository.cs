using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using WebApplication2.Models;

namespace WebApplication2.Repository
{
    public class LeaveRepository
    {
        private SqlConnection sqlCon;
        private EmployeesRepository empRepo = new EmployeesRepository();

        public List<LeaveRequest> GetEmployeeLeaves(Employees employee)
        {
            List<LeaveRequest> leaveList = new List<LeaveRequest>();
            try
            {
                using (sqlCon = new SqlConnection(ConfigurationManager.ConnectionStrings["PROD"].ConnectionString))
                {
                    SqlCommand sqlComm = new SqlCommand("usp_GetEmployeeLeaves", sqlCon);
                    sqlComm.CommandType = CommandType.StoredProcedure;
                    sqlComm.Parameters.AddWithValue("@employeeId", employee.EmpId);
                    SqlDataAdapter da = new SqlDataAdapter(sqlComm);
                    DataTable dt = new DataTable();
                    sqlCon.Open();
                    da.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dt.Rows)
                        {
                            leaveList.Add(new LeaveRequest
                            {
                                LeaveRequestId = Convert.ToInt32(dr["LeaveRequestId"]),
                                RequestorId = Convert.ToInt32(dr["RequestorId"]),
                                FromDate = Convert.ToDateTime(dr["FromDate"]),
                                ToDate = Convert.ToDateTime(dr["ToDate"]),
                                NoOfDays = Convert.ToInt32(dr["NoOfDays"]),
                                Status = dr["Status"].ToString(),
                                ApproverId = Convert.ToInt32(dr["ApproverId"]),
                                Reason = dr["Reason"].ToString()
                            });
                        }
                    }
                }
                if (leaveList.Count > 0)
                {
                    leaveList[0].Employees = empRepo.GetEmployeesByUsername(employee.UserName);
                }
                else
                {
                    leaveList.Add(new LeaveRequest
                    {
                        Employees = empRepo.GetEmployeesByUsername(employee.UserName)
                    });
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return leaveList;
        }

        public bool ApplyLeave(LeaveRequest leaveRequest, string userName)
        {
            bool returnVal = false;
            try
            {
                double noOfDays = (leaveRequest.ToDate - leaveRequest.FromDate).TotalDays;
                leaveRequest.Employees = empRepo.GetEmployeesByUsername(userName);
                using (sqlCon = new SqlConnection(ConfigurationManager.ConnectionStrings["PROD"].ConnectionString))
                {
                    SqlCommand sqlComm = new SqlCommand("usp_InsertLeaveRequest", sqlCon);
                    sqlComm.CommandType = CommandType.StoredProcedure;
                    sqlComm.Parameters.AddWithValue("@requestorId", leaveRequest.Employees.EmpId);
                    sqlComm.Parameters.AddWithValue("@fromDate", leaveRequest.FromDate);
                    sqlComm.Parameters.AddWithValue("@toDate", leaveRequest.ToDate);
                    sqlComm.Parameters.AddWithValue("@noOfDays", noOfDays);
                    sqlComm.Parameters.AddWithValue("@status", "Pending approval");
                    sqlComm.Parameters.AddWithValue("@reason", leaveRequest.Reason);
                    sqlComm.Parameters.AddWithValue("@contactNo", leaveRequest.ContactNo);
                    sqlComm.Parameters.AddWithValue("@approverId", leaveRequest.Employees.ManagerId);
                    sqlCon.Open();
                    var result = sqlComm.ExecuteNonQuery();
                    if (result > 0)
                        returnVal = true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return returnVal;
        }

        public List<LeaveRequest> GetReporteesLeaves(Employees employee)
        {
            List<LeaveRequest> leaveList = new List<LeaveRequest>();
            try
            {
                using (sqlCon = new SqlConnection(ConfigurationManager.ConnectionStrings["PROD"].ConnectionString))
                {
                    SqlCommand sqlComm = new SqlCommand("usp_GetReporteesLeaves", sqlCon);
                    sqlComm.CommandType = CommandType.StoredProcedure;
                    sqlComm.Parameters.AddWithValue("@employeeId", employee.EmpId);
                    SqlDataAdapter da = new SqlDataAdapter(sqlComm);
                    DataTable dt = new DataTable();
                    sqlCon.Open();
                    da.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dt.Rows)
                        {
                            leaveList.Add(new LeaveRequest
                            {
                                LeaveRequestId = Convert.ToInt32(dr["LeaveRequestId"]),
                                RequestorId = Convert.ToInt32(dr["RequestorId"]),
                                FromDate = Convert.ToDateTime(dr["FromDate"]),
                                ToDate = Convert.ToDateTime(dr["ToDate"]),
                                NoOfDays = Convert.ToInt32(dr["NoOfDays"]),
                                Status = dr["Status"].ToString(),
                                ApproverId = Convert.ToInt32(dr["ApproverId"]),
                                Reason = dr["Reason"].ToString(),
                                ContactNo = dr["ContactNo"].ToString()
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return leaveList;
        }

        public bool ApproveLeave(LeaveRequest leaveRequest)
        {
            bool returnVal = false;
            try
            {
                using (sqlCon = new SqlConnection(ConfigurationManager.ConnectionStrings["PROD"].ConnectionString))
                {
                    SqlCommand sqlComm = new SqlCommand("usp_ApproveLeave", sqlCon);
                    sqlComm.CommandType = CommandType.StoredProcedure;
                    sqlComm.Parameters.AddWithValue("@leaveRequestId", leaveRequest.LeaveRequestId);
                    sqlComm.Parameters.AddWithValue("@requestorId", leaveRequest.RequestorId);
                    sqlComm.Parameters.AddWithValue("@noOfDays", leaveRequest.NoOfDays);
                    sqlCon.Open();
                    var result = sqlComm.ExecuteNonQuery();
                    if (result > 0)
                        returnVal = true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return returnVal;
        }
    }
}