using EffectiveWebProg.Models;

namespace EffectiveWebProg.ViewModels
{
    public class ShopViewModel
    {
        public IEnumerable<ItemsModel> Item { get; set; }
        public IEnumerable<ItemCatModel> ItemCat { get; set; }
    }
}