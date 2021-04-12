import React, { useContext } from "react";
import { Card, CardImg, CardBody } from "reactstrap";
import { PostContext } from "../providers/PostProvider";
import { Link } from "react-router-dom";

const Post = ({ post }) => {

 return (
    <Card className="m-4">
      <p className="text-left px-2">Posted by: 
          <Link to={`/users/${post.userProfileId}`}>
            <strong> {post.userProfile.name}</strong>
          </Link>
      </p>
      <CardImg top src={post.imageUrl} alt={post.title} />
      <CardBody>
        <p>
          <Link to={`/posts/${post.id}`}>
            <strong>{post.title}</strong>
          </Link>
        </p>
        <p>{post.caption}</p>
        {/* <div>
          {
            post.comments.map(comment => {
              return <div>{comment.message}</div>
            })
          }
        </div> */}
      </CardBody>
    </Card>
  );

};

export default Post;