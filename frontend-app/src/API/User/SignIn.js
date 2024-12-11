import { userClient } from "../../Constants/BackendServerClients";

export default function signIn(signInData) {
  return userClient
    .post("/enter", signInData)
    .then((response) => {
      console.log(response);
      return response.data;
    })
    .catch((error) => {
      console.log(error);
      throw new Error(error);
    });
}
