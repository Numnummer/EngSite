import { userClient } from "../../Constants/BackendServerClients";

//Returns true if server gave success answer
export default async function SendLoginDataAsync(loginData) {
  return userClient
    .post("/register", loginData)
    .then((response) => {
      console.log(response);
      return true;
    })
    .catch((error) => {
      console.log(error);
      return false;
    });
}
