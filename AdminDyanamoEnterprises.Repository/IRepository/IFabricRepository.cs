using AdminDyanamoEnterprises.DTOs;
using AdminDyanamoEnterprises.DTOs.Master;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminDyanamoEnterprises.Repository.IRepository
{
    public interface IFabricRepository
    {
        List<FabricType> GetAllListType();

        void Sp_InsertOrUpdateOrDeleteFabric(FabricTypePageViewModel fabricType);

        void DeleteFabric(int id);
    }
}