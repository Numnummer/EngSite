import { userClient } from "../../Constants/BackendServerClients";
import { authKey } from "../../Constants/LocalStorageKeys";

export default function postUserPhoto(photo) {
  const token = localStorage.getItem(authKey);
  console.log(photo);
  return userClient
    .post("/postUserPhoto", photo, {
      headers: {
        Authorization: `Bearer ${token}`,
      },
    })
    .then((response) => {
      return response.data;
    })
    .catch((error) => {
      throw new Error(error);
    });
}
