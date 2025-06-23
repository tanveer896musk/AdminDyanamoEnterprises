using AdminDyanamoEnterprises.DTOs.Master;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminDyanamoEnterprises.Repository
{
    public interface IBlogsRepository
    {
        IEnumerable<BlogsModel> GetAllBlogs();
        BlogsModel GetBlogById(int id);
        void InsertBlog(BlogsModel blog);
        void UpdateBlog(BlogsModel blog);
        void DeleteBlog(int id);
    }
}
