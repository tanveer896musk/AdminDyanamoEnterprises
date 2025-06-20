using AdminDyanamoEnterprises.DTOs;
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
       
       
        public void InsertorUpdateCategoryType(CategoryTypePageViewModel categoryType)
        {
            using (SqlConnection con = new SqlConnection(sqlConnection()))
            {
              
                using (SqlCommand cmd = new SqlCommand("Sp_InsertOrUpdateOrDeleteCategory", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@CategoryId", categoryType.AddCategory.CategoryID <= 0 ? 0 : categoryType.AddCategory.CategoryID);
                    cmd.Parameters.AddWithValue("@CategoryName", categoryType.AddCategory.CategoryName);
                    cmd.Parameters.AddWithValue("@Action", DBNull.Value); // not needed unless delete
                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public List<CategoryType>GetAllCategoryType()
        {
            List<CategoryType> categorynames = new List<CategoryType>();
            using (SqlConnection con = new SqlConnection(sqlConnection()))
            {
                SqlCommand cmd = new SqlCommand("SP_InsertCategory", con);
               
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                cmd.Parameters.AddWithValue("@Action", "select");
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);

                foreach (DataRow dr in dt.Rows)
                {
                    // Add each row's value to your list
                    CategoryType obj = new CategoryType()
                    {
                        CategoryName = dr["CategoryName"].ToString(),
                        CategoryID = Convert.ToInt32(dr["CategoryID"])
                    };
                    
                    categorynames.Add(obj);
                    /*categorynames.Add(new CategoryType
                    {
                        Name = dr["CategoryName"].ToString()
                    });*/
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
                SqlCommand cmd = new SqlCommand("Dynamo.Sp_InsertOrUpdateOrDeletePattern", con);
                cmd.CommandType = CommandType.StoredProcedure;

                // Required params
                cmd.Parameters.AddWithValue("@PatternID", DBNull.Value);
                cmd.Parameters.AddWithValue("@PatternName", DBNull.Value);
                cmd.Parameters.AddWithValue("@Action", "select");

                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);

                foreach (DataRow dr in dt.Rows)
                {
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
        
        public void InsertOrUpdateSubCategory(SubAddCategoryType model)
        {
            using (SqlConnection conn = new SqlConnection(sqlConnection()))
            {
                using (SqlCommand cmd = new SqlCommand("Sp_InsertOrUpdateOrDelete_MasterSubCategory", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Pass actual values
                    cmd.Parameters.AddWithValue("@SubCategoryId", model.SubCategoryID ?? 0);
                    cmd.Parameters.AddWithValue("@CategoryId", model.CategoryID);
                    cmd.Parameters.AddWithValue("@SubCategoryName", model.SubCategoryName ?? string.Empty);

                    // Do not pass @Action for insert/update
                    // cmd.Parameters.AddWithValue("@Action", DBNull.Value); // optional

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public List<SubCategoryType> GetAllSubCategoriesWithCategoryName()
        {
            var list = new List<SubCategoryType>();

            using (SqlConnection con = new SqlConnection(sqlConnection()))
            {
                using (SqlCommand cmd = new SqlCommand("Sp_InsertOrUpdateOrDelete_MasterSubCategory", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Action", "Select");

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
                            CategoryName = new CategoryType
                            {
                                CategoryName = dr["CategoryName"].ToString()
                            }
                        });
                    }
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
