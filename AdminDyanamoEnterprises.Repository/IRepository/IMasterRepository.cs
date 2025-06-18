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
        public void InsertOrUpdateOrDeleteFabric(FabricTypePageViewModel addFabricType);

        public List<FabricType> GetAllFabricType();

        public void DeleteFabric(int id);


    }
}
