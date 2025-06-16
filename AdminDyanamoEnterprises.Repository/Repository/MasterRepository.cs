using AdminDyanamoEnterprises.DTOs;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminDyanamoEnterprises.Repository
{
    public class MasterRepository:IMasterRepository
    {
        private readonly IConfiguration _config;

        public MasterRepository(IConfiguration config)
        {
            this._config = config;
        }

        public int AddCategory(CategoryType categoryType)
        {
            throw new NotImplementedException();
        }

        public string MySqlConnection()
        {
            return _config.GetConnectionString("DyanamoEnterprises_DB").ToString();
        }
        //public void InsertCategoryType(CategoryType insertsubscriberDTOobj)
        //{
        //    try
        //    {
        //        using (var connection = new MySqlConnection(MySqlConnection()))
        //        {
                   

        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw;
        //    }
        //}
    }
}
//}
