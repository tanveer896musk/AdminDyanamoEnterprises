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

        private string sqlConnection()
        {
            return _config.GetConnectionString("DyanamoEnterprises_DB");
        }


        public List<BannerImage> GetBanners(bool? isActive = null)
        {
            var banners = new List<BannerImage>();

            using (var conn = new SqlConnection(sqlConnection()))
            using (var cmd = new SqlCommand("GetBanners", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@IsActive", (object?)isActive ?? DBNull.Value);

                conn.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        banners.Add(new BannerImage
                        {
                            BannerID = Convert.ToInt32(reader["BannerID"]),
                            Image = reader["Image"].ToString(),
                            CreatedDate = Convert.ToDateTime(reader["CreatedDate"]),
                            Active = Convert.ToBoolean(reader["Active"])
                        });
                    }
                }
            }

            return banners;
        }

        public void AddBanner(string fileName)
        {
            using (var conn = new SqlConnection(sqlConnection()))
            using (var cmd = new SqlCommand("AddBanner", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Image", fileName);
                cmd.Parameters.AddWithValue("@CreatedDate", DateTime.Now);
                cmd.Parameters.AddWithValue("@Active", true);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }


        public void DeleteBanner(int bannerId)
        {
            using (var conn = new SqlConnection(sqlConnection()))
            using (var cmd = new SqlCommand("DeleteBanner", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@BannerID", bannerId);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}
