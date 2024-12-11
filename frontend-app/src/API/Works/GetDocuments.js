import { worksClient } from "../../Constants/BackendServerClients";
import { authKey } from "../../Constants/LocalStorageKeys";

export default function getDocuments(teacherLogin, studentLogin) {
  const token = localStorage.getItem(authKey);
  return worksClient
    .get("/getDocuments", {
      headers: {
        Authorization: `Bearer ${token}`,
      },
      params: {
        teacherLogin: teacherLogin,
        studentLogin: studentLogin,
      },
    })
    .then((response) => {
      return response.data;
    })
    .catch((error) => {
      throw new Error(error);
    });
}
