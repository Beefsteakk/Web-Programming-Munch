using EffectiveWebProg.Models;

namespace EffectiveWebProg.ViewModels
{
    public class MainFeedViewModel
    {
        required public List<PostViewModel> PostLists { get; set; }
        public PostsModel? NewPost { get; set; }
        public CommentsModel? NewComment { get; set; }
    }
}
