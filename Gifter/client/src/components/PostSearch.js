import React, { useContext, useEffect } from "react";
import { PostContext } from "../providers/PostProvider";

export const PostSearch = () => {

    const { setSearchTerms } = useContext(PostContext)

    return (
        <input type="text"
                className="input--wide"
                id="searchbar"
                onKeyUp={(event) => setSearchTerms(event.target.value)}
                placeholder="Search for a Post... " />
    )
}

export default PostSearch;