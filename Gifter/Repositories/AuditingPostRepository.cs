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
            string oldValueString = null;
            string newValueString = $"Id: {postId}, Title: {post.Title}, ImageUrl: {post.ImageUrl}, Caption: {post.Caption}, UserProfileId: {post.UserProfileId}, DateCreated: {submitTime}";
            _auditRepository.Add("Post", "Insert", oldValueString, newValueString);

            return postId;
        }

        public void Delete(int id)
        {
            Post post = _postRepository.GetById(id);
            string oldValueString = $"Id: {post.Id}, Title: {post.Title}, ImageUrl: {post.ImageUrl}, Caption: {post.Caption}, UserProfileId: {post.UserProfileId}, DateCreated: {post.DateCreated}";
            string newValueString = null;
            _auditRepository.Add("Post", "Delete", oldValueString, newValueString);
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
            return _postRepository.Search(criterion, sortDescending);
        }

        public void Update(Post post)
        {
            Post oldPost = _postRepository.GetById(post.Id);
            string oldValueString = $"Id: {oldPost.Id}, Title: {oldPost.Title}, ImageUrl: {oldPost.ImageUrl}, Caption: {oldPost.Caption}, UserProfileId: {oldPost.UserProfileId}, DateCreated: {oldPost.DateCreated}";
            _postRepository.Update(post);

            DateTime submitTime = DateTime.Now;
            string newValueString = $"Id: {post.Id}, Title: {post.Title}, ImageUrl: {post.ImageUrl}, Caption: {post.Caption}, UserProfileId: {post.UserProfileId}, DateCreated: {submitTime}";
            _auditRepository.Add("Post", "Update", oldValueString, newValueString);
        }
    }
}
