import { wordsClient } from "../../Constants/BackendServerClients";
import { authKey } from "../../Constants/LocalStorageKeys";

export default async function sendWordsAsync(words) {
  const token = localStorage.getItem(authKey);
  await wordsClient.post("/setWords", words, {
    headers: {
      Authorization: `Bearer ${token}`,
    },
  });
}
