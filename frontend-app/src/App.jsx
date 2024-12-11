import "./styles/App.css";
import { Route, Routes } from "react-router-dom";
import {
  aboutPath,
  forumPath,
  mainPath,
  pdfViwerPath,
  profilePath,
  registrationPath,
  startPagePath,
  userRelationsPath,
  worksPath,
} from "./Constants/Paths.js";
import StartPage from "./Pages/StartPage/StartPage.jsx";
import React, { useState } from "react";
import Registration from "./Pages/Registration/Registration.jsx";
import Main from "./Pages/Main/Main.jsx";
import Profile from "./Pages/Profile/Profile.jsx";
import Forum from "./Pages/Forum/Forum.jsx";
import Works from "./Pages/Works/Works.jsx";
import PDFViewerPage from "./Pages/PdfViewer/PdfViewer.jsx";
import UserRelations from "./Pages/UserRelations/UserRelations.jsx";
import About from "./Pages/About/About.jsx";

function App() {
  const [navigatorSwitcher, setNavigatorSwitcher] = useState(false);
  return (
    <>
      <Routes>
        <Route path={startPagePath} element={StartPage()}></Route>
        <Route path={registrationPath} element={Registration()}></Route>
        <Route path={mainPath} element={Main({ setNavigatorSwitcher })}></Route>
        <Route
          path={profilePath}
          element={Profile({ navigatorSwitcher, setNavigatorSwitcher })}
        ></Route>
        <Route
          path={forumPath}
          element={Forum({ navigatorSwitcher, setNavigatorSwitcher })}
        ></Route>
        <Route
          path={userRelationsPath}
          element={UserRelations({ navigatorSwitcher, setNavigatorSwitcher })}
        ></Route>
        <Route
          path={worksPath}
          element={Works({ navigatorSwitcher, setNavigatorSwitcher })}
        ></Route>
        <Route
          path={aboutPath}
          element={About({ navigatorSwitcher, setNavigatorSwitcher })}
        ></Route>
        <Route path={pdfViwerPath} element={PDFViewerPage()}></Route>
      </Routes>
    </>
  );
}

export default App;
