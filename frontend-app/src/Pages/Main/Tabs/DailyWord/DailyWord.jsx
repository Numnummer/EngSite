import styles from "./DailyWord.module.css";
import CommonButton from "../../../../Components/CommonButton/CommonButton.jsx";
import { useEffect, useState } from "react";
import sendNewWord from "../../../../API/Common/SendNewWord.js";
import getDailyWord from "../../../../API/DailyWords/GetDailyWord.js";
import { ToastContainer, toast } from "react-toastify";
import "react-toastify/dist/ReactToastify.css";

export default function DailyWord() {
  const [word, setWord] = useState("");
  const [translation, setTranslation] = useState("");
  const [skipSwither, setSkipSwitcher] = useState(true);
  useEffect(() => {
    const fetchData = async () => {
      const [fetchedWord, fetchedTranslation] = await getDailyWord();
      setWord(fetchedWord);
      setTranslation(fetchedTranslation);
    };

    fetchData();
  }, [skipSwither]);
  return (
    <>
      <ToastContainer></ToastContainer>
      <div className={styles.verticalContainer}>
        <label className={styles.word}>{word}</label>
        <label className={styles.translation}>{translation}</label>
      </div>
      <div className={styles.buttonContainer}>
        <CommonButton
          text={"Add to my dictionary"}
          isOrange={true}
          handler={addToMyDictionary}
        ></CommonButton>
        <CommonButton
          text={"Skip"}
          isOrange={false}
          handler={skipWord}
        ></CommonButton>
      </div>
    </>
  );

  async function addToMyDictionary() {
    await sendNewWord(word, translation);
    toast.success("Successfully added");
  }

  function skipWord() {
    setSkipSwitcher(!skipSwither);
  }
}
