import signIn from "../API/User/SignIn";
import { authKey } from "../Constants/LocalStorageKeys";
import { mainPath } from "../Constants/Paths";
import { configureChatConnection } from "./Forum/ForumService";

export default async function processSignIn(navigate, loginData) {
  localStorage.removeItem(authKey);
  const token = await signIn(loginData);
  if (token != null) {
    localStorage.setItem(authKey, token);
    navigate(mainPath);
    configureChatConnection();
  }
}
