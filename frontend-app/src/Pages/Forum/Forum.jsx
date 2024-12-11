import SendMessageBar from "./Components/SendMessageBar/SendMessageBar.jsx";
import MessageContainer from "./Components/MessageContainer/MessageContainer.jsx";
import { useNavigate } from "react-router-dom";
import {
  aboutPath,
  forumPath,
  mainPath,
  profilePath,
  startPagePath,
  userRelationsPath,
  worksPath,
} from "../../Constants/Paths.js";
import NavBar from "../../Components/NavBar/NavBar.jsx";
import { useEffect, useState } from "react";
import getForumMessages from "../../API/Forum/GetForumMessages.js";
import getUserData from "../../API/User/GetUserData.js";
import { authKey } from "../../Constants/LocalStorageKeys.js";
import forumStore from "../../Services/Forum/ForumStore.js";
import { observer } from "mobx-react";
import { configureChatConnection } from "../../Services/Forum/ForumService.js";

function Forum({ navigatorSwitcher, setNavigatorSwitcher }) {
  const navigate = useNavigate();
  const navBarLabels = ["About", "Main", "Profile", "Works", "LogOut"];
  const navBarHandlers = [
    aboutHandler,
    mainHandler,
    profileHandler,
    worksHandler,
    logOutHandler,
  ];

  const [userData, setUserData] = useState();
  useEffect(() => {
    const fetchMessages = () => {
      getForumMessages()
        .then((msgs) => {
          forumStore.setMessages(msgs);
          console.log(msgs);
        })
        .catch((error) => {
          console.log(error);
        });
    };
    const fetchUserData = () => {
      getUserData()
        .then((data) => {
          setUserData(data);
        })
        .catch((error) => {
          console.log(error);
        });
    };

    if (!navigatorSwitcher) {
      fetchMessages();
      fetchUserData();
      console.log(forumStore.connection.state);
      if (forumStore.connection.state != "Connected") {
        configureChatConnection();
      }
    }
  }, [navigatorSwitcher]);

  return (
    <div className={"page"}>
      <NavBar
        buttonLabels={navBarLabels}
        buttonHandlers={navBarHandlers}
      ></NavBar>
      <MessageContainer userData={userData}></MessageContainer>
      <SendMessageBar userData={userData}></SendMessageBar>
    </div>
  );

  function aboutHandler() {
    navigate(aboutPath);
    setNavigatorSwitcher((prev) => !prev);
  }
  function profileHandler() {
    navigate(profilePath);
    setNavigatorSwitcher((prev) => !prev);
  }
  function mainHandler() {
    navigate(mainPath);
    setNavigatorSwitcher((prev) => !prev);
  }
  function logOutHandler() {
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

export default Forum;
