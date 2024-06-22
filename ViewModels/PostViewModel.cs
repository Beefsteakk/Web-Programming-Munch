using EffectiveWebProg.Models;

namespace EffectiveWebProg.ViewModels
{
    public class PostViewModel
    {
        required public PostsModel Post { get; set; }
        required public bool IsLikedByUser { get; set; }
        required public bool IsOwnPost { get; set; }
    }
}
