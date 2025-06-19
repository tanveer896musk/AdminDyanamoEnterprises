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
    public class ColorRepository : IColorRepository
    {
        private readonly IConfiguration _config;

        public ColorRepository(IConfiguration config)
        {
            this._config = config;
        }
        public string sqlConnection()
        {
            return _config.GetConnectionString("DyanamoEnterprises_DB").ToString();
        }


        public void Sp_InsertOrUpdateOrDeleteColor(ColorTypePageViewModel colorType)
        {
            using (SqlConnection con = new SqlConnection(sqlConnection()))
            {
                using (SqlCommand cmd = new SqlCommand("Sp_InsertOrUpdateOrDeleteColor", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    int id = colorType.AddColor.ColorID;
                    string action = id <= 0 ? "insert" : "update";

                    cmd.Parameters.AddWithValue("@ColorID", id);
                    cmd.Parameters.AddWithValue("@ColorName", colorType.AddColor.ColorName);
                    cmd.Parameters.AddWithValue("@Action", action);

                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }


        public List<ColorType> GetAllListType()
        {
            List<ColorType> colornames = new List<ColorType>();
            using (SqlConnection con = new SqlConnection(sqlConnection()))
            {
                SqlCommand cmd = new SqlCommand("Sp_InsertOrUpdateOrDeleteColor", con);

                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                cmd.Parameters.AddWithValue("@ColorID", 0); // Dummy value
                cmd.Parameters.AddWithValue("@ColorName", DBNull.Value); // Dummy value
                cmd.Parameters.AddWithValue("@Action", "select");
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);

                foreach (DataRow dr in dt.Rows)
                {
                    // Add each row's value to your list
                    ColorType obj = new ColorType()
                    {
                        ColorName = dr["ColorName"].ToString(),
                        ColorID = Convert.ToInt32(dr["ColorID"])
                    };

                    colornames.Add(obj);
                }
            }
            return colornames;
        }

        public void DeleteColor(int id)
        {
            using (SqlConnection con = new SqlConnection(sqlConnection()))
            {
                SqlCommand cmd = new SqlCommand("Sp_InsertOrUpdateOrDeleteColor", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Action", "delete");
                cmd.Parameters.AddWithValue("@ColorID", id);
                cmd.Parameters.AddWithValue("@ColorName", DBNull.Value); // ✅ ADD THIS

                con.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}