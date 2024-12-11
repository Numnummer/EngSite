import styles from "./Registration.module.css";
import { useNavigate } from "react-router-dom";
import SendLoginDataAsync from "../../API/User/SendLoginData.js";
import { aboutPath, mainPath, startPagePath } from "../../Constants/Paths.js";
import NavBar from "../../Components/NavBar/NavBar.jsx";
import validateLoginData from "../../Helpers/IsValidLoginData.js";
import "../../styles/ErrorLabels.css";
import processSignIn from "../../Services/SignInService.js";

async function handleSubmit(loginData, navigate) {
  const validationResult = validateLoginData(loginData);
  if (validationResult === "OK") {
    if (await SendLoginDataAsync(loginData)) {
      const signInData = {
        login: loginData.login,
        password: loginData.password,
      };
      await processSignIn(navigate, signInData);
    }
  } else {
    document.getElementById("registration-error-label").style.display = "block";
    document.getElementById("registration-error-label").innerText =
      validationResult;
  }
}

export default function Registration() {
  const navigate = useNavigate();
  let loginData = {
    fullName: "",
    email: "",
    login: "",
    password: "",
    confirm: "",
    role: "User",
  };
  const labels = ["About", "Back"];
  const handlers = [() => navigate(aboutPath), () => navigate(startPagePath)];
  return (
    <div className={"page"}>
      <NavBar buttonLabels={labels} buttonHandlers={handlers}></NavBar>
      <form className={styles.regForm}>
        <div className={styles.title}>Registration</div>
        <div className={styles.formItem}>
          <label className={styles.formLabel}>Full Name</label>
          <input
            className={styles.formInput}
            onChange={(event) => {
              loginData.fullName = event.target.value;
            }}
            type={"text"}
          />
        </div>

        <div className={styles.formItem}>
          <label className={styles.formLabel}>Login</label>
          <input
            className={styles.formInput}
            onChange={(event) => {
              loginData.login = event.target.value;
            }}
            type={"text"}
          />
        </div>

        <div className={styles.formItem}>
          <label className={styles.formLabel}>Email</label>
          <input
            className={styles.formInput}
            onChange={(event) => {
              loginData.email = event.target.value;
            }}
            type={"text"}
          />
        </div>

        <div className={styles.formItem}>
          <label className={styles.formLabel}>Password</label>
          <input
            className={styles.formInput}
            onChange={(event) => {
              loginData.password = event.target.value;
            }}
            type={"password"}
          />
        </div>

        <div className={styles.formItem}>
          <label className={styles.formLabel}>Confirm</label>
          <input
            className={styles.formInput}
            onChange={(event) => {
              loginData.confirm = event.target.value;
            }}
            type={"password"}
          />
        </div>

        <div className={styles.formItem}>
          <label className={styles.formLabel}>Я учитель</label>
          <input
            type={"checkbox"}
            onChange={(event) => {
              let role = "User";
              if (event.target.checked) role = "Teacher";
              loginData.role = role;
            }}
          ></input>
        </div>

        <label id="registration-error-label"></label>

        <button
          className={styles.formButton}
          onClick={async (event) => {
            event.preventDefault();
            await handleSubmit(loginData, navigate);
          }}
        >
          Register
        </button>
      </form>
    </div>
  );
}
