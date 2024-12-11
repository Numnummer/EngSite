import styles from "./WordsPractice.module.css";
import CommonButton from "../../../../Components/CommonButton/CommonButton.jsx";
import sendNewWord from "../../../../API/Common/SendNewWord.js";
import { useEffect, useState } from "react";
import getWordFromDictionary, {
  getWordsFromUserDictionary,
} from "../../../../API/WordsPractice/GetWordFromDictionary.js";
import {
  wordsLearned,
  wordsPracticeType,
} from "../../../../Constants/LocalStorageKeys.js";
import { ToastContainer, toast } from "react-toastify";
import "react-toastify/dist/ReactToastify.css";
import getWordsFromDictionary from "../../../../API/WordsPractice/GetWordFromDictionary.js";
import translateEnglishSentenceAsync from "../../../../API/Words/TranslateSentence.js";

export default function WordsPractice() {
  const [correctWord, setCorrectWord] = useState([]);
  const [correctEnglishWord, setCorrectEnglishWord] = useState("");
  const [words, setWords] = useState([]);
  const [answerSwitcher, setAnswerSwitcher] = useState("");
  const [practiceOption, setPracticeOption] = useState(
    localStorage.getItem(wordsPracticeType)
  );
  const [selectedWord, setSelectedWord] = useState();
  const selectWord = (sentence, practiceOption) => {
    if (practiceOption === "UserDictionary") {
      setSelectedWord(sentence[0]);
    } else {
      setSelectedWord(sentence);
    }
  };

  useEffect(() => {
    const fetchWords = async () => {
      if (practiceOption === "UserDictionary") {
        getWordsFromUserDictionary(6)
          .then((response) => {
            setWords(response);
            const randomIndex = Math.floor(Math.random() * response.length);
            const randomElement = response[randomIndex];
            setCorrectWord(randomElement);
          })
          .catch((error) => {
            console.log(error);
          });
      } else {
        getWordsFromDictionary(6)
          .then((response) => {
            setWords(response);
            const randomIndex = Math.floor(Math.random() * response.length);
            const randomElement = response[randomIndex];
            setCorrectEnglishWord(randomElement);
            translateEnglishSentenceAsync(randomElement).then((translation) => {
              setCorrectWord(translation);
            });
          })
          .catch((error) => {
            console.log(error);
          });
      }
    };
    fetchWords();
  }, [answerSwitcher]);

  return (
    <>
      <ToastContainer></ToastContainer>
      <div className={styles.verticalBox}>
        <label className={styles.word}>
          {practiceOption === "UserDictionary" ? correctWord[1] : correctWord}
        </label>
        <div className={styles.answers}>
          {words.map((sentence, index) => {
            let text =
              practiceOption === "UserDictionary" ? sentence[0] : sentence;
            return (
              <button
                onClick={() => selectWord(sentence, practiceOption)}
                key={index}
                className={
                  selectedWord
                    ? selectedWord === text
                      ? styles.selectedButton
                      : styles.answerButton
                    : styles.answerButton
                }
              >
                {text}
              </button>
            );
          })}
        </div>
        {/*<input className={styles.translationInput} />*/}
      </div>
      <div className={styles.buttonContainer}>
        <CommonButton
          text={"Skip"}
          isOrange={false}
          handler={() => {
            setAnswerSwitcher(!answerSwitcher);
            setSelectedWord(undefined);
          }}
        ></CommonButton>
        <CommonButton
          text={"AddToMyDictionary"}
          isOrange={true}
          handler={
            practiceOption === "UserDictionary"
              ? () => addToMyDictionary(correctWord[0], correctWord[1])
              : () => addToMyDictionary(correctEnglishWord, correctWord)
          }
        ></CommonButton>
        <CommonButton
          text={"Answer"}
          isOrange={false}
          handler={() => answer()}
        ></CommonButton>
      </div>
    </>
  );
  function addToMyDictionary(word, translation) {
    sendNewWord(word, translation);
    toast.success("Successfully added");
  }
  function answer() {
    if (selectedWord) {
      if (
        practiceOption === "UserDictionary" &&
        selectedWord === correctWord[0]
      ) {
        handleRightAnswer();
      } else if (selectedWord === correctEnglishWord) {
        handleRightAnswer();
      } else {
        toast.error("Incorrect, try again");
      }
    } else {
      toast.error("Choose the sentence");
    }
  }

  function handleRightAnswer() {
    setAnswerSwitcher(!answerSwitcher);
    setSelectedWord(undefined);
    toast.success("That's right");
    localStorage.setItem(
      wordsLearned,
      Number(localStorage.getItem(wordsLearned)) + 1
    );
  }
}
