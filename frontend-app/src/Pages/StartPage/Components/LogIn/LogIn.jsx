import { Link, useNavigate } from "react-router-dom";
import { mainPath, registrationPath } from "../../../../Constants/Paths.js";
import IsValidLoginData, {
  validateSignInData,
} from "../../../../Helpers/IsValidLoginData.js";
import SendLoginDataAsync from "../../../../API/User/SendLoginData.js";
import styles from "./LogIn.module.css";
import signIn from "../../../../API/User/SignIn.js";
import { authKey } from "../../../../Constants/LocalStorageKeys.js";
import processSignIn from "../../../../Services/SignInService.js";

async function handleSubmit(loginData, navigate) {
  console.log(loginData);
  const validationResult = validateSignInData(loginData);
  if (validationResult === "OK") {
    try {
      await processSignIn(navigate, loginData);
    } catch (error) {
      showError("Пользователь не найден");
    }
  } else {
    showError(validationResult);
  }
}

function showError(error) {
  document.getElementById("enter-error-label").style.display = "block";
  document.getElementById("enter-error-label").innerText = error;
}

export default function LogIn() {
  let loginData = {
    login: "",
    password: "",
  };
  const navigate = useNavigate();

  return (
    <form className={styles.logInForm}>
      <div className={styles.formInput}>
        <label className={styles.logInLabel}>Login</label>
        <input
          className={styles.logInInput}
          onChange={(event) => {
            loginData.login = event.target.value;
          }}
        />
      </div>
      <div className={styles.formInput}>
        <label className={styles.logInLabel}>Password</label>
        <input
          type="password"
          className={styles.logInInput}
          onChange={(event) => {
            loginData.password = event.target.value;
          }}
        />
      </div>
      <label id="enter-error-label"></label>
      <Link className={styles.link} to={registrationPath}>
        Or create a new account
      </Link>
      <button
        className={styles.formButton}
        type={"submit"}
        onClick={async (event) => {
          event.preventDefault();
          await handleSubmit(loginData, navigate);
        }}
      >
        LogIn
      </button>
    </form>
  );
}
