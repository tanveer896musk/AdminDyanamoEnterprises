using AdminDyanamoEnterprises.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminDyanamoEnterprises.Repository
{
    public interface IMasterRepository
    {
       
        public void AddCategory(CategoryTypePageViewModel addCategoryType);
        public List<CategoryType> GetAllListType();
    }
}
