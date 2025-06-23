using AdminDyanamoEnterprises.DTOs;
using AdminDyanamoEnterprises.DTOs.Common;
using AdminDyanamoEnterprises.DTOs.Master;
using Microsoft.Data.SqlClient; 
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AdminDyanamoEnterprises.Repository
{
    public class MasterRepository:IMasterRepository
    {
        private readonly IConfiguration _config;

        public MasterRepository(IConfiguration config)
        {
            this._config = config;
        }
        public string sqlConnection()
        {
            return _config.GetConnectionString("DyanamoEnterprises_DB").ToString();
        }

        public MasterResponse InsertorUpdateCategoryType(CategoryTypePageViewModel categoryType)
        {
            MasterResponse response = new MasterResponse();

            using (SqlConnection con = new SqlConnection(sqlConnection()))
            {
                using (SqlCommand cmd = new SqlCommand("Dynamo.Sp_InsertOrUpdateOrDeleteCategory", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    int categoryId = categoryType.AddCategory.CategoryID <= 0 ? 0 : categoryType.AddCategory.CategoryID;
                    string action = categoryId == 0 ? "insert" : "update";

                    cmd.Parameters.AddWithValue("@CategoryID", categoryId);
                    cmd.Parameters.AddWithValue("@CategoryName", categoryType.AddCategory.CategoryName);
                    cmd.Parameters.AddWithValue("@Action", action);

                    // Add OUTPUT parameters
                    SqlParameter errorCodeParam = new SqlParameter("@ErrorCode", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Output
                    };
                    SqlParameter returnMsgParam = new SqlParameter("@ReturnMessage", SqlDbType.NVarChar, 200)
                    {
                        Direction = ParameterDirection.Output
                    };

                    cmd.Parameters.Add(errorCodeParam);
                    cmd.Parameters.Add(returnMsgParam);

                    con.Open();
                    cmd.ExecuteNonQuery();

                    response.ErrorCode = (int)errorCodeParam.Value;
                    response.ReturnMessage = returnMsgParam.Value.ToString();
                }
            }

            return response;
        }


        public List<CategoryType> GetAllCategoryType()
        {
            List<CategoryType> categorynames = new List<CategoryType>();
            using (SqlConnection con = new SqlConnection(sqlConnection()))
            {
                SqlCommand cmd = new SqlCommand("Dynamo.Sp_InsertOrUpdateOrDeleteCategory", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@CategoryID", DBNull.Value);
                cmd.Parameters.AddWithValue("@CategoryName", DBNull.Value);
                cmd.Parameters.AddWithValue("@Action", "select");

                // Add dummy output parameters (since SP expects them)
                cmd.Parameters.Add("@ErrorCode", SqlDbType.Int).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@ReturnMessage", SqlDbType.NVarChar, 200).Direction = ParameterDirection.Output;

                con.Open();
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);

                foreach (DataRow dr in dt.Rows)
                {
                    CategoryType obj = new CategoryType()
                    {
                        CategoryName = dr["CategoryName"].ToString(),
                        CategoryID = Convert.ToInt32(dr["CategoryID"])
                    };

                    categorynames.Add(obj);
                }
            }
            return categorynames;
        }



        public void DeleteCategory(int id)
        {
            using (SqlConnection con = new SqlConnection(sqlConnection()))
            {
                SqlCommand cmd = new SqlCommand("SP_InsertCategory", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Action", "delete");
                cmd.Parameters.AddWithValue("@CategoryId", id);

                con.Open();
                cmd.ExecuteNonQuery();
            }
        }
        public void InsertOrUpdateOrDeletePattern(PatternTypePageViewModel PatternType)
        {
            using (SqlConnection con = new SqlConnection(sqlConnection()))
            {
                using (SqlCommand cmd = new SqlCommand("SP_InsertOrUpdateOrDeletePattern", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@PatternId", PatternType.AddPattern.PatternID <= 0 ? 0 : PatternType.AddPattern.PatternID);
                    cmd.Parameters.AddWithValue("@PatternName", PatternType.AddPattern.PatternName ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@Action", PatternType.AddPattern.PatternID > 0 ? "update" : "insert");

                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }
        public List<PatternType> GetAllPatternType()
        {
            List<PatternType> PatternNames = new List<PatternType>();
            using (SqlConnection con = new SqlConnection(sqlConnection()))
            {
                SqlCommand cmd = new SqlCommand("SP_InsertOrUpdateOrDeletePattern", con);

                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                cmd.Parameters.AddWithValue("@Action", "select");
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);

                foreach (DataRow dr in dt.Rows)
                {
                    // Add each row's value to your list
                    PatternType obj = new PatternType()
                    {
                        PatternName = dr["PatternName"].ToString(),
                        PatternID = Convert.ToInt32(dr["PatternID"])
                    };

                    PatternNames.Add(obj);
                }
            }
            return PatternNames;
        }
        public void DeletePattern(int id)
        {
            using (SqlConnection con = new SqlConnection(sqlConnection()))
            {
                SqlCommand cmd = new SqlCommand("SP_InsertOrUpdateOrDeletePattern", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Action", "delete");
                cmd.Parameters.AddWithValue("@PatternID", id);

                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public MasterResponse InsertOrUpdateSubCategory(SubAddCategoryType model)
        {
            MasterResponse response = new MasterResponse();

            using (SqlConnection conn = new SqlConnection(sqlConnection()))
            {
                using (SqlCommand cmd = new SqlCommand("Sp_InsertOrUpdateOrDelete_MasterSubCategory", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@SubCategoryId", model.SubCategoryID ?? 0);
                    cmd.Parameters.AddWithValue("@CategoryId", model.CategoryID);
                    cmd.Parameters.AddWithValue("@SubCategoryName", model.SubCategoryName ?? string.Empty);

                    // Output parameters
                    SqlParameter errorCodeParam = new SqlParameter("@ErrorCode", SqlDbType.Int) { Direction = ParameterDirection.Output };
                    SqlParameter returnMsgParam = new SqlParameter("@ReturnMessage", SqlDbType.NVarChar, 200) { Direction = ParameterDirection.Output };

                    cmd.Parameters.Add(errorCodeParam);
                    cmd.Parameters.Add(returnMsgParam);

                    conn.Open();
                    cmd.ExecuteNonQuery();

                    response.ErrorCode = Convert.ToInt32(errorCodeParam.Value);
                    response.ReturnMessage = returnMsgParam.Value.ToString();
                }
            }

            return response;
        }


        public List<SubCategoryType> GetAllSubCategoriesWithCategoryName()
        {
            var list = new List<SubCategoryType>();

            using (SqlConnection con = new SqlConnection(sqlConnection()))
            {
                using (SqlCommand cmd = new SqlCommand("Sp_InsertOrUpdateOrDelete_MasterSubCategory", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Mandatory parameters
                    cmd.Parameters.AddWithValue("@Action", "select");

                    // Output parameters (required by the procedure)
                    SqlParameter errorCodeParam = new SqlParameter("@ErrorCode", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Output
                    };
                    SqlParameter returnMessageParam = new SqlParameter("@ReturnMessage", SqlDbType.NVarChar, 200)
                    {
                        Direction = ParameterDirection.Output
                    };

                    cmd.Parameters.Add(errorCodeParam);
                    cmd.Parameters.Add(returnMessageParam);

                    con.Open();
                    SqlDataAdapter sda = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    sda.Fill(dt);

                    foreach (DataRow dr in dt.Rows)
                    {
                        list.Add(new SubCategoryType
                        {
                            SubCategoryName = dr["SubCategoryName"].ToString(),
                            SubCategoryID = Convert.ToInt32(dr["SubCategoryID"]),
                            CategoryID = Convert.ToInt32(dr["CategoryID"]),
                            CategoryName = new CategoryType
                            {
                                CategoryName = dr["CategoryName"].ToString()
                            }
                        });
                    }

                    
                    var errorCode = errorCodeParam.Value;
                    var returnMsg = returnMessageParam.Value?.ToString();
                }
            }

            return list;
        }

        public void DeleteSubCategory(int subCategoryId)
        {
            using (SqlConnection con = new SqlConnection(sqlConnection()))
            {
                SqlCommand cmd = new SqlCommand("Sp_InsertOrUpdateOrDelete_MasterSubCategory", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@SubCategoryId", subCategoryId);
                cmd.Parameters.AddWithValue("@Action", "delete");

                con.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}
