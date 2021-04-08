using Gifter.Models;
using System.Collections.Generic;

namespace Gifter.Repositories
{
    public interface IUserProfileRepository
    {
        void Add(UserProfile profile);
        List<UserProfile> GetAll();
        UserProfile GetUserProfileById(int id);
    }
}