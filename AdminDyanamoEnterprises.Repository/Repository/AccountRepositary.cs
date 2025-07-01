using AdminDyanamoEnterprises.DTOs;
using AdminDyanamoEnterprises.DTOs.Account;
using AdminDyanamoEnterprises.Repository;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
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
        public bool CheckLogin(LoginType loginType)
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

        public (int ErrorCode, string ErrorMessage) RegisterUser(RegisterType registerType)
        {
            using (SqlConnection con = new SqlConnection(sqlConnection()))
            {
                using (SqlCommand cmd = new SqlCommand("SP_InsertUser", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@Fullname", registerType.Fullname);
                    cmd.Parameters.AddWithValue("@Emailid", registerType.Emailid);
                    cmd.Parameters.AddWithValue("@mobileno", registerType.mobileno);
                    cmd.Parameters.AddWithValue("@password", registerType.password);

                    // Define output parameters
                    SqlParameter errorCodeParam = new SqlParameter("@ErrorCode", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Output
                    };
                    cmd.Parameters.Add(errorCodeParam);

                    SqlParameter returnMessageParam = new SqlParameter("@ReturnMessage", SqlDbType.NVarChar, 200)
                    {
                        Direction = ParameterDirection.Output
                    };
                    cmd.Parameters.Add(returnMessageParam);

                    con.Open();
                    cmd.ExecuteNonQuery();

                    int errorCode = (int)errorCodeParam.Value;
                    string errorMessage = returnMessageParam.Value.ToString();

                    return (errorCode, errorMessage);
                }
            }
        }


    }
}
