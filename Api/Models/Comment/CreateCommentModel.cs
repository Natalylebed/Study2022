namespace Api.Models.Comment
{
    public class CreateCommentModel
    {
        public Guid? AuthorContentId { get; set; }

        public Guid? PostId { get; set; }

        public List<string>? Comments { get; set; }


    }
}
