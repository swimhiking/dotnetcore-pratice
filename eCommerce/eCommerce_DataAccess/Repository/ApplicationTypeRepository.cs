using eCommerce_DataAccess.Data;
using eCommerce_DataAccess.Repository.IRepository;
using eCommerce_Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace eCommerce_DataAccess.Repository
{
    public class ApplicationTypeRepository : Repository<ApplicationType>, IApplicationTypeRepository
    {
        private readonly ApplicationDbContext _db;
        public ApplicationTypeRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(ApplicationType obj)
        {
            var objFromDb = base.FirstOrDefault(u => u.Id == obj.Id);
            if(objFromDb != null)
            {
                objFromDb.Name = obj.Name;
            }
        }
    }
}
