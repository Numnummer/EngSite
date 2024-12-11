import { useLocation, useNavigate } from "react-router-dom";
import NavBar from "../../Components/NavBar/NavBar";
import {
  aboutPath,
  forumPath,
  mainPath,
  pdfViwerPath,
  profilePath,
  startPagePath,
} from "../../Constants/Paths";
import WorksContainer from "./Components/WorksContainer/WorksContainer";
import { useEffect, useState } from "react";
import CommonButton from "../../Components/CommonButton/CommonButton";
import styles from "./Works.module.css";
import { ToastContainer, toast } from "react-toastify";
import Modal from "react-modal";
import { created } from "../../Constants/DocumentStatus";
import getDocuments from "../../API/Works/GetDocuments";
import getDocument from "../../API/Works/GetDocument";
import { arrayBufferToBase64 } from "../../Helpers/ConvertArrayBufferToBase64";
import { authKey } from "../../Constants/LocalStorageKeys";
import createDocument from "../../API/Works/CreateDocument";
Modal.setAppElement("#root");

export default function ({ navigatorSwitcher, setNavigatorSwitcher }) {
  const navigate = useNavigate();
  const navBarLabels = ["About", "Forum", "Main", "Profile", "LogOut"];
  const navBarHandlers = [
    aboutHandler,
    forumHandler,
    mainHandler,
    profileHandler,
    logOutHandler,
  ];

  const { state } = useLocation();
  const { myLogin, otherLogin, role } = state || {};

  const [selectedFile, setSelectedFile] = useState(null);

  function aboutHandler() {
    navigate(aboutPath);
    setNavigatorSwitcher((prev) => !prev);
  }
  function forumHandler() {
    navigate(forumPath);
    setNavigatorSwitcher((prev) => !prev);
  }
  function logOutHandler() {
    localStorage.removeItem(authKey);
    navigate(startPagePath);
    setNavigatorSwitcher((prev) => !prev);
    forumStore.disconnect();
  }
  function mainHandler() {
    navigate(mainPath);
    setNavigatorSwitcher((prev) => !prev);
  }
  function profileHandler() {
    navigate(profilePath);
    setNavigatorSwitcher((prev) => !prev);
  }
  function pdfViewerHandler() {
    if (selectedFile && selectedFile.type === "application/pdf") {
      const reader = new FileReader();
      reader.onload = () => {
        const base64 = reader.result.split(",")[1];
        const typedArray = new Uint8Array(
          atob(base64)
            .split("")
            .map((c) => c.charCodeAt(0))
        );
        const blob = new Blob([typedArray], { type: "application/pdf" });
        closeModal();
        createDocument(base64, selectedFile.name, otherLogin, null)
          .then((res) => {
            navigate(pdfViwerPath, {
              state: {
                pdfData: blob,
                documentBase64: base64,
                documentStatus: created,
                otherLogin: otherLogin,
                documentName: selectedFile.name,
                id: null,
                role: role,
                myLogin: myLogin,
              },
            });
          })
          .catch((error) => {
            console.log(error);
          });
      };
      reader.readAsDataURL(selectedFile);
    } else {
      toast.error("Failed to open pdf file");
    }
  }

  const [works, setWorks] = useState([]);
  useEffect(() => {
    if (role === "User") {
      getDocuments(otherLogin, myLogin)
        .then((docs) => {
          setWorks(docs);
        })
        .catch((error) => {
          console.log(error);
        });
    } else if (role === "Teacher") {
      getDocuments(myLogin, otherLogin)
        .then((docs) => {
          setWorks(docs);
        })
        .catch((error) => {
          console.log(error);
        });
    }

    //setWorks([{ name: "asdqwe", status: "created" }]);
  }, [navigatorSwitcher]);

  const handleFileChange = (event) => {
    setSelectedFile(event.target.files[0]);
  };

  const modalStyles = {
    content: {
      top: "50%",
      left: "50%",
      transform: "translate(-50%, -50%)",
    },
  };
  const [modalIsOpen, setModalIsOpen] = useState(false);
  function openModal() {
    setModalIsOpen(true);
  }
  function closeModal() {
    setModalIsOpen(false);
  }
  function onWorkClick(id, name, status) {
    getDocument(id, name)
      .then((document) => {
        const byteArray = new Uint8Array(document);
        navigate(pdfViwerPath, {
          state: {
            pdfData: document,
            documentBase64: arrayBufferToBase64(document),
            documentStatus: status,
            otherLogin: otherLogin,
            documentName: name,
            id: id,
            role: role,
            myLogin: myLogin,
          },
        });
      })
      .catch((error) => {
        console.log(error);
      });
  }

  return (
    <div className={"page"}>
      <ToastContainer></ToastContainer>
      <NavBar
        buttonLabels={navBarLabels}
        buttonHandlers={navBarHandlers}
      ></NavBar>
      <WorksContainer
        setNavigatorSwitcher={setNavigatorSwitcher}
        works={works}
        onWorkClick={onWorkClick}
      ></WorksContainer>

      <div className={styles.buttonContainer}>
        {role === "Teacher" && (
          <CommonButton
            text={"Load work"}
            isOrange={true}
            handler={openModal}
          ></CommonButton>
        )}
      </div>
      <Modal
        isOpen={modalIsOpen}
        onRequestClose={closeModal}
        contentLabel="Modal"
        style={modalStyles}
      >
        <label>Choose your pdf file</label>
        <input type="file" onChange={handleFileChange} accept=".pdf" />
        <button onClick={pdfViewerHandler}>Open</button>
      </Modal>
    </div>
  );
}
