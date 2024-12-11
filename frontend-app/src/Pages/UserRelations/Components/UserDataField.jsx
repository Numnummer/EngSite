import { useEffect } from "react";
import styles from "./UserDataField.module.css";

export default function ({ userName, handleClick }) {
  return (
    <div
      className={styles.workItem}
      onClick={() => {
        handleClick(userName);
      }}
    >
      <label className={styles.name}>{userName}</label>
    </div>
  );
}
