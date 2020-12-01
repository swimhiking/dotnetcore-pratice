using System;
using System.Collections.Generic;
using System.Text;

namespace eCommerce_Model.ViewModels
{
    public class OrderVM
    {
        public OrderHeader OrderHeader { get; set; }
        public IEnumerable<OrderDetail> OrderDetail { get; set; }
    }
}
