import { studentTeacherClient } from "../../Constants/BackendServerClients";
import { authKey } from "../../Constants/LocalStorageKeys";

export default function addStudent(studentLogin) {
  const token = localStorage.getItem(authKey);
  return studentTeacherClient
    .post(
      "/addStudent",
      { login: studentLogin },
      {
        headers: {
          Authorization: `Bearer ${token}`,
        },
      }
    )
    .then((response) => {
      return response.data;
    })
    .catch((error) => {
      throw new Error(error);
    });
}
