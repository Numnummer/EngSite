import { useNavigate } from "react-router-dom";
import NavBar from "../../Components/NavBar/NavBar.jsx";
import {
  aboutPath,
  forumPath,
  mainPath,
  startPagePath,
  userRelationsPath,
  worksPath,
} from "../../Constants/Paths.js";
import styles from "./Profile.module.css";
import LeftSide from "./Components/LeftSide/LeftSide.jsx";
import RightSide from "./Components/RightSide/RightSide.jsx";
import { useEffect, useState } from "react";
import getUserData from "../../API/User/GetUserData.js";
import { wordsLearned } from "../../Constants/LocalStorageKeys.js";
import addLearnedWords from "../../API/Stats/AddLearnedWords.js";
import getStats from "../../API/Stats/GetStats.js";
import forumStore from "../../Services/Forum/ForumStore.js";

export default function Profile({ navigatorSwitcher, setNavigatorSwitcher }) {
  const navigate = useNavigate();
  const navBarLabels = ["About", "Forum", "Main", "Works", "LogOut"];
  const navBarHandlers = [
    aboutHandler,
    forumHandler,
    mainHandler,
    worksHandler,
    logOutHandler,
  ];
  const [userData, setUserData] = useState();
  const [userStats, setUserStats] = useState();
  useEffect(() => {
    const fetchUserData = () => {
      getUserData()
        .then((data) => {
          setUserData(data);
        })
        .catch((error) => {
          console.log(error);
        });
    };
    const fetchStats = () => {
      getStats()
        .then((stat) => {
          setUserStats(stat);
        })
        .catch((error) => {
          console.log(error);
        });
    };
    const learnedWordsCount = localStorage.getItem(wordsLearned);
    if (learnedWordsCount) {
      addLearnedWords(learnedWordsCount).then((result) => {
        localStorage.setItem(wordsLearned, 0);
        fetchUserData();
        fetchStats();
      });
    }
  }, [navigatorSwitcher]);
  return (
    <div className={"page"}>
      <NavBar
        buttonLabels={navBarLabels}
        buttonHandlers={navBarHandlers}
      ></NavBar>
      <div className={styles.horizontalBox}>
        <LeftSide userData={userData}></LeftSide>
        <RightSide userStats={userStats} userData={userData}></RightSide>
      </div>
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
  function logOutHandler() {
    localStorage.removeItem(authKey);
    navigate(startPagePath);
    setNavigatorSwitcher((prev) => !prev);
    forumStore.disconnect();
  }
  function mainHandler() {
    navigate(mainPath);
    setNavigatorSwitcher((prev) => !prev);
  }
  function worksHandler() {
    navigate(userRelationsPath);
    setNavigatorSwitcher((prev) => !prev);
  }
}
