using AdminDyanamoEnterprises.DTOs;
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
        public List<CategoryType> GetAllCategoryType();

        public void DeleteCategory(int id);
        public MasterResponse InsertOrUpdateOrDeletePattern(PatternTypePageViewModel addPatternType);

        public List<PatternType> GetAllPatternType();

        public void DeletePattern(int id);

        public MasterResponse InsertOrUpdateSubCategory(SubAddCategoryType model);
        public string DeleteSubCategory(int subCategoryId);

        public List<SubCategoryType> GetAllSubCategoriesWithCategoryName();
        
        public List<SubCategoryType> GetSubCategoriesByCategoryId(int categoryId);

        List<ColorType> GetAllListColorType();

        string Sp_InsertOrUpdateOrDeleteColor(ColorTypePageViewModel colorType);

        string DeleteColor(int id);


        List<FabricType> GetAllListFabricType();

        string Sp_InsertOrUpdateOrDeleteFabric(FabricTypePageViewModel fabricType);

        string DeleteFabric(int id);

        List<MaterialType> GetAllListMaterialType();

        string Sp_InsertOrUpdateOrDeleteMaterialType(MaterialTypePageViewModel materialType);

        string DeleteMaterial(int id);


    }
}
