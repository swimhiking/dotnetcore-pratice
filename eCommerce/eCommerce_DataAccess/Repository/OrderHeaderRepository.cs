using eCommerce_DataAccess.Data;
using eCommerce_Model;
using eCommerce_DataAccess.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace eCommerce_DataAccess.Repository
{
    public class OrderHeaderRepository : Repository<OrderHeader>, IOrderHeaderRepository
    {
        private readonly ApplicationDbContext _db;
        public OrderHeaderRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(OrderHeader obj)
        {
            _db.OrderHeader.Update(obj);
        }
    }
}
