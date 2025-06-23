using AdminDyanamoEnterprises.DTOs.Master;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminDyanamoEnterprises.Repository.IRepository
{
    public interface IAccountRepository
    {
        public bool CheckLogin(Account loginType);
    }
}
