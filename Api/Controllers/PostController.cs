using Api.Consts;
using Api.Models.Attach;
using Api.Models.Comment;
using Api.Models.Post;
using Api.Models.User;
using Api.Services;
using Common.Extentions;
using DAL.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IO;

namespace Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "Api")]
    public class PostController : ControllerBase
    {
        private readonly PostService _postService;
        private readonly CommentService _commentService;
        public PostController(PostService postService, CommentService commentService, LinkGeneratorService links)
        {
            _commentService = commentService;
            _postService = postService;
            links.LinkContentGenerator = x => Url.ControllerAction<AttachController>(nameof(AttachController.GetPostContent), new
            {
                postContentId = x.Id,
            });
            links.LinkAvatarGenerator = x => Url.ControllerAction<AttachController>(nameof(AttachController.GetUserAvatar), new
            {
                userId = x.Id,
            });

        }

        [HttpGet]
        public async Task<List<PostModel>> GetPosts(int skip = 0, int take = 10)
            => await _postService.GetPosts(skip, take);
        [HttpGet]
        public async Task<PostModel> GetPostById(Guid id)
            => await _postService.GetPostById(id);

        [HttpPost]
        public async Task CreatePost(CreatePostRequest request)
        {
            if (!request.AuthorId.HasValue)
            {
                var userId = User.GetClaimValue<Guid>(ClaimNames.Id);
                if (userId == default)
                    throw new Exception("not authorize");
                request.AuthorId = userId;
            }
            await _postService.CreatePost(request);

        }
        [HttpPost]
        public async Task AddCommentToPost(CreateCommentModel model)
        {
            var userId = User.GetClaimValue<Guid>(ClaimNames.Id);
            if (userId != default)
            {
                var path = Path.Combine(Directory.GetCurrentDirectory(), "attaches", model.AuthorContentId.ToString());
                if (!model.AuthorContentId.HasValue)
                {

                    if (userId == default)
                        throw new Exception("not authorize");
                    model.AuthorContentId = userId;
                }
                await _commentService.AddCommentToPost(userId, path, model);
            }
        }
        [HttpPost]
        public async Task AddlikeToPostContent(DoLikeModel model)
        {
            var userId = User.GetClaimValue<Guid>(ClaimNames.Id);
            if (userId != default)
            {
                model.AutorLikeId = userId;
                await _commentService.AddlikeToPost(model);

            }
            else
                throw new Exception("not authorize");
        }

        }

    }

