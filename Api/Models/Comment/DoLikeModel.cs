namespace Api.Models.Comment
{
    public class DoLikeModel
    {
        public Guid AutorLikeId { get; set; }
        public Guid? PostCommentId { get; set; }
        public bool Like { get; set; }
    }
}
