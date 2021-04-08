using Gifter.Models;
using System.Collections.Generic;

namespace Gifter.Repositories
{
    public interface IUserProfileRepository
    {
        void Add(UserProfile profile);
        void Delete(int id);
        List<UserProfile> GetAll();
        UserProfile GetUserProfileById(int id);
        UserProfile GetUserProfileWithPosts(int id);
        void Update(UserProfile profile);
    }
}