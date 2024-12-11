import { studentTeacherClient } from "../../Constants/BackendServerClients";
import { authKey } from "../../Constants/LocalStorageKeys";

export default function getTeachers() {
  const token = localStorage.getItem(authKey);
  return studentTeacherClient
    .get("/getTeachers", {
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
