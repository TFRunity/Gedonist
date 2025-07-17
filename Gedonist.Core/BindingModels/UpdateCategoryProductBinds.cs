using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gedonist.Core.BindingModels
{
    public class UpdateCategoryProductBinds
    {
        public int[] product_Ids { get; set; } = [];
        public string categoryName { get; set; } = string.Empty;
    }
}
