import NavBar from "../../Components/NavBar/NavBar.jsx";
import MainTab from "./Tabs/Main/MainTab.jsx";
import ActualText from "./Tabs/ActualText/ActualText.jsx";
import DailyWord from "./Tabs/DailyWord/DailyWord.jsx";
import MyDictionary from "./Tabs/MyDictionary/MyDictionary.jsx";
import Texts from "./Tabs/Texts/Texts.jsx";
import Understanding from "./Tabs/Understanding/Understanding.jsx";
import Words from "./Tabs/Words/Words.jsx";
import WordsPractice from "./Tabs/WordsPractice/WordsPractice.jsx";
import { useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";
import {
  aboutPath,
  forumPath,
  profilePath,
  startPagePath,
  userRelationsPath,
  worksPath,
} from "../../Constants/Paths.js";
import TabBar from "../../Components/TabBar/TabBar.jsx";
import { authKey } from "../../Constants/LocalStorageKeys.js";
import forumStore from "../../Services/Forum/ForumStore.js";

export default function Main({ setNavigatorSwitcher }) {
  const navigate = useNavigate();
  const [location, setLocation] = useState(
    localStorage.getItem("location") || "Main"
  );
  useEffect(() => {
    localStorage.setItem("location", location);
  }, [location]);
  const navBarLabels = ["About", "Forum", "Profile", "Works", "Tabs", "Logout"];
  const navBarHandlers = [
    aboutHandler,
    forumHandler,
    profileHandler,
    worksHandler,
    tabBarHandler,
    logoutHandler,
  ];

  let [showTabBar, setShowTabBar] = useState(false);

  let componentToShow;
  switch (location) {
    case "Main":
      componentToShow = <MainTab setLocation={setLocation}></MainTab>;
      break;
    case "ActualText":
      componentToShow = <ActualText setLocation={setLocation}></ActualText>;
      break;
    case "DailyWord":
      componentToShow = <DailyWord></DailyWord>;
      break;
    case "MyDictionary":
      componentToShow = <MyDictionary></MyDictionary>;
      break;
    case "Texts":
      componentToShow = <Texts setLocation={setLocation}></Texts>;
      break;
    case "Understanding":
      componentToShow = (
        <Understanding setLocation={setLocation}></Understanding>
      );
      break;
    case "Words":
      componentToShow = <Words setLocation={setLocation}></Words>;
      break;
    case "WordsPractice":
      componentToShow = <WordsPractice></WordsPractice>;
      break;
  }

  return (
    <div className={"page"}>
      <NavBar
        buttonLabels={navBarLabels}
        buttonHandlers={navBarHandlers}
      ></NavBar>
      {showTabBar ? <TabBar setCurrentTab={setLocation}></TabBar> : null}
      {componentToShow}
    </div>
  );

  function aboutHandler() {
    navigate(aboutPath);
    setNavigatorSwitcher((prev) => !prev);
  }
  function forumHandler() {
    navigate(forumPath);
    setNavigatorSwitcher((prev) => !prev);
  }
  function profileHandler() {
    navigate(profilePath);
    setNavigatorSwitcher((prev) => !prev);
  }
  function tabBarHandler() {
    setShowTabBar(!showTabBar);
  }
  function logoutHandler() {
    localStorage.removeItem(authKey);
    navigate(startPagePath);
    setNavigatorSwitcher((prev) => !prev);
    forumStore.disconnect();
  }
  function worksHandler() {
    navigate(userRelationsPath);
    setNavigatorSwitcher((prev) => !prev);
  }
}
