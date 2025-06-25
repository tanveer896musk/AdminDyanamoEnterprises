using AdminDyanamoEnterprises.DTOs.Master;
using AdminDyanamoEnterprises.Repository.IRepository;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;

namespace AdminDyanamoEnterprises.Repository
{
    public class BannerRepository : IBannerRepository
    {
        private readonly IConfiguration _config;

        public BannerRepository(IConfiguration config)
        {
            _config = config;
        }

        public string sqlConnection()
        {
            return _config.GetConnectionString("DyanamoEnterprises_DB").ToString();
        }

        public List<BannerImage> GetBanners(string bannerType = null)
        {
            List<BannerImage> banners = new List<BannerImage>();

            using (SqlConnection conn = new SqlConnection(sqlConnection()))
            {
                using (SqlCommand cmd = new SqlCommand("GetBanners", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@IsActive", true);

                    if (!string.IsNullOrEmpty(bannerType))
                        cmd.Parameters.AddWithValue("@BannerType", bannerType);
                    else
                        cmd.Parameters.AddWithValue("@BannerType", DBNull.Value);

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    foreach (DataRow row in dt.Rows)
                    {
                        banners.Add(new BannerImage
                        {
                            BannerID = Convert.ToInt32(row["BannerID"]),
                            Image = row["Image"].ToString(),
                            CreatedDate = Convert.ToDateTime(row["CreatedDate"]),
                            Active = Convert.ToBoolean(row["Active"]),
                            BannerType = row["BannerType"].ToString()
                        });
                    }
                }
            }

            return banners;
        }

        public void AddBanner(string fileName, string bannerType)
        {
            using (SqlConnection conn = new SqlConnection(sqlConnection()))
            {
                using (SqlCommand cmd = new SqlCommand("AddBanner", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Image", fileName);
                    cmd.Parameters.AddWithValue("@CreatedDate", DateTime.Now);
                    cmd.Parameters.AddWithValue("@Active", true);
                    cmd.Parameters.AddWithValue("@BannerType", bannerType);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void DeleteBanner(int bannerId)
        {
            using (SqlConnection conn = new SqlConnection(sqlConnection()))
            {
                using (SqlCommand cmd = new SqlCommand("DeleteBanner", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@BannerID", bannerId);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}