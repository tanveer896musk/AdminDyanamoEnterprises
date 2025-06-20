using AdminDyanamoEnterprises.DTOs;
using AdminDyanamoEnterprises.DTOs.Common;
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
       
        public MasterResponse InsertorUpdateCategoryType(CategoryTypePageViewModel addCategoryType);
        public List<CategoryType>GetAllCategoryType();

        public void DeleteCategory(int id);
        public void InsertOrUpdateOrDeletePattern(PatternTypePageViewModel addPatternType);

        public List<PatternType> GetAllPatternType();

        public void DeletePattern(int id);



        public void InsertOrUpdateSubCategory(SubAddCategoryType model);
        public void DeleteSubCategory(int subCategoryId);

        public List<SubCategoryType> GetAllSubCategoriesWithCategoryName();


    }
}
