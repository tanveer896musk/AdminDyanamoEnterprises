using AdminDyanamoEnterprises.IRepository;

using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminDyanamoEnterprises.Repository
{
    public class ProductRepository:IProductRepository
    {
        private readonly IConfiguration _config;

        public ProductRepository(IConfiguration config)
        {
            _config = config;
        }

        private string sqlConnection()
        {
            return _config.GetConnectionString("DyanamoEnterprises_DB");
        }
    }
}
