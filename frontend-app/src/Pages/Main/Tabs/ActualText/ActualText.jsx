import styles from "./ActualText.module.css";
import { useEffect, useState } from "react";
import { addTextByNameAsync } from "../../../../API/Texts/ActualTextApi.js";
import {
  actualText,
  actualTextLevel,
  actualTextName,
} from "../../../../Constants/LocalStorageKeys.js";
import translateEnglishSentenceAsync from "../../../../API/Words/TranslateSentence.js";
import CommonButton from "../../../../Components/CommonButton/CommonButton.jsx";

const Modal = ({ text, x, y, fontSize }) => {
  return (
    <div
      style={{
        position: "fixed",
        left: x + "px",
        top: y + "px",
        width: "200px",
        background: "white",
        border: "1px solid black",
        padding: "10px",
        fontSize: fontSize,
      }}
    >
      {text}
    </div>
  );
};

export default function ActualText({ setLocation }) {
  const [text, setText] = useState("");
  const [textName, setTextName] = useState("");
  const [fontSize, setFontSize] = useState(28);
  const [showTranslation, setShowTranslation] = useState(false);
  const [mousePosition, setMousePosition] = useState({ x: 0, y: 0 });
  const [translation, setTranslation] = useState("");
  useEffect(() => {
    setText(localStorage.getItem(actualText));
    let textFileName = localStorage.getItem(actualTextName);
    setTextName(textFileName.substring(0, textFileName.length - 4));
  }, []);

  let cache = new Map();
  return (
    <div onMouseUp={textHighlited} style={{ height: "100%" }}>
      <div className={styles.settings}>
        <label style={{ fontSize: `${fontSize}px` }}>Font Size </label>
        <input
          className={styles.input}
          type={"number"}
          value={fontSize}
          onChange={(event) => setFontSize(+event.target.value)}
          style={{ fontSize: `${fontSize - 10}px` }}
        />
      </div>
      <label
        className={styles.textName}
        style={{ fontSize: `${fontSize + 8}px` }}
      >
        {textName}
      </label>
      <div className={styles.text} style={{ fontSize: `${fontSize}px` }}>
        {text}
      </div>
      {showTranslation && (
        <Modal
          text={translation}
          x={mousePosition.x}
          y={mousePosition.y + 20}
          fontSize={fontSize - 10}
        />
      )}
      <div className={styles.buttonContainer}>
        <CommonButton
          text={"Add to my texts"}
          isOrange={true}
          handler={addToTexts}
        ></CommonButton>
        <CommonButton
          text={"Back to my texts"}
          isOrange={false}
          handler={backToTexts}
        ></CommonButton>
      </div>
    </div>
  );

  function textHighlited(event) {
    setShowTranslation(false);
    const selectedText = window.getSelection().toString();
    if (selectedText != "") {
      setMousePosition({ x: +event.clientX, y: +event.clientY });
      setShowTranslation(true);
      if (cache.has(selectedText)) {
        console.log("Text from cache:", cache[selectedText]);
      } else {
        translateEnglishSentenceAsync(selectedText).then((result) => {
          cache[selectedText] = result;
          setTranslation(result);
        });
      }
    }
  }

  function addToTexts() {
    const name = localStorage.getItem(actualTextName);
    const level = localStorage.getItem(actualTextLevel);
    addTextByNameAsync(name, level);
  }
  function backToTexts() {
    setLocation("Texts");
  }
}
