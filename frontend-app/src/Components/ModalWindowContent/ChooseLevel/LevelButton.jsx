import styles from "./ChooseLevel.module.css";
export default function ({ text, onClick }) {
  return (
    <button onClick={() => onClick(text)} className={styles.button}>
      {text}
    </button>
  );
}
