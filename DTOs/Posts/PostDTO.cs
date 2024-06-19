namespace EffectiveWebProg.DTOs.Posts
{
    public class PostDTO(Guid PostID, PostAuthorDTO PostAuthor, string PostContent, string PostCreatedAt)
    {
        public Guid PostID { get; } = PostID;
        public PostAuthorDTO PostAuthor { get; } = PostAuthor;
        public string PostContent { get; } = PostContent;
        public string PostCreatedAt { get; } = PostCreatedAt;
    }

    public class PostAuthorDTO(Guid UserID, string Username)
    {
        public Guid UserID { get; } = UserID;
        public string Username { get; } = Username;
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

    public class CommentAuthorDTO(Guid UserID, string Username)
    {
        public Guid UserID { get; } = UserID;
        public string Username { get; } = Username;
        public string? ProfilePicURL { get; }
    }
}
