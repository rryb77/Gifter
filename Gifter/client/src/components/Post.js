import React, { useContext } from "react";
import { Card, CardImg, CardBody } from "reactstrap";
import { PostContext } from "../providers/PostProvider";
import { Link } from "react-router-dom";
import { UserContext } from "../providers/UserProvider";

const Post = ({ post }) => {

  const { currentUserProfileId } = useContext(UserContext)

  if (currentUserProfileId === post.userProfileId)
  {
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
        <p className="text-left px-2"> 
          <Link to={`/posts/edit/${post.id}`}>
            <strong> Edit </strong>
          </Link>
      </p>
        {/* <div>
          {
            post.comments.map(comment => {
              return <div>{comment.message}</div>
            })
          }
        </div> */}
      </CardBody>
    </Card>
    )
  }

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