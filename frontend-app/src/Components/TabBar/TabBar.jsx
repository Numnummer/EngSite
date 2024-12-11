import { tabBarLabels } from "../../Constants/TabBarArray";
import BarButton from "../ButtonOnBars/BarButton";
import styles from "./TabBar.module.css";
export default function TabBar({ setCurrentTab }) {
  return (
    <div className={styles.tabBar}>
      {tabBarLabels.map((item, index) => (
        <div className={styles.tabButton} key={index}>
          <BarButton
            fontSize={"1.2vw"}
            onClick={() => setCurrentTab(item)}
            text={item}
            key={index}
          ></BarButton>
        </div>
      ))}
    </div>
  );
}
