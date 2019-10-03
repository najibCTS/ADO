using _18_ADO_Assignment_01.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace _18_ADO_Assignment_01.Repository
{
    public class CategoryRepository
    {
        private SqlConnection sqlConn;

        public Category SearchCategory(Category category)
        {
            category.CategoryResults = new List<Category>();

            try
            {
                using (sqlConn = new SqlConnection(ConfigurationManager.ConnectionStrings["PROD"].ToString()))
                {
                    SqlCommand sqlComm = new SqlCommand("usp_SearchCategory", sqlConn);
                    sqlComm.CommandType = CommandType.StoredProcedure;
                    sqlComm.Parameters.Add("@category_code", SqlDbType.VarChar).Value = category.category_code ?? (object)DBNull.Value;
                    sqlComm.Parameters.Add("@division", SqlDbType.VarChar).Value = category.division ?? (object)DBNull.Value;
                    sqlComm.Parameters.Add("@region", SqlDbType.VarChar).Value = category.region ?? (object)DBNull.Value;
                    sqlComm.Parameters.Add("@supplier_id", SqlDbType.Int).Value = category.supplier_id == 0 ? (object)DBNull.Value : category.supplier_id;
                    sqlComm.Parameters.Add("@supplier_name", SqlDbType.VarChar).Value = category.supplier_name ?? (object)DBNull.Value;
                    SqlDataAdapter da = new SqlDataAdapter(sqlComm);
                    DataTable dt = new DataTable();
                    sqlConn.Open();
                    da.Fill(dt);

                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dt.Rows)
                        {
                            category.CategoryResults.Add(new Category
                            {
                                category_code = dr["category_code"].ToString(),
                                category_name = dr["category_name"].ToString(),
                                division = dr["division"].ToString(),
                                region = dr["region"].ToString(),
                                supplier_id = Convert.ToInt32(dr["supplier_id"]),
                                supplier_name = dr["supplier_name"].ToString()
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return category;
        }

        public Category GetCategoryByCode(string code)
        {
            Category category = new Category();

            try
            {
                using (sqlConn = new SqlConnection(ConfigurationManager.ConnectionStrings["PROD"].ToString()))
                {
                    SqlCommand sqlComm = new SqlCommand("usp_GetCategoryByCode", sqlConn);
                    sqlComm.CommandType = CommandType.StoredProcedure;
                    sqlComm.Parameters.Add("@category_code", SqlDbType.VarChar).Value = code ?? (object)DBNull.Value;
                    SqlDataAdapter da = new SqlDataAdapter(sqlComm);
                    DataTable dt = new DataTable();
                    sqlConn.Open();
                    da.Fill(dt);

                    if (dt.Rows.Count == 1)
                    {
                        category.category_code = dt.Rows[0]["category_code"].ToString();
                        category.category_name = dt.Rows[0]["category_name"].ToString();
                        category.division = dt.Rows[0]["division"].ToString();
                        category.region = dt.Rows[0]["region"].ToString();
                        category.supplier_id = Convert.ToInt32(dt.Rows[0]["supplier_id"]);
                        category.supplier_name = dt.Rows[0]["supplier_name"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return category;
        }

        public bool UpdateCategory(Category category)
        {
            bool result = false;
            try
            {
                using (sqlConn = new SqlConnection(ConfigurationManager.ConnectionStrings["PROD"].ToString()))
                {
                    SqlCommand sqlComm = new SqlCommand("usp_UpdateCategory", sqlConn);
                    sqlComm.CommandType = CommandType.StoredProcedure;
                    sqlComm.Parameters.Add("@category_code", SqlDbType.VarChar).Value = category.category_code ?? (object)DBNull.Value;
                    sqlComm.Parameters.Add("@division", SqlDbType.VarChar).Value = category.division ?? (object)DBNull.Value;
                    sqlComm.Parameters.Add("@region", SqlDbType.VarChar).Value = category.region ?? (object)DBNull.Value;

                    sqlConn.Open();
                    var rowsAffected = sqlComm.ExecuteNonQuery();
                    result = rowsAffected > 0 ? true : false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return result;
        }
    }
}