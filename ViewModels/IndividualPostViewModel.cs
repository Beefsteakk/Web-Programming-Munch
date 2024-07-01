using EffectiveWebProg.Models;

namespace EffectiveWebProg.ViewModels
{
    public class IndividualPostViewModel
    {
        required public PostsModel Post { get; set; }
        required public List<CommentsModel> CommentsList { get; set; }
        required public List<string?>? PostImageURLs { get; set; }
    }
}
