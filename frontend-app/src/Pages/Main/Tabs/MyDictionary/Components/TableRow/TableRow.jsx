import styles from "./TableRow.module.css";
import TableCell from "../TableCell/TableCell.jsx";
import { useState } from "react";
import { saveInput } from "../../Services/InputService.js";
import { removeSentence } from "../../../../../../API/MyDictionary/RemoveSentence.js";
export default function TableRow({
  word,
  translation,
  edit,
  words,
  index,
  setWords,
  setAllWords,
  allWords,
}) {
  const [currentWordValue, setCurrentWordValue] = useState(word);
  const [currentTranslationValue, setCurrentTranslationValue] =
    useState(translation);
  return (
    <div className={styles.tableRow}>
      <TableCell
        currentValue={currentWordValue}
        setCurrentValue={setCurrentWordValue}
        allWords={allWords}
        setAllWords={setAllWords}
        index={index}
        setWords={setWords}
        words={words}
        isTranslation={false}
        text={word}
        edit={edit}
      ></TableCell>
      <TableCell
        currentValue={currentTranslationValue}
        setCurrentValue={setCurrentTranslationValue}
        allWords={allWords}
        setAllWords={setAllWords}
        setWords={setWords}
        words={words}
        index={index}
        isTranslation={true}
        text={translation}
        edit={edit}
      ></TableCell>
      {edit && (
        <button
          onClick={() => {
            setCurrentWordValue("");
            setCurrentTranslationValue("");
            saveInput(
              "",
              words,
              allWords,
              index,
              false,
              setWords,
              setAllWords,
              setCurrentWordValue
            );
            saveInput(
              "",
              words,
              allWords,
              index,
              true,
              setWords,
              setAllWords,
              setCurrentTranslationValue
            );
            removeSentence([word, translation]);
          }}
        >
          -
        </button>
      )}
    </div>
  );
}
