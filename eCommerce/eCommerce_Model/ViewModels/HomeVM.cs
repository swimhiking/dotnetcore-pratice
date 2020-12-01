using System;
using System.Collections.Generic;
using System.Text;

namespace eCommerce_Model.ViewModels
{
    public class HomeVM
    {
        public IEnumerable<Product> Products { get; set; }
        public IEnumerable<Category> Categories { get; set; }
    }
}
