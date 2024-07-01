using EffectiveWebProg.Models;

namespace EffectiveWebProg.ViewModels
{
    public class PostViewModel(PostsModel post, bool isLikedByUser, bool isOwnPost, List<string?>? imageUrls)
    {
        public PostsModel Post { get; set; } = post;
        public bool IsLikedByUser { get; set; } = isLikedByUser;
        public bool IsOwnPost { get; set; } = isOwnPost;
        public List<string?>? ImageUrls { get; set; } = imageUrls;
    }
}
