using EffectiveWebProg.Models;

namespace EffectiveWebProg.ViewModels
{
    public class MainFeedViewModel(List<PostViewModel> PostLists)
    {
        public List<PostViewModel> PostLists { get; set; } = PostLists;
        public PostsModel? NewPost { get; set; }
        public CommentsModel? NewComment { get; set; }
    }
}
