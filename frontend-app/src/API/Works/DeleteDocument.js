import { worksClient } from "../../Constants/BackendServerClients";
import { authKey } from "../../Constants/LocalStorageKeys";

export default function deleteDocument(id, name) {
  const token = localStorage.getItem(authKey);
  return worksClient
    .post(
      "/deleteDocument",
      {
        DocumentMegaId: id,
        DocumentName: name,
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
