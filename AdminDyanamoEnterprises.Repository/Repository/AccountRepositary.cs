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

namespace AdminDyanamoEnterprises.Repository.Repository
{
    public class AccountRepositary : IAccountRepositary
    {
        private readonly IConfiguration _config;

        public AccountRepositary(IConfiguration config)
        {
            this._config = config;
        }
        public string sqlConnection()
        {
            return _config.GetConnectionString("DyanamoEnterprises_DB").ToString();
        }
        public bool CheckLogin(LoginType loginType)
        {
            using (SqlConnection con = new SqlConnection())
            {
                using (SqlCommand cmd = new SqlCommand("sp_Loginuser", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@Emailid", loginType.Emailid);
                    cmd.Parameters.AddWithValue("@password", loginType.Password);

                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);

                        return dt.Rows.Count > 0;
                    }
                }


            }
        }
    }
}

