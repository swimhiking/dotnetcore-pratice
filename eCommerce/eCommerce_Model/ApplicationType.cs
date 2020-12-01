using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace eCommerce_Model
{
    public class ApplicationType
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }

    }
}
