import React, { useContext } from "react";
import { Card, CardImg, CardBody } from "reactstrap";
import { PostContext } from "../providers/PostProvider";

const Post = ({ post }) => {

 return (
    <Card className="m-4">
      <p className="text-left px-2">Posted by: {post.userProfile.name}</p>
      <CardImg top src={post.imageUrl} alt={post.title} />
      <CardBody>
        <p>
          <strong>{post.title}</strong>
        </p>
        <p>{post.caption}</p>
        <div>
          {
            post.comments.map(comment => {
              return <div>{comment.message}</div>
            })
          }
        </div>
      </CardBody>
    </Card>
  );

};

export default Post;