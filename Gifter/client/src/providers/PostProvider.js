import React, { useState } from "react";

export const PostContext = React.createContext();

export const PostProvider = (props) => {
  const [posts, setPosts] = useState([]);
  const [ searchTerms, setSearchTerms ] = useState("");

  const getAllPosts = () => {
    return fetch("/api/post")
      .then((res) => res.json())
      .then(setPosts);
  };

  const addPost = (post) => {
    return fetch("/api/post", {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
      },
      body: JSON.stringify(post),
    });
  };

  const updatePost = (post) => {
    return fetch(`/api/post/${post.id}`, {
      method: "PUT",
      headers: {
          "Content-Type": "application/json"
      },
      body: JSON.stringify(post)
    })
      
  }

  const deletePost = (id) => {
    return fetch(`/api/post/${id}`, {
      method: "DELETE"
    })
  }

  const searchPosts = (searchTerms) => {
    return fetch(`/api/post/search?q=${searchTerms}`)
      .then((res) => res.json())
      .then(setPosts);
  }

  const getPostsWithComments = () => {
    return fetch(`/api/post/GetWithComments`)
      .then((res) => res.json())
      .then(setPosts)
  }

  const getPost = (id) => {
    return fetch(`/api/post/GetPostWithComments?id=${id}`).then((res) => res.json());
  };

  const getUserPosts = (id) => {
    return fetch(`/api/UserProfile/GetByIdWithPosts?id=${id}`).then((res) => res.json())
  }

  return (
    <PostContext.Provider value={{ posts, getAllPosts, addPost, searchPosts, setSearchTerms, searchTerms, getPostsWithComments, getPost, getUserPosts, updatePost, deletePost }}>
      {props.children}
    </PostContext.Provider>
  );
};