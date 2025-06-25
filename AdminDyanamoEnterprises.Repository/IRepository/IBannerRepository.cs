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
        List<BannerImage> GetBanners(string bannerType = null);
        void AddBanner(string fileName, string bannerType);
        void DeleteBanner(int bannerId);
    }
}