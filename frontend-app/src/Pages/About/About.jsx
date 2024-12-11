import { useNavigate } from "react-router-dom";
import styles from "./About.module.css";
import NavBar from "../../Components/NavBar/NavBar";
import {
  forumPath,
  mainPath,
  profilePath,
  startPagePath,
  userRelationsPath,
} from "../../Constants/Paths";
export default function ({ navigatorSwitcher, setNavigatorSwitcher }) {
  const navigate = useNavigate();
  const navBarLabels = [];
  const navBarHandlers = [];
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
  function profileHandler() {
    navigate(profilePath);
    setNavigatorSwitcher((prev) => !prev);
  }
  function worksHandler() {
    navigate(userRelationsPath);
    setNavigatorSwitcher((prev) => !prev);
  }
  return (
    <div className="page">
      <NavBar
        buttonLabels={navBarLabels}
        buttonHandlers={navBarHandlers}
      ></NavBar>
      <div className={styles.description}>Hi this is english learning site</div>
    </div>
  );
}
