using System.Collections.Generic;
using EffectiveWebProg.Models;

namespace EffectiveWebProg.ViewModels
{
    public class FoodListViewModel
    {
        public List<FoodListsModel> FoodLists { get; set; }
        public List<FoodListEntriesModel> FoodListEntries { get; set; }
    }
}