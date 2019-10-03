using _19_ADO_Assignment_02.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Web;
using System.Xml;
using System.Xml.Linq;

namespace _19_ADO_Assignment_02.Repository
{
    public class CustomerRepository
    {
        private SqlConnection sqlCon;

        #region SQL Injection
        public List<Customer> GetAllCustomers()
        {
            List<Customer> custList = new List<Customer>();
            try
            {
                using (sqlCon = new SqlConnection(ConfigurationManager.ConnectionStrings["PROD"].ConnectionString))
                {
                    SqlCommand sqlComm = new SqlCommand("SELECT * FROM Customer", sqlCon)
                    {
                        CommandType = CommandType.Text
                    };
                    SqlDataAdapter da = new SqlDataAdapter(sqlComm);
                    DataTable dt = new DataTable();
                    sqlCon.Open();
                    da.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dt.Rows)
                        {
                            custList.Add(new Customer
                            {
                                Custid = Convert.ToInt32(dr["Custid"]),
                                Custname = dr["Custname"].ToString(),
                                CustAddress = dr["CustAddress"].ToString(),
                                DOB = Convert.ToDateTime(dr["DOB"]),
                                Salary = Convert.ToInt64(dr["Salary"])
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return custList;
        }

        public Customer GetCustomerById(int custId)
        {
            Customer customer = new Customer();
            try
            {
                using (sqlCon = new SqlConnection(ConfigurationManager.ConnectionStrings["PROD"].ConnectionString))
                {
                    SqlCommand sqlComm = new SqlCommand("SELECT * FROM Customer WHERE Custid=" + custId, sqlCon)
                    {
                        CommandType = CommandType.Text
                    };
                    SqlDataAdapter da = new SqlDataAdapter(sqlComm);
                    DataTable dt = new DataTable();
                    sqlCon.Open();
                    da.Fill(dt);
                    if (dt.Rows.Count == 1)
                    {
                        customer.Custid = Convert.ToInt32(dt.Rows[0]["Custid"]);
                        customer.Custname = dt.Rows[0]["Custname"].ToString();
                        customer.CustAddress = dt.Rows[0]["CustAddress"].ToString();
                        customer.DOB = Convert.ToDateTime(dt.Rows[0]["DOB"]);
                        customer.Salary = Convert.ToInt64(dt.Rows[0]["Salary"]);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return customer;
        }

        public bool InsertCustomer(Customer customer)
        {
            bool returnVal = false;
            try
            {
                using (sqlCon = new SqlConnection(ConfigurationManager.ConnectionStrings["PROD"].ConnectionString))
                {
                    SqlCommand sqlComm = new SqlCommand("INSERT INTO Customer VALUES('" + customer.Custname + "','" + customer.CustAddress + "','" + customer.DOB.ToShortDateString() + "'," + customer.Salary + ")", sqlCon)
                    {
                        CommandType = CommandType.Text
                    };
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

        public bool UpdateCustomer(Customer customer)
        {
            bool returnVal = false;
            try
            {
                using (sqlCon = new SqlConnection(ConfigurationManager.ConnectionStrings["PROD"].ConnectionString))
                {
                    SqlCommand sqlComm = new SqlCommand("UPDATE Customer SET Custname ='" + customer.Custname + "', CustAddress='" + customer.CustAddress + "', DOB='" + customer.DOB + "', Salary=" + customer.Salary + " WHERE Custid=" + customer.Custid, sqlCon)
                    {
                        CommandType = CommandType.Text
                    };
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

        public bool DeleteCustomer(int custId)
        {
            bool returnVal = false;
            try
            {
                using (sqlCon = new SqlConnection(ConfigurationManager.ConnectionStrings["PROD"].ConnectionString))
                {
                    SqlCommand sqlComm = new SqlCommand("DELETE FROM Customer WHERE Custid=" + custId, sqlCon)
                    {
                        CommandType = CommandType.Text
                    };
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

        #endregion 

        public List<Customer> GetAllCustomersByDob(DateTime dob)
        {
            List<Customer> custList = new List<Customer>();
            try
            {
                using (sqlCon = new SqlConnection(ConfigurationManager.ConnectionStrings["PROD"].ConnectionString))
                {
                    SqlCommand sqlComm = new SqlCommand("usp_getCustomerByDob", sqlCon)
                    {
                        CommandType = CommandType.StoredProcedure,
                    };
                    sqlComm.Parameters.AddWithValue("@dob", dob);

                    SqlDataAdapter da = new SqlDataAdapter(sqlComm);
                    DataTable dt = new DataTable();
                    sqlCon.Open();
                    da.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dt.Rows)
                        {
                            custList.Add(new Customer
                            {
                                Custid = Convert.ToInt32(dr["Custid"]),
                                Custname = dr["Custname"].ToString(),
                                CustAddress = dr["CustAddress"].ToString(),
                                DOB = Convert.ToDateTime(dr["DOB"]),
                                Salary = Convert.ToInt64(dr["Salary"])
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return custList;
        }

        #region Stored Procedures
        public List<Customer> GetAllCustomersDs()
        {
            List<Customer> custList = new List<Customer>();
            try
            {
                using (sqlCon = new SqlConnection(ConfigurationManager.ConnectionStrings["PROD"].ConnectionString))
                {
                    SqlCommand sqlComm = new SqlCommand("usp_GetAllCustomer", sqlCon)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    SqlDataAdapter da = new SqlDataAdapter(sqlComm);
                    DataSet ds = new DataSet();
                    sqlCon.Open();
                    da.Fill(ds);
                    if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {
                            custList.Add(new Customer
                            {
                                Custid = Convert.ToInt32(dr["Custid"]),
                                Custname = dr["Custname"].ToString(),
                                CustAddress = dr["CustAddress"].ToString(),
                                DOB = Convert.ToDateTime(dr["DOB"]),
                                Salary = Convert.ToInt64(dr["Salary"])
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return custList;
        }

        public bool InsertCustomerDs(Customer customer)
        {
            bool returnVal = false;
            try
            {
                using (sqlCon = new SqlConnection(ConfigurationManager.ConnectionStrings["PROD"].ConnectionString))
                {
                    SqlCommand sqlComm = new SqlCommand("usp_InsertCustomer", sqlCon)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    sqlComm.Parameters.AddWithValue("@custname", customer.Custname);
                    sqlComm.Parameters.AddWithValue("@custAddress", customer.CustAddress);
                    sqlComm.Parameters.AddWithValue("@dOB", customer.DOB);
                    sqlComm.Parameters.AddWithValue("@salary", customer.Salary);
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

        public bool UpdateCustomerDs(Customer customer)
        {
            bool returnVal = false;
            try
            {
                using (sqlCon = new SqlConnection(ConfigurationManager.ConnectionStrings["PROD"].ConnectionString))
                {
                    SqlCommand sqlComm = new SqlCommand("usp_UpdateCustomer", sqlCon)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    sqlComm.Parameters.AddWithValue("@custid", customer.Custid);
                    sqlComm.Parameters.AddWithValue("@custname", customer.Custname);
                    sqlComm.Parameters.AddWithValue("@custAddress", customer.CustAddress);
                    sqlComm.Parameters.AddWithValue("@dOB", customer.DOB);
                    sqlComm.Parameters.AddWithValue("@salary", customer.Salary);
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

        public bool DeleteCustomerDs(int custId)
        {
            bool returnVal = false;
            try
            {
                using (sqlCon = new SqlConnection(ConfigurationManager.ConnectionStrings["PROD"].ConnectionString))
                {
                    SqlCommand sqlComm = new SqlCommand("usp_DeleteCustomer", sqlCon)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    sqlComm.Parameters.AddWithValue("@custid", custId);
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

        #endregion

        public void UploadXML(HttpPostedFileBase file)
        {
            // Conversion Xml file to DataTable
            DataTable dt = CreateDataTableXML(file);
            if (dt.Columns.Count == 0)
                dt.ReadXml(file.InputStream);

            // Creating Query for Table Creation
            string Query = CreateTableQuery(dt);
            using (sqlCon = new SqlConnection(ConfigurationManager.ConnectionStrings["PROD"].ConnectionString))
            {
                sqlCon.Open();

                // Deletion of Table if already Exist
                SqlCommand cmd = new SqlCommand("IF OBJECT_ID('dbo." +
            dt.TableName + "', 'U') IS NOT NULL DROP TABLE dbo." + dt.TableName + ";", sqlCon);
                cmd.ExecuteNonQuery();

                // Table Creation
                cmd = new SqlCommand(Query, sqlCon);
                int check = cmd.ExecuteNonQuery();
                if (check != 0)
                {
                    // Copy Data from DataTable to Sql Table
                    using (var bulkCopy = new SqlBulkCopy
                          (sqlCon.ConnectionString, SqlBulkCopyOptions.KeepIdentity))
                    {
                        foreach (DataColumn col in dt.Columns)
                        {
                            bulkCopy.ColumnMappings.Add(col.ColumnName, col.ColumnName);
                        }

                        bulkCopy.BulkCopyTimeout = 600;
                        bulkCopy.DestinationTableName = dt.TableName;
                        bulkCopy.WriteToServer(dt);
                    }
                }
            }
        }

        // Conversion XML file to DataTable
        public DataTable CreateDataTableXML(HttpPostedFileBase file)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(file.InputStream);

            DataTable dt = new DataTable();

            try
            {
                dt.TableName = Path.GetFileNameWithoutExtension(file.FileName);
                XmlNodeList NodoEstructura = doc.DocumentElement.ChildNodes;

                foreach (XmlNode columna in NodoEstructura)
                {
                    dt.Columns.Add(columna.Name, typeof(string));
                }

                XmlNode Filas = doc.DocumentElement;
                List<string> Valores = new List<string>();
                //  Data Rows 
                foreach (XmlNode Fila in Filas.ChildNodes)
                {
                    Valores.Add(Fila.InnerText);
                }
                dt.Rows.Add(Valores.ToArray());
                if (dt.Columns.Count == 0)
                {
                    dt.ReadXml(file.InputStream);
                }
            }
            catch (Exception ex)
            {

            }
            return dt;
        }

        // Getting Query for Table Creation
        public string CreateTableQuery(DataTable table)
        {
            string sqlsc = "CREATE TABLE " + table.TableName + "(";
            for (int i = 0; i < table.Columns.Count; i++)
            {
                sqlsc += "[" + table.Columns[i].ColumnName + "]";
                string columnType = table.Columns[i].DataType.ToString();
                switch (columnType)
                {
                    case "System.Int32":
                        sqlsc += " int ";
                        break;
                    case "System.Int64":
                        sqlsc += " bigint ";
                        break;
                    case "System.Int16":
                        sqlsc += " smallint";
                        break;
                    case "System.Byte":
                        sqlsc += " tinyint";
                        break;
                    case "System.Decimal":
                        sqlsc += " decimal ";
                        break;
                    case "System.DateTime":
                        sqlsc += " datetime ";
                        break;
                    case "System.String":
                    default:
                        sqlsc += string.Format(" nvarchar({0}) ",
                        table.Columns[i].MaxLength == -1 ? "max" :
                        table.Columns[i].MaxLength.ToString());
                        break;
                }
                if (table.Columns[i].AutoIncrement)
                    sqlsc += " IDENTITY(" + table.Columns[i].AutoIncrementSeed.ToString() +
                    "," + table.Columns[i].AutoIncrementStep.ToString() + ") ";
                if (!table.Columns[i].AllowDBNull)
                    sqlsc += " NOT NULL ";
                sqlsc += ",";
            }
            return sqlsc.Substring(0, sqlsc.Length - 1) + "\n)";
        }
    }
}