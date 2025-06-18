using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminDyanamoEnterprises.DTOs
{
    public class CategoryType
    {
        public string ? Name { get; set; }
        public int CategoryID { get; set; }
    }
    public class AddCategoryType
    {
        public  string ? Name { get; set; }
    }
    public class UpdateCategoryType
    {
        public string ? Name { get; set; }
    }
    public class DeleteCategoryType
    {
        public int ? CategoryID { get; set; }
    }

    public class CategoryTypePageViewModel
    {
        public AddCategoryType AddCategory { get; set; } = new(); 
        public List<CategoryType> CategoryList { get; set; } = new();
    }

}
