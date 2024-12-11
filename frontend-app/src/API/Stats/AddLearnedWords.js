import { statsClient } from "../../Constants/BackendServerClients";
import { authKey } from "../../Constants/LocalStorageKeys";

export default function addLearnedWords(wordsCount) {
  const token = localStorage.getItem(authKey);
  return statsClient
    .post(
      "/addLearnedWords",
      { learnedWords: wordsCount },
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
