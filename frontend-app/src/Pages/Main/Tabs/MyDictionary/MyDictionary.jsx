import Search from "../../../../Components/Search/Search.jsx";
import styles from "./MyDictionary.module.css";
import TableRow from "./Components/TableRow/TableRow.jsx";
import CommonButton from "../../../../Components/CommonButton/CommonButton.jsx";
import { useEffect, useRef, useState } from "react";
import selectWordsByBeginning, {
  mapWordsTranslation,
  removeEmptyItems,
  translateUntranslatedAsync,
} from "../../../../Services/DictionaryService.js";
import loadWordsAsync from "../../../../API/MyDictionary/LoadWords.js";
import sendWordsAsync from "../../../../API/MyDictionary/SendWords.js";

export default function MyDictionary() {
  const [renderWords, setRenderWords] = useState([]);
  const [allWords, setAllWords] = useState([]);
  useEffect(() => {
    const fetchData = async () => {
      const data = await loadWordsAsync();
      setRenderWords(data);
      setAllWords(data);
    };
    fetchData();
  }, []);

  const [edit, setEdit] = useState(false);
  const wordsContainer = useRef();
  useEffect(() => {
    if (edit == true && wordsContainer) {
      wordsContainer.current.scrollTop = wordsContainer.current.scrollHeight;
    }
  }, [edit]);

  return (
    <>
      <div className={styles.search}>
        <Search onSearch={onSearch}></Search>
      </div>

      <div className={styles.tableHat}>
        <label className={styles.tableHatText}>Word/Sentence</label>
        <label className={styles.tableHatText}>Translation</label>
      </div>

      <div className={styles.table} ref={wordsContainer}>
        {renderWords.map((row, index) => (
          <TableRow
            allWords={allWords}
            setAllWords={setAllWords}
            setWords={setRenderWords}
            words={renderWords}
            index={index}
            key={index}
            edit={edit}
            word={row[0]}
            translation={row[1]}
          ></TableRow>
        ))}
      </div>

      <div className={styles.buttonContainer}>
        <CommonButton
          handler={addNewWord}
          text={"Add new word"}
          isOrange={true}
        ></CommonButton>
        <CommonButton
          text={edit ? "Save" : "Edit word/translation"}
          isOrange={false}
          handler={!edit ? editHandler : saveHandler}
        ></CommonButton>
      </div>
    </>
  );

  function onSearch(input) {
    setRenderWords(selectWordsByBeginning(allWords, input));
  }
  function editHandler() {
    setEdit(true);
  }
  async function saveHandler() {
    setEdit(false);

    const result = await translateUntranslatedAsync(removeEmptyItems(allWords));
    setAllWords(result);
    setRenderWords(mapWordsTranslation(removeEmptyItems(renderWords), result));

    sendWordsAsync(result);
  }
  function addNewWord() {
    setEdit(true);
    setRenderWords((prevState) => [...prevState, ["", ""]]);
    setAllWords((prevState) => [...prevState, ["", ""]]);
  }
}
