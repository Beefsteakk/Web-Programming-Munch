using EffectiveWebProg.Models;

namespace EffectiveWebProg.ViewModels
{
    public class MainFeedViewModel
    {
        required public List<PostsModel> PostLists { get; set; }
        public PostsModel NewPost { get; set; }
    }
}
