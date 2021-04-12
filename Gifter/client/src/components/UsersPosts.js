import React, { useEffect, useContext, useState } from "react";
import { useParams } from "react-router-dom";
import { PostContext } from "../providers/PostProvider";
import Post from "./Post";

const UserPosts = () => {
    const [userPosts, setUserPosts] = useState();
    const { getUserPosts } = useContext(PostContext);
    const { id } = useParams();

    useEffect(() => {
        getUserPosts(id).then(setUserPosts);
      }, []);
    
    return (
        <div className="container">
          <div className="row justify-content-center">
            <div className="col-sm-12 col-lg-6">
              {
                  userPosts ? 
                  userPosts.posts.map(post => {
                      return <Post key={post.id} post={post} />
                  })
                  : null
              }
            </div>
          </div>
        </div>
      );
}

export default UserPosts;