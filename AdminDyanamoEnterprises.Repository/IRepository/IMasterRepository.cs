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
        public List<CategoryType>GetAllListType();

        public void DeleteCategory(int id);
        public void InsertOrUpdateOrDeletePattern(PatternTypePageViewModel addPatternType);

        public List<PatternType> GetAllPatternType();

        public void DeletePattern(int id);


    }
}
