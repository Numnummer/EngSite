import { wordsClient } from "../../Constants/BackendServerClients";
import { authKey } from "../../Constants/LocalStorageKeys";

export default async function loadWordsAsync() {
  const token = localStorage.getItem(authKey);
  const response = await wordsClient.get("/getWords", {
    headers: {
      Authorization: `Bearer ${token}`,
    },
  });
  return response.data;
}
