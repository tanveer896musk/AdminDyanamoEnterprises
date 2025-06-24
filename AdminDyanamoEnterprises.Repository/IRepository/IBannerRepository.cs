using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdminDyanamoEnterprises.DTOs.Master;

namespace AdminDyanamoEnterprises.Repository.IRepository
{
    public interface IBannerRepository
    {
        List<BannerImage> GetBanners(bool? isActive = null);
        void AddBanner(string fileName);
        void DeleteBanner(int bannerId);
    }
}
