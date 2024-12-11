import { useNavigate } from "react-router-dom";
import NavBar from "../../Components/NavBar/NavBar";
import {
  aboutPath,
  forumPath,
  mainPath,
  pdfViwerPath,
  profilePath,
  startPagePath,
  worksPath,
} from "../../Constants/Paths";
import { useEffect, useState } from "react";
import CommonButton from "../../Components/CommonButton/CommonButton";
import styles from "./UserRelations.module.css";
import UserDataField from "./Components/UserDataField";
import { authKey } from "../../Constants/LocalStorageKeys";
import { jwtDecode } from "jwt-decode";
import getTeachers from "../../API/StudentTeacher/GetTeachers";
import getStudents from "../../API/StudentTeacher/GetStudents";
import Modal from "react-modal";
import { ToastContainer, toast } from "react-toastify";
import addStudent from "../../API/StudentTeacher/AddStudent";
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

  const [users, setUsers] = useState();
  const [role, setRole] = useState();
  const [login, setLogin] = useState();

  useEffect(() => {
    const token = localStorage.getItem(authKey);
    if (token) {
      const tokenClaims = Object.values(jwtDecode(token));
      setLogin(tokenClaims[0]);
      setRole(tokenClaims[1]);

      if (tokenClaims[1] === "User") {
        getTeachers()
          .then((teachers) => {
            setUsers(teachers);
          })
          .catch((error) => {
            console.log(error);
          });
      } else if (tokenClaims[1] === "Teacher") {
        getStudents()
          .then((students) => {
            setUsers(students);
          })
          .catch((error) => {
            console.log(error);
          });
      }
    }
  }, [navigatorSwitcher]);

  const modalStyles = {
    content: {
      top: "50%",
      left: "50%",
      transform: "translate(-50%, -50%)",
    },
  };
  const [modalIsOpen, setModalIsOpen] = useState(false);
  const [studentLogin, setStudentLogin] = useState("");

  function openModal() {
    setModalIsOpen(true);
  }
  function closeModal() {
    setModalIsOpen(false);
  }

  const addStudentHandler = () => {
    closeModal();
    addStudent(studentLogin)
      .then((res) => {
        setUsers((prev) => [...prev, studentLogin]);
        toast.success("Student added");
      })
      .catch((error) => {
        console.log(error);
        toast.error("Failed to add student");
      });
  };

  const handleClick = (otherLogin) => {
    navigate(worksPath, {
      state: { myLogin: login, otherLogin: otherLogin, role: role },
    });
    setNavigatorSwitcher(!navigatorSwitcher);
  };

  return (
    <div className={"page"}>
      <ToastContainer></ToastContainer>
      <NavBar
        buttonLabels={navBarLabels}
        buttonHandlers={navBarHandlers}
      ></NavBar>
      {users &&
        users.map((user, index) => (
          <UserDataField
            key={index}
            userName={user}
            handleClick={handleClick}
          ></UserDataField>
        ))}

      {role == "Teacher" && (
        <div className={styles.buttonContainer}>
          <CommonButton
            text={"Add student"}
            isOrange={true}
            handler={openModal}
          ></CommonButton>
        </div>
      )}

      <Modal
        isOpen={modalIsOpen}
        onRequestClose={closeModal}
        contentLabel="Modal"
        style={modalStyles}
      >
        <input
          className={styles.studentInput}
          type="text"
          placeholder="Student login"
          value={studentLogin}
          onChange={(event) => {
            setStudentLogin(event.target.value);
          }}
        ></input>
        <button className={styles.addStudentButton} onClick={addStudentHandler}>
          Add
        </button>
      </Modal>
    </div>
  );
}
