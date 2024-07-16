using EffectiveWebProg.Models;
using System.Collections.Generic;

namespace EffectiveWebProg.ViewModels
{
    public class InventoryViewModel
    {
        public IEnumerable<InventoryItemsModel> InventoryItems { get; set; }
        public IEnumerable<ItemCatModel> ItemCat { get; set; }
    }
}
