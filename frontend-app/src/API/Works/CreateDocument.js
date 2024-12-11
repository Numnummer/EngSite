import { worksClient } from "../../Constants/BackendServerClients";
import { authKey } from "../../Constants/LocalStorageKeys";

export default function createDocument(base64, name, studentLogin, id) {
  const token = localStorage.getItem(authKey);
  console.log(studentLogin);
  return worksClient
    .post(
      "/addDocument",
      {
        DocumentBase64: base64,
        DocumentName: name,
        StudentLogin: studentLogin,
        DocumentId: id,
      },
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
