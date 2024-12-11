import Search from "../../../../Components/Search/Search.jsx";
import styles from "./Texts.module.css";
import { useEffect, useState } from "react";
import TextRecord from "./Components/TextRecord/TextRecord.jsx";
import CommonButton from "../../../../Components/CommonButton/CommonButton.jsx";
import {
  fetchRandomTextAsync,
  GetAllTextsForUserAsync,
  getTextByName,
} from "../../../../API/Texts/TextApi.js";
import selectTextsByBeginning from "../../../../Services/TextService.js";
import Modal from "react-modal";
import ChooseLevel from "../../../../Components/ModalWindowContent/ChooseLevel/ChooseLevel.jsx";
import {
  actualText,
  actualTextLevel,
  actualTextName,
} from "../../../../Constants/LocalStorageKeys.js";
Modal.setAppElement("#root");

export default function Texts({ setLocation }) {
  const modalStyles = {
    content: {
      top: "50%",
      left: "50%",
      transform: "translate(-50%, -50%)",
    },
  };
  const [modalIsOpen, setModalIsOpen] = useState(false);

  const [renderTexts, setRenderTexts] = useState([]);
  const [allTexts, setAllTexts] = useState([]);

  useEffect(() => {
    const fetchTexts = async () => {
      let texts = await GetAllTextsForUserAsync();
      setAllTexts(texts);
      setRenderTexts(texts);
    };
    fetchTexts();
  }, []);

  return (
    <>
      <div className={styles.searcher}>
        <Search onSearch={onSearch}></Search>
      </div>
      <div className={styles.container}>
        {renderTexts.map((item, index) => (
          <TextRecord
            key={index}
            textName={item}
            handler={goToText}
          ></TextRecord>
        ))}
      </div>
      <div className={styles.buttonContainer}>
        <CommonButton
          text={"Go to current text"}
          isOrange={true}
          handler={() => setLocation("ActualText")}
        ></CommonButton>
        <CommonButton
          text={"Get Text"}
          isOrange={false}
          handler={openModal}
        ></CommonButton>
      </div>

      <Modal
        isOpen={modalIsOpen}
        onRequestClose={closeModal}
        contentLabel="Modal"
        style={modalStyles}
      >
        <ChooseLevel
          closeModal={closeModal}
          buttonsOnClick={getText}
        ></ChooseLevel>
      </Modal>
    </>
  );

  function onSearch(input) {
    setRenderTexts(selectTextsByBeginning(allTexts, input));
  }
  function getText(level) {
    setModalIsOpen(false);
    fetchRandomTextAsync(level)
      .then((result) => {
        setAllTexts((prevState) => [...prevState, result.fileName]);
        localStorage.setItem(actualTextName, result.fileName);
        localStorage.setItem(actualText, result.data);
        localStorage.setItem(actualTextLevel, level);
        setLocation("ActualText");
      })
      .catch((error) => {
        console.log(error);
      });
  }
  function goToText(textName) {
    getTextByName(textName)
      .then((result) => {
        localStorage.setItem(actualTextName, textName);
        localStorage.setItem(actualText, result.data);
        setLocation("ActualText");
      })
      .catch((error) => {
        console.log(error);
      });
  }
  function openModal() {
    setModalIsOpen(true);
  }
  function closeModal() {
    setModalIsOpen(false);
  }
}
