using _18_ADO_Assignment_01.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace _18_ADO_Assignment_01.Repository
{
    public class ProductRepository
    {
        private SqlConnection sqlCon;

        public List<Product> GetAllProducts()
        {
            List<Product> productList = new List<Product>();
            try
            {
                using (sqlCon = new SqlConnection(ConfigurationManager.ConnectionStrings["PROD"].ConnectionString))
                {
                    SqlCommand sqlComm = new SqlCommand("usp_GetAllProducts", sqlCon);
                    sqlComm.CommandType = CommandType.StoredProcedure;
                    SqlDataAdapter da = new SqlDataAdapter(sqlComm);
                    DataTable dt = new DataTable();
                    sqlCon.Open();
                    da.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dt.Rows)
                        {
                            productList.Add(new Product
                            {
                                ProductId = Convert.ToInt32(dr["ProductId"]),
                                ProductName = dr["ProductName"].ToString(),
                                SupplierId = Convert.ToInt32(dr["SupplierId"])
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return productList;
        }

        public Product GetProductById(int productId)
        {
            Product product = new Product();
            try
            {
                using (sqlCon = new SqlConnection(ConfigurationManager.ConnectionStrings["PROD"].ConnectionString))
                {
                    SqlCommand sqlComm = new SqlCommand("usp_GetProductById", sqlCon);
                    sqlComm.CommandType = CommandType.StoredProcedure;
                    sqlComm.Parameters.AddWithValue("@productId", productId);
                    SqlDataAdapter da = new SqlDataAdapter(sqlComm);
                    DataTable dt = new DataTable();
                    sqlCon.Open();
                    da.Fill(dt);
                    if (dt.Rows.Count == 1)
                    {
                        product.SupplierId = Convert.ToInt32(dt.Rows[0]["SupplierId"]);
                        product.ProductName = dt.Rows[0]["ProductName"].ToString();
                        product.ProductId = Convert.ToInt32(dt.Rows[0]["ProductId"]);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return product;
        }

        public bool InsertProduct(Product product)
        {
            bool returnVal = false;
            try
            {
                using (sqlCon = new SqlConnection(ConfigurationManager.ConnectionStrings["PROD"].ConnectionString))
                {
                    SqlCommand sqlComm = new SqlCommand("usp_InsertProduct", sqlCon);
                    sqlComm.CommandType = CommandType.StoredProcedure;
                    sqlComm.Parameters.AddWithValue("@productName", product.ProductName);
                    sqlComm.Parameters.AddWithValue("@supplierId", product.SupplierId);
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

        public bool UpdateProduct(Product product)
        {
            bool returnVal = false;
            try
            {
                using (sqlCon = new SqlConnection(ConfigurationManager.ConnectionStrings["PROD"].ConnectionString))
                {
                    SqlCommand sqlComm = new SqlCommand("usp_UpdateProduct", sqlCon);
                    sqlComm.CommandType = CommandType.StoredProcedure;
                    sqlComm.Parameters.AddWithValue("@productName", product.ProductName);
                    sqlComm.Parameters.AddWithValue("@supplierId", product.SupplierId);
                    sqlComm.Parameters.AddWithValue("@productId", product.ProductId);
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

        public bool DeleteProduct(int productId)
        {
            bool returnVal = false;
            try
            {
                using (sqlCon = new SqlConnection(ConfigurationManager.ConnectionStrings["PROD"].ConnectionString))
                {
                    SqlCommand sqlComm = new SqlCommand("usp_DeleteProduct", sqlCon);
                    sqlComm.CommandType = CommandType.StoredProcedure;
                    sqlComm.Parameters.AddWithValue("@productId", productId);
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