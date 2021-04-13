import React, {useContext} from "react";
import { Link } from "react-router-dom";
import { UserContext } from "../providers/UserProvider";

const Header = () => {
  
  const { setCurrentUserProfileId } = useContext(UserContext)

  var handlerChange = (e) => {
      setCurrentUserProfileId(parseInt(e.target.value))
  }

  return (
    <nav className="navbar navbar-expand navbar-dark bg-info">
      <Link to="/" className="navbar-brand">
        GiFTER
      </Link>
      <ul className="navbar-nav mr-auto">
        <li className="nav-item">
          <Link to="/" className="nav-link">
            Feed
          </Link>
        </li>
        <li className="nav-item">
          <Link to="/posts/add" className="nav-link">
            New Post
          </Link>
        </li>
      </ul>

      <select name="users" id="users" onChange={handlerChange}>
        <option value="1">Oliver Hardy</option>
        <option value="2">Stan Laurel</option>
      </select>

    </nav>
  );
};

export default Header;