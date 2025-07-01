using AdminDyanamoEnterprises.DTOs;
using AdminDyanamoEnterprises.DTOs.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminDyanamoEnterprises.Repository
{
    public interface IAccountRepository
    {
        public bool CheckLogin(LoginType loginType);
        public (int ErrorCode, string ErrorMessage) RegisterUser(RegisterType registerType);
    }
}
