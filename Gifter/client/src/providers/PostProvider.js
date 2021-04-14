import React, { useState, useContext } from "react";
import { UserProfileContext } from './UserProfileProvider';

export const PostContext = React.createContext();

export const PostProvider = (props) => {
 
  const apiUrl = "/api/post";
  const [posts, setPosts] = useState([]);
  const [ searchTerms, setSearchTerms ] = useState("");
  const { getToken } = useContext(UserProfileContext);

  const getAllPosts = () => 
    getToken().then((token) =>
        fetch(apiUrl, {
          method: "GET",
          headers: {
            Authorization: `Bearer ${token}`
          }
        }).then(resp => resp.json())
          .then(setPosts));

  const addPost = (post) =>
    getToken().then((token) => 
      fetch(apiUrl, {
      method: "POST",
      headers: {
        Authorization: `Bearer ${token}`,
        "Content-Type": "application/json",
      },
      body: JSON.stringify(post),
    }));

  const updatePost = (post) => {
    return getToken().then((token) =>
      fetch(`${apiUrl}/${post.id}`, {
      method: "PUT",
      headers: {
        Authorization: `Bearer ${token}`,  
        "Content-Type": "application/json"
      },
      body: JSON.stringify(post)
    }))      
  }

  const deletePost = (id) => {
    return getToken().then((token) =>
      fetch(`${apiUrl}/${id}`, {
      method: "DELETE",
      headers: {
        Authorization: `Bearer ${token}`,
      }
    }))
  }

  const searchPosts = (searchTerms) => {
    return getToken().then((token) =>
      fetch(`${apiUrl}/search?q=${searchTerms}`, {
        method: "GET",
        headers: {
          Authorization: `Bearer ${token}`,
        }
      })
      .then((res) => res.json())
      .then(setPosts));
  }

  const getPostsWithComments = () => {
    return getToken().then((token) =>
      fetch(`/api/post/GetWithComments`, {
        method: "GET",
        headers: {
          Authorization: `Bearer ${token}`,
        }
      })
      .then((res) => res.json())
      .then(setPosts))
  }

  const getPost = (id) => {
    return getToken().then((token) =>
    fetch(`/api/post/GetPostWithComments?id=${id}`, {
      method: "GET",
        headers: {
          Authorization: `Bearer ${token}`,
        }
    })
    .then((res) => res.json()));
  };

  const getUserPosts = (id) => {
    return getToken().then((token) =>
    fetch(`/api/UserProfile/GetByIdWithPosts?id=${id}`, {
      method: "GET",
        headers: {
          Authorization: `Bearer ${token}`,
        }
    })
    .then((res) => res.json()));
  }

  return (
    <PostContext.Provider value={{ posts, getAllPosts, addPost, searchPosts, setSearchTerms, searchTerms, getPostsWithComments, getPost, getUserPosts, updatePost, deletePost }}>
      {props.children}
    </PostContext.Provider>
  );
};