using eCommerce_Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace eCommerce_DataAccess.Repository.IRepository
{
    public interface IApplicationTypeRepository :IRepository<ApplicationType>
    {
        void Update(ApplicationType obj);
    }
}
