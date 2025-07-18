
using System.ComponentModel.DataAnnotations.Schema;

namespace Gedonist.Core.Models
{
    [Serializable]
    public class Product
    {
        public int Product_Id { get; set; }
        public string Name { get; set; } = "NoName";
        public string Description { get; set; } = "NoDesc";
        public decimal Price { get; set; } = 0;
        
        public int? ProductCategoryBind_Id { get; set; }
        public virtual ProductCategoryBind? ProductCategoryBind { get; set; }

    }
}
