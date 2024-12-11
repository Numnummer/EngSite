import { worksClient } from "../../Constants/BackendServerClients";
import { authKey } from "../../Constants/LocalStorageKeys";

export default function sendDocument(base64, name, id, status) {
  const token = localStorage.getItem(authKey);
  return worksClient
    .post(
      "/sendDocument",
      {
        DocumentBase64: base64,
        DocumentName: name,
        DocumentMegaId: id,
        DocumentStatus: status,
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
