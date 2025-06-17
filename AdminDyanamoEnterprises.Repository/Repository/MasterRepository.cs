using AdminDyanamoEnterprises.DTOs;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient; 

using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.Common;

namespace AdminDyanamoEnterprises.Repository
{
    public class MasterRepository:IMasterRepository
    {
        private readonly IConfiguration _config;

        public MasterRepository(IConfiguration config)
        {
            this._config = config;
        }
        public string SqlCon()
        {
            return _config.GetConnectionString("DyanamoEnterprises_DB").ToString();
        }

        public void InsertCategory(AddCategoryType addCategoryType)
        {
            
            }

         void IMasterRepository.AddCategory(CategoryTypePageViewModel categoryTypePageViewModel)
        {
            using (SqlConnection con = new SqlConnection(SqlCon()))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand("SP_InsertCategory", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@Action", "insertcategory");
                    cmd.Parameters.AddWithValue("@CategoryName", categoryTypePageViewModel.AddCategory.Name);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        List<CategoryType> IMasterRepository.GetAllListType()
        {
            List<CategoryType> categorynames = new List<CategoryType>();
            using (SqlConnection con = new SqlConnection(SqlCon()))
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
                        Name = dr["CategoryName"].ToString()
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





        //public List<CategoryType> GetAllListType()
        //{
        //    CategoryType categoryTypeObj = new CategoryType();
        //    using (SqlConnection connection = new SqlConnection(SqlConnection()))
        //    {

        //    }
        //    //return new categoryTypeObj;
        //}

        //void IMasterRepository.AddCategory(CategoryType categoryType)
        //{
        //    throw new NotImplementedException();
        //}
        //public void InsertCategoryType(CategoryType insertsubscriberDTOobj)
        //{
        //    try
        //    {
        //        using (var connection = new MySqlConnection(MySqlConnection()))
        //        {


        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw;
        //    }
        //}
    }
}
//}
