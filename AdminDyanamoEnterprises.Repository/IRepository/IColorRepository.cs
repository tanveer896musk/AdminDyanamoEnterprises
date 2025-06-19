using AdminDyanamoEnterprises.DTOs;
using AdminDyanamoEnterprises.DTOs.Master;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminDyanamoEnterprises.Repository.IRepository
{
    public interface IColorRepository
    {
        List<ColorType> GetAllListType();

        void Sp_InsertOrUpdateOrDeleteColor(ColorTypePageViewModel colorType);

        void DeleteColor(int id);
    }
}