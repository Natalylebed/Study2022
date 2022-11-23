using Api.Exceptions;
using Api.Models.Post;
using Api.Models.User;
using Api.Models.Comment;
using AutoMapper;
using DAL;
using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System.IO;

namespace Api.Services
{
    public class CommentService
    {
        private readonly IMapper _mapper;
        private readonly DataContext _context;
       

        public CommentService(IMapper mapper, UserService userService, DataContext context)
        {
           
            _mapper = mapper;
            _context = context;
        }

        //var dbUser = _mapper.Map<DAL.Entities.User>(model);
        //var t = await _context.Users.AddAsync(dbUser);
        //await _context.SaveChangesAsync();
        //    return t.Entity.Id;

    //    //var user = await _context.Users.Include(x => x.Avatar).FirstOrDefaultAsync(x => x.Id == userId);
    //        if (user != null)
    //        {
    //            var avatar = new Avatar
    //            {
    //                Author = user,
    //                MimeType = meta.MimeType,
    //                FilePath = filePath,
    //                Name = meta.Name,
    //                Size = meta.Size
    //            };
    //    user.Avatar = avatar;

    //            await _context.SaveChangesAsync();
    //}//
    public async Task AddCommentToPost(Guid userId, string filePath, CreateCommentModel model)
        {

            var comment = await _context.Posts.Include(x=>x.PostContents).FirstOrDefaultAsync(x => x.Id == model.PostId);
            if (comment != null)
            {
                var dbUser = await GetUserById(userId);
                var postcomment = new PostContent
                {
                    Post=comment,
                   FilePath= filePath,
                   Id=Guid.NewGuid(),               
                    MimeType ="string",
                    Author= dbUser,
                    AuthorId = userId,
                    Size = model.Comments.LastOrDefault().Length,
                    Name = model.Comments.LastOrDefault().ToString(),
                    Comment = model.Comments.LastOrDefault(),
                    Created = DateTime.UtcNow
                };
                await _context.PostContents.AddAsync(postcomment);
                
                // comment.PostContents.Add(postcomment);

                await _context.SaveChangesAsync();
            }
            
        }
        private async Task<User> GetUserById(Guid id)
        {
            var user = await _context.Users.Include(x => x.Avatar).Include(x => x.Posts).FirstOrDefaultAsync(x => x.Id == id);
            if (user == null || user == default)
                throw new UserNotFoundException();
            return user;
        }

    }
}
