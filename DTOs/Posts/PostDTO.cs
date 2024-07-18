namespace EffectiveWebProg.DTOs.Posts
{
    public class PostDTO(Guid PostID, string PostContent, string PostCreatedAt)
    {
        public Guid PostID { get; } = PostID;
        public PostUserAuthorDTO? PostAuthorUser { get; set; }
        public PostRestaurantAuthorDTO? PostAuthorRestaurant { get; set; }
        public string PostContent { get; } = PostContent;
        public string PostCreatedAt { get; } = PostCreatedAt;

        public List<string?>? PostPictureURLs { get; set; }
    }

    public class PostUserAuthorDTO(Guid UserID, string Username)
    {
        public Guid UserID { get; } = UserID;
        public string Username { get; } = Username;
    }

    public class PostRestaurantAuthorDTO(Guid RestID, string RestName, string? RestPic)
    {
        public Guid RestID { get; } = RestID;
        public string RestName { get; } = RestName;
        public string? RestPic { get; } = RestPic;
    }

    public class CommentDTO(Guid CommentID, Guid ParentPostID, string CommentContent, bool IsOwnComment)
    {
        public Guid CommentID { get; } = CommentID;
        public Guid ParentPostID { get; } = ParentPostID;
        public CommentUserAuthorDTO? CommentAuthorUser { get; set; }
        public CommentRestaurantAuthorDTO? CommentAuthorRestaurant { get; set; }
        public string CommentContent { get; } = CommentContent;
        public bool IsOwnComment { get; } = IsOwnComment;
    }

    public class CommentUserAuthorDTO(Guid UserID, string Username)
    {
        public Guid UserID { get; } = UserID;
        public string Username { get; } = Username;
        public string? ProfilePicURL { get; }
    }

    public class CommentRestaurantAuthorDTO(Guid RestID, string RestName)
    {
        public Guid RestID { get; } = RestID;
        public string RestName { get; } = RestName;
        public string? ProfilePicURL { get; }
    }
}
