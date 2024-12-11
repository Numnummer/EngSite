import image from "../../../public/images/start-page-img.jpeg";
import LogIn from "./Components/LogIn/LogIn.jsx";
import styles from "./StartPage.module.css";
import NavBar from "../../Components/NavBar/NavBar.jsx";
import { useNavigate } from "react-router-dom";
import { aboutPath } from "../../Constants/Paths.js";

export default function StartPage() {
  const navigate = useNavigate();
  const labels = ["About"];
  const handlers = [() => navigate(aboutPath)];
  return (
    <div className={"page"}>
      <NavBar buttonLabels={labels} buttonHandlers={handlers} />
      <div className={styles.horizontalContainer}>
        <img src={image} className={styles.image} />
        <div className={"verticalContainer"}>
          <div className={styles.welcome}>Welcome</div>
          <LogIn></LogIn>
        </div>
      </div>
    </div>
  );
}
