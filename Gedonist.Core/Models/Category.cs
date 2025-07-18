
namespace Gedonist.Core.Models
{
    [Serializable]
    public class Category
    {
        public int Category_Id { get; set; }
        public string Name { get; set; } = "NoName";

        public virtual IEnumerable<ProductCategoryBind>? ProductCategoryBinds { get; set; }
    }
}
