import React from "react";
import { BrowserRouter as Router } from "react-router-dom";
import "./App.css";
import ApplicationViews from "./components/ApplicationsViews";
import { PostProvider } from "./providers/PostProvider";
import Header from "./components/Header"
import { UserProfileProvider } from "./providers/UserProfileProvider";

function App() {
  return (
    <div className="App">
      <Router>
      <UserProfileProvider>
          <PostProvider>
          
            <Header />
            <ApplicationViews />
          
         </PostProvider> 
        </UserProfileProvider>
      </Router>
    </div>
  );
}

export default App;
