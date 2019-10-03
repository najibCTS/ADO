using _18_ADO_Assignment_01.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace _18_ADO_Assignment_01.Repository
{
    public class SupplierRepository
    {
        private SqlConnection sqlCon;

        public List<Supplier> GetAllSuppliers()
        {
            List<Supplier> suppList = new List<Supplier>();
            try
            {
                using (sqlCon = new SqlConnection(ConfigurationManager.ConnectionStrings["PROD"].ConnectionString))
                {
                    SqlCommand sqlComm = new SqlCommand("usp_GetAllSuppliers", sqlCon);
                    sqlComm.CommandType = CommandType.StoredProcedure;
                    SqlDataAdapter da = new SqlDataAdapter(sqlComm);
                    DataTable dt = new DataTable();
                    sqlCon.Open();
                    da.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dt.Rows)
                        {
                            suppList.Add(new Supplier
                            {
                                SupplierId = Convert.ToInt32(dr["SupplierId"]),
                                SupplierName = dr["SupplierName"].ToString(),
                                Address = dr["Address"].ToString(),
                                City = dr["City"].ToString(),
                                ContactNo = Convert.ToInt64(dr["ContactNo"]),
                                Email = dr["Email"].ToString()
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return suppList;
        }

        public Supplier GetSupplierById(int supplierId)
        {
            Supplier supplier = new Supplier();
            try
            {
                using (sqlCon = new SqlConnection(ConfigurationManager.ConnectionStrings["PROD"].ConnectionString))
                {
                    SqlCommand sqlComm = new SqlCommand("usp_GetSupplierById", sqlCon);
                    sqlComm.CommandType = CommandType.StoredProcedure;
                    sqlComm.Parameters.AddWithValue("@supplierId", supplierId);
                    SqlDataAdapter da = new SqlDataAdapter(sqlComm);
                    DataTable dt = new DataTable();
                    sqlCon.Open();
                    da.Fill(dt);
                    if (dt.Rows.Count == 1)
                    {
                        supplier.SupplierId = Convert.ToInt32(dt.Rows[0]["SupplierId"]);
                        supplier.SupplierName = dt.Rows[0]["SupplierName"].ToString();
                        supplier.Address = dt.Rows[0]["Address"].ToString();
                        supplier.City = dt.Rows[0]["City"].ToString();
                        supplier.ContactNo = Convert.ToInt64(dt.Rows[0]["ContactNo"]);
                        supplier.Email = dt.Rows[0]["Email"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return supplier;
        }

        public bool InsertSupplier(Supplier supplier)
        {
            bool returnVal = false;
            try
            {
                using (sqlCon = new SqlConnection(ConfigurationManager.ConnectionStrings["PROD"].ConnectionString))
                {
                    SqlCommand sqlComm = new SqlCommand("usp_InsertSupplier", sqlCon);
                    sqlComm.CommandType = CommandType.StoredProcedure;
                    sqlComm.Parameters.AddWithValue("@supplierName", supplier.SupplierName);
                    sqlComm.Parameters.AddWithValue("@address", supplier.Address);
                    sqlComm.Parameters.AddWithValue("@city", supplier.City);
                    sqlComm.Parameters.AddWithValue("@contactNo", supplier.ContactNo);
                    sqlComm.Parameters.AddWithValue("@email", supplier.Email);
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

        public bool UpdateSupplier(Supplier supplier)
        {
            bool returnVal = false;
            try
            {
                using (sqlCon = new SqlConnection(ConfigurationManager.ConnectionStrings["PROD"].ConnectionString))
                {
                    SqlCommand sqlComm = new SqlCommand("usp_UpdateSupplier", sqlCon);
                    sqlComm.CommandType = CommandType.StoredProcedure;
                    sqlComm.Parameters.AddWithValue("@supplierId", supplier.SupplierId);
                    sqlComm.Parameters.AddWithValue("@supplierName", supplier.SupplierName);
                    sqlComm.Parameters.AddWithValue("@address", supplier.Address);
                    sqlComm.Parameters.AddWithValue("@city", supplier.City);
                    sqlComm.Parameters.AddWithValue("@contactNo", supplier.ContactNo);
                    sqlComm.Parameters.AddWithValue("@email", supplier.Email);
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

        public bool DeleteSupplier(int supplierId)
        {
            bool returnVal = false;
            try
            {
                using (sqlCon = new SqlConnection(ConfigurationManager.ConnectionStrings["PROD"].ConnectionString))
                {
                    SqlCommand sqlComm = new SqlCommand("usp_DeleteSupplier", sqlCon);
                    sqlComm.CommandType = CommandType.StoredProcedure;
                    sqlComm.Parameters.AddWithValue("@supplierId", supplierId);
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