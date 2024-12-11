import { worksClient } from "../../Constants/BackendServerClients";
import { authKey } from "../../Constants/LocalStorageKeys";

export default function getDocument(id, name) {
  const token = localStorage.getItem(authKey);
  return worksClient
    .get("/getDocument", {
      headers: {
        Authorization: `Bearer ${token}`,
      },
      params: {
        documentId: id,
        documentName: name,
      },
      responseType: "arraybuffer",
    })
    .then((response) => {
      return response.data;
    })
    .catch((error) => {
      throw new Error(error);
    });
}
