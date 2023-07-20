namespace ForumSystem.Web.ViewModels.Comments
{
    public class CreateCommentInputModel
    {
        public int PostId { get; set; }

        public int ParentCommentId { get; set; }

        public string Content { get; set; }
    }
}
