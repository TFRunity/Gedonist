

namespace Gedonist.Core.Models
{
    [Serializable]
    public class ProductCategoryBind
    {
        public int Product_Id { get; set; }
        public int Category_Id { get; set; }

        public virtual Product? Product { get; set; }
        public virtual Category? Category { get; set; }
    }
}
