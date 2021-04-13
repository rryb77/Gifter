import React from "react";
import { BrowserRouter as Router } from "react-router-dom";
import "./App.css";
import ApplicationViews from "./components/ApplicationsViews";
import { PostProvider } from "./providers/PostProvider";
import Header from "./components/Header"
import { UserProvider } from "./providers/UserProvider";

function App() {
  return (
    <div className="App">
      <Router>
        <PostProvider>
          <UserProvider>
            <Header />
            <ApplicationViews />
          </UserProvider>
        </PostProvider>
      </Router>
    </div>
  );
}

export default App;
