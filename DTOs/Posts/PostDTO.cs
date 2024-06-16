namespace EffectiveWebProg.DTOs.Posts
{
    public class PostDTO
    {
        public Guid PostID { get; }
        public Guid? RestID { get; }
        public Guid? UserID { get; }
        public string PostTitle { get; }
        public string PostContent { get; }

        public PostDTO(Guid PostID, Guid UserID, string PostTitle, string PostContent)
        {
            this.PostTitle = PostTitle;
            this.PostID = PostID;
            this.UserID = UserID;
            this.PostContent = PostContent;
        }
    }

    public class CommentDTO
    {
        public Guid CommentID { get; }
        public Guid ParentPostID { get; }
        public CommentAuthorDTO CommentAuthor { get; }
        public string CommentContent { get; }

        public CommentDTO(Guid CommentID, Guid ParentPostID, CommentAuthorDTO CommentAuthor, string CommentContent)
        {
            this.CommentID = CommentID;
            this.ParentPostID = ParentPostID;
            this.CommentAuthor = CommentAuthor;
            this.CommentContent = CommentContent;
        }
    }

    public class CommentAuthorDTO
    {
        public Guid UserID { get; }
        public string Username { get; }
        public string? ProfilePicURL { get; }

        public CommentAuthorDTO (Guid UserID, string Username) {
            this.UserID = UserID;
            this.Username = Username;
        }
    }
}
