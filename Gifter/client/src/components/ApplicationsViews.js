import React, {useContext} from "react";
import { Switch, Route, Redirect } from "react-router-dom";
import PostList from "./PostList";
import PostForm from "./PostForm";
import PostDetails from "./PostDetails";
import UserPosts from "./UsersPosts";
import PostEdit from "./PostEdit";
import Login from "./Login";
import Register from "./Register";
import {UserProfileContext} from '../providers/UserProfileProvider'

const ApplicationViews = () => {
  const { isLoggedIn } = useContext(UserProfileContext);

  return (
    <Switch>
      <Route path="/" exact>
        {isLoggedIn ? <PostList /> : <Redirect to="/login" />}
      </Route>

      <Route path="/posts/add">
        {isLoggedIn ? <PostForm /> : <Redirect to="/login" />}
      </Route>

      <Route path="/posts/edit/:id">
        {isLoggedIn ? <PostEdit/> : <Redirect to="/login" />}
      </Route>

      <Route path="/posts/:id">
        {isLoggedIn ? <PostDetails/> : <Redirect to="/login" />}
      </Route>

      <Route path="/users/:id">
        {isLoggedIn ? <UserPosts/> : <Redirect to="/login" />}
      </Route>

      <Route path="/login">
          <Login />
        </Route>

        <Route path="/register">
          <Register />
        </Route>
      
    </Switch>
  );
};

export default ApplicationViews;