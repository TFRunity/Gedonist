using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gedonist.Core.BindingModels
{
    public class BindingProductCategory
    {
        [Required]
        public string Category_Name { get; set; } = string.Empty;
        [Required]
        public int Product_Id { get; set; }
    }
}
