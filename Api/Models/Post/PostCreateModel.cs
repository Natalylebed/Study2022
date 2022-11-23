using Api.Models.Attach;
using Api.Models.Comment;
using DAL.Entities;

namespace Api.Models.Post
{
    public class CreatePostModel
    {
        public Guid Id { get; set; }
        public string? Description { get; set; }
        public Guid AuthorId { get; set; }
        public List<MetadataLinkModel> Contents { get; set; } = new List<MetadataLinkModel>();
        public List<string>? Comments { get; set; } 


    }
    public class CreatePostRequest
    {
        public Guid? AuthorId { get; set; }
        public string? Description { get; set; }
        public List<MetadataModel> Contents { get; set; } = new List<MetadataModel>();
        public List<string>? Comments { get; set; }


    }
}
