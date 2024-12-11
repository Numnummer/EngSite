import { wordsClient } from "../../Constants/BackendServerClients";
import { authKey } from "../../Constants/LocalStorageKeys";

export function removeSentence(sentence) {
  const token = localStorage.getItem(authKey);
  wordsClient.post("/removeSentence", sentence, {
    headers: {
      Authorization: `Bearer ${token}`,
    },
  });
}
