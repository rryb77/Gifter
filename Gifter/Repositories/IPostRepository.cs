using Gifter.Models;
using System;
using System.Collections.Generic;

namespace Gifter.Repositories
{
    public interface IPostRepository
    {
        int Add(Post post);
        void Delete(int id);
        List<Post> GetAll();
        List<Post> GetAllWithComments();
        Post GetById(int id);
        Post GetPostByIdWithComments(int id);
        List<Post> Hottest(string criterion, bool sortDescending);
        List<Post> Search(string criterion, bool sortDescending);
        void Update(Post post);
    }
}