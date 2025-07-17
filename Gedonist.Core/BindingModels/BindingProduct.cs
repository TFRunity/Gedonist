using System.ComponentModel.DataAnnotations;

namespace Gedonist.Core.BindingModels
{
    public class BindingProduct
    {
        public string Name { get; set; } = "NoName";
        public string Description { get; set; } = "NoDesc";
        public decimal Price { get; set; } = 0;
    }
}
