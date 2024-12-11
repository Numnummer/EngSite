import styles from "./BarButton.module.css";
export default function ({ text, onClick, fontSize }) {
  return (
    <button
      className={styles.button}
      onClick={onClick}
      style={{ fontSize: fontSize }}
    >
      {text}
    </button>
  );
}
