import { saveInput } from "../../Services/InputService";
import styles from "./TableCell.module.css";
import { useEffect, useState } from "react";
export default function TableCell({
  currentValue,
  setCurrentValue,
  text,
  edit,
  isTranslation,
  words,
  index,
  setWords,
  setAllWords,
  allWords,
}) {
  useEffect(() => {
    setCurrentValue(text);
  }, [edit]);

  return (
    <div className={styles.cell}>
      {edit ? (
        <input
          className={styles.innerText}
          value={currentValue}
          onChange={(event) =>
            saveInput(
              event.target.value,
              words,
              allWords,
              index,
              isTranslation,
              setWords,
              setAllWords,
              setCurrentValue
            )
          }
        />
      ) : (
        <label className={styles.innerText}>{text}</label>
      )}
    </div>
  );
}
