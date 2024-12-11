import React from "react";
import ReactDOM from "react-dom/client";
import App from "./App.jsx";
import "./styles/index.css";
import { BrowserRouter } from "react-router-dom";
import { Provider } from "mobx-react";
import forumStore from "./Services/Forum/ForumStore.js";

ReactDOM.createRoot(document.getElementById("root")).render(
  <Provider myStore={forumStore}>
    <BrowserRouter>
      <App />
    </BrowserRouter>
  </Provider>
);
