// ViewModels/ItemCreateViewModel.cs
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using EffectiveWebProg.Models;
using Microsoft.AspNetCore.Http;

namespace EffectiveWebProg.ViewModels
{
    public class ItemCreateViewModel
    {
        [Required]
        public Guid CatID { get; set; }

        [Required]
        [StringLength(255)]
        public string ItemName { get; set; }

        public IFormFile? ItemPic { get; set; }

        public List<ItemCatModel> Categories { get; set; }

        public ItemCreateViewModel()
        {
            Categories = new List<ItemCatModel>();
        }
    }
}