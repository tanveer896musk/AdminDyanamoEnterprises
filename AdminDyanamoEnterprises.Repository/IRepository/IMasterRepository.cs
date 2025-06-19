using AdminDyanamoEnterprises.DTOs;
using AdminDyanamoEnterprises.DTOs.Master;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminDyanamoEnterprises.Repository
{
    public interface IMasterRepository
    {
       
        public void InsertorUpdateCategoryType(CategoryTypePageViewModel addCategoryType);
        public List<CategoryType>GetAllCategoryType();

        public void DeleteCategory(int id);

        public void InsertOrUpdateSubCategory(SubAddCategoryType model);
        public void UpdateSubCategory(SubAddCategoryType model);

        public List<SubCategoryType> GetAllSubCategoriesWithCategoryName();

    }
}
