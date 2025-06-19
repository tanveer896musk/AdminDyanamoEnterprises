using AdminDyanamoEnterprises.DTOs;
using AdminDyanamoEnterprises.DTOs.Master;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminDyanamoEnterprises.Repository.IRepository
{
    public interface IMaterialRepository
    {
        List<MaterialType> GetAllListType();

        void Sp_InsertOrUpdateOrDeleteMaterialType(MaterialTypePageViewModel materialType);

        void DeleteMaterial(int id);
    }
}
