using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Gifter.Models;
using Gifter.Utils;

namespace Gifter.Repositories
{
    public class UserProfileRepository : BaseRepository, IUserProfileRepository
    {
        public UserProfileRepository(IConfiguration configuration) : base(configuration) { }

        public List<UserProfile> GetAll()
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "SELECT Id, Name, Email, ImageUrl, Bio, DateCreated FROM UserProfile";

                    var reader = cmd.ExecuteReader();

                    var profiles = new List<UserProfile>();

                    while (reader.Read())
                    {
                        profiles.Add(new UserProfile()
                        {
                            Id = DbUtils.GetInt(reader, "Id"),
                            Name = DbUtils.GetString(reader, "Name"),
                            Email = DbUtils.GetString(reader, "Email"),
                            ImageUrl = DbUtils.GetString(reader, "ImageUrl"),
                            Bio = DbUtils.GetString(reader, "Bio"),
                            DateCreated = DbUtils.GetDateTime(reader, "DateCreated")
                        });
                    }

                    reader.Close();
                    return profiles;
                }
            }
        }

        public UserProfile GetByFirebaseUserId(string firebaseUserId)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        SELECT up.Id, Up.FirebaseUserId, up.Name AS UserProfileName, up.Email
                          FROM UserProfile up
                         WHERE up.FirebaseUserId = @FirebaseUserId";

                    DbUtils.AddParameter(cmd, "@FirebaseUserId", firebaseUserId);

                    UserProfile userProfile = null;

                    var reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        userProfile = new UserProfile()
                        {
                            Id = DbUtils.GetInt(reader, "Id"),
                            FirebaseUserId = DbUtils.GetString(reader, "FirebaseUserId"),
                            Name = DbUtils.GetString(reader, "UserProfileName"),
                            Email = DbUtils.GetString(reader, "Email"),
                        };
                    }
                    reader.Close();

                    return userProfile;
                }
            }
        }
        public UserProfile GetUserProfileById(int id)
        {
            using(var conn = Connection)
            {
                conn.Open();
                using(var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "SELECT Id, Name, Email, ImageUrl, Bio, DateCreated FROM UserProfile WHERE Id = @id";

                    DbUtils.AddParameter(cmd, "@Id", id);

                    var reader = cmd.ExecuteReader();
                    var profile = new UserProfile();

                    if(reader.Read())
                    {
                        profile = new UserProfile()
                        {
                            Id = DbUtils.GetInt(reader, "Id"),
                            Name = DbUtils.GetString(reader, "Name"),
                            Email = DbUtils.GetString(reader, "Email"),
                            ImageUrl = DbUtils.GetString(reader, "ImageUrl"),
                            Bio = DbUtils.GetString(reader, "Bio"),
                            DateCreated = DbUtils.GetDateTime(reader, "DateCreated")
                        };
                    }

                    reader.Close();
                    return profile;
                }
            }
        }

        public UserProfile GetUserProfileWithPosts(int id)
        {
            using(var conn = Connection)
            {
                conn.Open();
                using(var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        SELECT up.Id AS ProfileId, up.Name, up.Email, up.ImageUrl, up.Bio, up.DateCreated,
                               p.Id AS PostId, p.Title, p.ImageUrl AS PostImageUrl, p.Caption, p.UserProfileId AS UserProfileId, p.DateCreated AS PostDateCreated,
                               c.Id AS CommentId, c.Message, c.UserProfileId AS CommentUserProfileId
                        FROM UserProfile up
                        LEFT JOIN Post p ON p.UserProfileId = up.Id
                        LEFT JOIN Comment c ON c.PostId = p.Id
                        WHERE up.Id = @Id
                    ";

                    DbUtils.AddParameter(cmd, "@Id", id);

                    var reader = cmd.ExecuteReader();
                    UserProfile profile = null;

                    while(reader.Read())
                    {

                        if (profile == null)
                        {
                            profile = new UserProfile()
                            {
                                Id = id,
                                Name = DbUtils.GetString(reader, "Name"),
                                Email = DbUtils.GetString(reader, "Email"),
                                ImageUrl = DbUtils.GetString(reader, "ImageUrl"),
                                Bio = DbUtils.GetString(reader, "Bio"),
                                DateCreated = DbUtils.GetDateTime(reader, "DateCreated"),
                                Posts = new List<Post>(),
                            };
                        }
                        

                        if(DbUtils.IsNotDbNull(reader, "PostId"))
                        {

                            if(profile.Posts.Find(p => p.Id == DbUtils.GetInt(reader, "PostId")) == null)
                            {
                                profile.Posts.Add(new Post()
                                {
                                    Id = DbUtils.GetInt(reader, "PostId"),
                                    Title = DbUtils.GetString(reader, "Title"),
                                    Caption = DbUtils.GetString(reader, "Caption"),
                                    DateCreated = DbUtils.GetDateTime(reader, "PostDateCreated"),
                                    ImageUrl = DbUtils.GetString(reader, "PostImageUrl"),
                                    UserProfileId = DbUtils.GetInt(reader, "UserProfileId"),
                                    UserProfile = new UserProfile()
                                    {
                                        Id = id,
                                        Name = DbUtils.GetString(reader, "Name"),
                                        Email = DbUtils.GetString(reader, "Email"),
                                        ImageUrl = DbUtils.GetString(reader, "ImageUrl"),
                                        Bio = DbUtils.GetString(reader, "Bio"),
                                        DateCreated = DbUtils.GetDateTime(reader, "DateCreated"),
                                    },
                                    Comments = new List<Comment>()
                                });
                            }
                            


                            if (DbUtils.IsNotDbNull(reader, "CommentId"))
                            {
                                var commentsForThisPost = profile.Posts.Find(p => p.Id == DbUtils.GetInt(reader, "PostId")).Comments;
                           
                                commentsForThisPost.Add(new Comment()
                                {
                                    Id = DbUtils.GetInt(reader, "CommentId"),
                                    Message = DbUtils.GetString(reader, "Message"),
                                    PostId = id,
                                    UserProfileId = DbUtils.GetInt(reader, "CommentUserProfileId")
                                });
                            }
                        }
                    }

                    reader.Close();
                    return profile;
                }
            }
        }

        public void Add(UserProfile profile)
        {
            using(var conn = Connection)
            {
                conn.Open();
                using(var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        INSERT INTO UserProfile ([Name], Email, ImageUrl, Bio, DateCreated)
                        OUTPUT inserted.Id
                        VALUES (@Name, @Email, @ImageUrl, @Bio, @DateCreated)
                    ";

                    DbUtils.AddParameter(cmd, "@Name", profile.Name);
                    DbUtils.AddParameter(cmd, "@Email", profile.Email);
                    DbUtils.AddParameter(cmd, "@ImageUrl", profile.ImageUrl);
                    DbUtils.AddParameter(cmd, "@Bio", profile.Bio);
                    DbUtils.AddParameter(cmd, "@DateCreated", DateTime.Now);

                    profile.Id = (int)cmd.ExecuteScalar();
                }
            }
        }

        public void Update(UserProfile profile)
        {
            using(var conn = Connection)
            {
                conn.Open();
                using(var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                    UPDATE UserProfile
                    SET Name = @Name,
                        Email = @Email,
                        ImageUrl = @ImageUrl,
                        Bio = @Bio,
                        DateCreated = @DateCreated
                    WHERE Id = @Id
                    ";

                    DbUtils.AddParameter(cmd, "Id", profile.Id);
                    DbUtils.AddParameter(cmd, "@Name", profile.Name);
                    DbUtils.AddParameter(cmd, "@Email", profile.Email);
                    DbUtils.AddParameter(cmd, "@ImageUrl", profile.ImageUrl);
                    DbUtils.AddParameter(cmd, "@Bio", profile.Bio);
                    DbUtils.AddParameter(cmd, "@DateCreated", profile.DateCreated);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Delete(int id)
        {
            using(var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand()) 
                {
                    cmd.CommandText = "DELETE FROM UserProfile WHERE Id = @Id";
                    DbUtils.AddParameter(cmd, "@Id", id);
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
