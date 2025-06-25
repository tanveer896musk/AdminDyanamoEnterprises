using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminDyanamoEnterprises.DTOs.Master
{
    public class BannerImage
    {
        public int BannerID { get; set; }
        public string Image { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool Active { get; set; }

        public string BannerType { get; set; }
    }
}
