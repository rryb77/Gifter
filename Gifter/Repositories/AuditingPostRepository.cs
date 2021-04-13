using Gifter.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gifter.Repositories
{
    public class AuditingPostRepository : IPostRepository
    {
        private readonly IPostRepository _postRepository;
        private readonly AuditRepository _auditRepository;
        public AuditingPostRepository(PostRepository postRepository, AuditRepository auditRepository)
        {
            _postRepository = postRepository;
            _auditRepository = auditRepository;
        }

        public int Add(Post post)
        {
            int postId = _postRepository.Add(post);
            DateTime submitTime = DateTime.Now;
            string newValueString = "Id: {postId}, Title: {post.Title}, ImageUrl: {post.ImageUrl}, Caption: {post.Caption}, UserProfileId: {post.UserProfileId}, DateCreated: {submitTime}";
            _auditRepository.Add("Post", "Insert", null, newValueString);

            return postId;
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public List<Post> GetAll()
        {
            return _postRepository.GetAll();
        }

        public List<Post> GetAllWithComments()
        {
            return _postRepository.GetAllWithComments();
        }

        public Post GetById(int id)
        {
            return _postRepository.GetById(id);
        }

        public Post GetPostByIdWithComments(int id)
        {
            return _postRepository.GetPostByIdWithComments(id);
        }

        public List<Post> Hottest(string criterion, bool sortDescending)
        {
            return _postRepository.Hottest(criterion, sortDescending);
        }

        public List<Post> Search(string criterion, bool sortDescending)
        {
            throw new NotImplementedException();
        }

        public void Update(Post post)
        {
            throw new NotImplementedException();
        }
    }
}
