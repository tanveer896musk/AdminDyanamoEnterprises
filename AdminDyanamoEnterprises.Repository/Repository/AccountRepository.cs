using AdminDyanamoEnterprises.DTOs;
using AdminDyanamoEnterprises.DTOs.Master;
using AdminDyanamoEnterprises.Repository.IRepository;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AdminDyanamoEnterprises.Repository
{
    public class AccountRepository : IAccountRepository
    {
        private readonly IConfiguration _config;

        public AccountRepository(IConfiguration config)
        {
            this._config = config;
        }
        public string sqlConnection()
        {
            return _config.GetConnectionString("DyanamoEnterprises_DB").ToString();
        }
        public bool CheckLogin(Account loginType)
        {
            using (SqlConnection con = new SqlConnection(sqlConnection()))
            {
                using (SqlCommand cmd = new SqlCommand("sp_Loginuser", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@Emailid", loginType.Emailid);
                    cmd.Parameters.AddWithValue("@password", loginType.Password);

                    con.Open();
                    var result = cmd.ExecuteScalar();

                    return result != null && Convert.ToInt32(result) == 1;
                }
            }
        }


    }
}

