import React, { useState } from "react";

export const UserContext = React.createContext();

export const UserProvider = (props) => {

    const [users, setUsers] = useState([]);
    const [currentUserProfileId, setCurrentUserProfileId] = useState(1);

    const getAllUsers = () => {
        return fetch("/api/UserProfile")
        .then((res) => res.json())
        .then(setUsers);
    };

    return (
        <UserContext.Provider value={{ users, getAllUsers, currentUserProfileId, setCurrentUserProfileId }}>
          {props.children}
        </UserContext.Provider>
    );
}