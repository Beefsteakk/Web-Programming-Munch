using System.Collections.Generic;
using EffectiveWebProg.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EffectiveWebProg.ViewModels
{
    public class FoodListViewModel
    {
        public FoodListsModel FoodList { get; set; }
        public List<FoodListEntriesModel> FoodListEntries { get; set; }
        public SelectList Restaurants {get; set;}
    }
}