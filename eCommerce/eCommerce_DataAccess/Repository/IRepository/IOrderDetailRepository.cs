﻿using eCommerce_Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace eCommerce_DataAccess.Repository.IRepository
{
    public interface IOrderDetailRepository
    {
        void Update(OrderDetail obj);
    }
}
