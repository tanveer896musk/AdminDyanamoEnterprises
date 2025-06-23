using AdminDyanamoEnterprises.DTOs;
using AdminDyanamoEnterprises.DTOs.Master;
using AdminDyanamoEnterprises.Repository;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

public class BlogsRepository : IBlogsRepository
{
    private readonly string _connectionString;
    private readonly IConfiguration _config;

    public BlogsRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("DyanamoEnterprises_DB");
    }
    public IEnumerable<BlogsModel> GetAllBlogs()
    {
        List<BlogsModel> blogsmodel = new List<BlogsModel>();
        using (SqlConnection con = new SqlConnection(_connectionString))
        {
            SqlCommand cmd = new SqlCommand("SP_ManageBlogs", con);

            cmd.CommandType = CommandType.StoredProcedure;
            con.Open();
            cmd.Parameters.AddWithValue("@Action", "GET");
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);

            foreach (DataRow dr in dt.Rows)
            {
                // Add each row's value to your list
                BlogsModel obj = new BlogsModel()
                {
                    BlogId = Convert.ToInt32(dr["BlogId"]),
                    Title = dr["Title"].ToString(),
                    Category = dr["Category"].ToString(),
                    PublishDate = Convert.ToDateTime(dr["PublishDate"]),
                    Description = dr["Description"].ToString(),
                    Author = dr["Author"].ToString(),
                    ImageUrl = dr["ImageUrl"].ToString()
                };
                blogsmodel.Add(obj);
            }
        }
        return blogsmodel;
    }
    public BlogsModel GetBlogById(int id)
    {
        BlogsModel blog = null;

        using (SqlConnection con = new SqlConnection(_connectionString)) // Replace with your actual connection method
        {
            SqlCommand cmd = new SqlCommand("SP_ManageBlogs", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Action", "GETBYID");
            cmd.Parameters.AddWithValue("@BlogId", id);

            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);

            if (dt.Rows.Count > 0)
            {
                DataRow dr = dt.Rows[0];
                blog = new BlogsModel()
                {
                    BlogId = Convert.ToInt32(dr["BlogId"]),
                    Title = dr["Title"].ToString(),
                    Category = dr["Category"].ToString(),
                    PublishDate = Convert.ToDateTime(dr["PublishDate"]),
                    Description = dr["Description"].ToString(),
                    Author = dr["Author"].ToString(),
                    ImageUrl = dr["ImageUrl"].ToString()
                };
            }
        }
        return blog;
    }


    public void InsertBlog(BlogsModel blog)
    {
        ExecuteCommand("INSERT", blog);
    }

    public void UpdateBlog(BlogsModel blog)
    {
        ExecuteCommand("UPDATE", blog);
    }

    public void DeleteBlog(int id)
    {
        using (SqlConnection con = new SqlConnection(_connectionString))
        {
            SqlCommand cmd = new SqlCommand("SP_ManageBlogs", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@Action", "DELETE");
            cmd.Parameters.AddWithValue("@BlogId", id);

            con.Open();
            cmd.ExecuteNonQuery();
        }
    }

    private void ExecuteCommand(string action, BlogsModel blog)
    {
        using (SqlConnection con = new SqlConnection(_connectionString))
        {
            SqlCommand cmd = new SqlCommand("SP_ManageBlogs", con)
            {
                CommandType = CommandType.StoredProcedure
            };
            cmd.Parameters.AddWithValue("@Action", action);
            cmd.Parameters.AddWithValue("@BlogId", blog.BlogId);
            cmd.Parameters.AddWithValue("@Title", blog.Title);
            cmd.Parameters.AddWithValue("@Category", blog.Category);
            cmd.Parameters.AddWithValue("@PublishDate", blog.PublishDate);
            cmd.Parameters.AddWithValue("@Description", blog.Description);
            cmd.Parameters.AddWithValue("@Author", blog.Author);
            cmd.Parameters.AddWithValue("@ImageUrl", blog.ImageUrl);

            con.Open();
            cmd.ExecuteNonQuery();
        }
    }
}
