import styles from "./ChooseLevel.module.css";
import LevelButton from "./LevelButton";
export default function ({ closeModal, buttonsOnClick }) {
  return (
    <div className={styles.verticalBox}>
      <button className={styles.closeButton} onClick={closeModal}>
        X
      </button>
      <label className={styles.label}>Choose level</label>
      <div className={styles.horizontalBox}>
        <LevelButton onClick={buttonsOnClick} text={"A1"}></LevelButton>
        <LevelButton onClick={buttonsOnClick} text={"A2"}></LevelButton>
        <LevelButton onClick={buttonsOnClick} text={"B1"}></LevelButton>
        <LevelButton onClick={buttonsOnClick} text={"B2"}></LevelButton>
        <LevelButton onClick={buttonsOnClick} text={"C1"}></LevelButton>
      </div>
    </div>
  );
}
