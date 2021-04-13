import React, { useState, useContext, useEffect } from "react";
import {
  Form,
  FormGroup,
  Card,
  CardBody,
  Label,
  Input,
  Button,
} from "reactstrap";
import { PostContext } from "../providers/PostProvider";
import { useHistory, useParams } from "react-router-dom";
import { UserContext } from "../providers/UserProvider";

const PostEdit = () => {
  const { updatePost, getPost} = useContext(PostContext);
  const [imageUrl, setImageUrl] = useState("");
  const [title, setTitle] = useState("");
  const [caption, setCaption] = useState("");
  const { currentUserProfileId } = useContext(UserContext)
  const { id } = useParams();
  const [post, setPost] = useState({});

  // Use this hook to allow us to programatically redirect users
  const history = useHistory();

  useEffect(() => {
    getPost(id).then(setPost);
  }, []);

  useEffect(() => {
    setImageUrl(post.imageUrl)
    setTitle(post.title)
    setCaption(post.caption)
  }, [post]);

  const submit = (e) => {
    const newPost = {
      ...post
    };

    newPost.title = title
    newPost.imageUrl = imageUrl
    newPost.caption = caption

    updatePost(newPost).then((p) => {
      // Navigate the user back to the home route
      history.push("/");
    });
  };

  if(post === null || currentUserProfileId !== post.userProfileId)
  {
      return null
  }

  return (
    <div className="container pt-4">
      <div className="row justify-content-center">
        <Card className="col-sm-12 col-lg-6">
          <CardBody>
            <Form>
              <FormGroup>
                <Label for="imageUrl">Gif URL</Label>
                <Input
                  id="imageUrl"
                  onChange={(e) => setImageUrl(e.target.value)}
                  value={imageUrl}
                />
              </FormGroup>
              <FormGroup>
                <Label for="title">Title</Label>
                <Input id="title" onChange={(e) => setTitle(e.target.value)} value={title}/>
              </FormGroup>
              <FormGroup>
                <Label for="caption">Caption</Label>
                <Input
                  id="caption"
                  onChange={(e) => setCaption(e.target.value)}
                  value={caption}
                />
              </FormGroup>
            </Form>
            <Button color="info" onClick={submit}>
              SUBMIT
            </Button>
          </CardBody>
        </Card>
      </div>
    </div>
  );
};

export default PostEdit;