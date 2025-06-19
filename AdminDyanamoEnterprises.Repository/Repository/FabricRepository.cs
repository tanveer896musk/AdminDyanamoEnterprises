using AdminDyanamoEnterprises.DTOs;
using AdminDyanamoEnterprises.DTOs.Master;
using AdminDyanamoEnterprises.Repository.IRepository;
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

namespace AdminDyanamoEnterprises.Repository.Repository
{
    public class FabricRepository : IFabricRepository
    {
        private readonly IConfiguration _config;

        public FabricRepository(IConfiguration config)
        {
            this._config = config;
        }
        public string sqlConnection()
        {
            return _config.GetConnectionString("DyanamoEnterprises_DB").ToString();
        }


        public void Sp_InsertOrUpdateOrDeleteFabric(FabricTypePageViewModel fabricType)
        {
            using (SqlConnection con = new SqlConnection(sqlConnection()))
            {
                using (SqlCommand cmd = new SqlCommand("Sp_InsertOrUpdateOrDeleteFabric", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    int id = fabricType.AddFabric.FabricID;
                    string action = id <= 0 ? "insert" : "update";

                    cmd.Parameters.AddWithValue("@FabricID", id);
                    cmd.Parameters.AddWithValue("@FabricName", fabricType.AddFabric.FabricName);
                    cmd.Parameters.AddWithValue("@Action", action);

                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }


        public List<FabricType> GetAllListType()
        {
            List<FabricType> fabricnames = new List<FabricType>();
            using (SqlConnection con = new SqlConnection(sqlConnection()))
            {
                SqlCommand cmd = new SqlCommand("Sp_InsertOrUpdateOrDeleteFabric", con);

                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                cmd.Parameters.AddWithValue("@FabricID", 0); // Dummy value
                cmd.Parameters.AddWithValue("@FabricName", DBNull.Value); // Dummy value
                cmd.Parameters.AddWithValue("@Action", "select");
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);

                foreach (DataRow dr in dt.Rows)
                {
                    // Add each row's value to your list
                    FabricType obj = new FabricType()
                    {
                        FabricName = dr["FabricName"].ToString(),
                        FabricID = Convert.ToInt32(dr["FabricID"])
                    };

                    fabricnames.Add(obj);
                }
            }
            return fabricnames;
        }

        public void DeleteFabric(int id)
        {
            using (SqlConnection con = new SqlConnection(sqlConnection()))
            {
                SqlCommand cmd = new SqlCommand("Sp_InsertOrUpdateOrDeleteFabric", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Action", "delete");
                cmd.Parameters.AddWithValue("@FabricID", id);

                con.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}