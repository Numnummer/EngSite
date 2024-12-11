import BarButton from "../ButtonOnBars/BarButton";
import styles from "./NavBar.module.css";

export default function NavBar({ buttonLabels, buttonHandlers }) {
  return (
    <nav className={styles.navBar}>
      {buttonLabels.map((label, index) => (
        /*<button onClick={buttonHandlers[index]} key={index} className={styles.navButton}>
                    {label}
                </button>*/
        <div className={styles.navButton}>
          <BarButton
            text={label}
            onClick={buttonHandlers[index]}
            key={index}
            fontSize={"1.7vw"}
          ></BarButton>
        </div>
      ))}
    </nav>
  );
}
