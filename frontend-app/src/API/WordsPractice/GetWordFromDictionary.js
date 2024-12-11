import { wordsClient } from "../../Constants/BackendServerClients";
import { authKey } from "../../Constants/LocalStorageKeys";

export default async function getWordsFromDictionary(count) {
  const token = localStorage.getItem(authKey);
  return wordsClient
    .get(`getRandomDictionaryWords/${count}`, {
      headers: {
        Authorization: `Bearer ${token}`,
      },
    })
    .then((response) => {
      return response.data;
    })
    .catch((error) => {
      throw new Error(error);
    });
}

export function getWordsFromUserDictionary(count) {
  const token = localStorage.getItem(authKey);
  return wordsClient
    .get(`getRandomUserDictionaryWords/${count}`, {
      headers: {
        Authorization: `Bearer ${token}`,
      },
    })
    .then((response) => {
      return response.data;
    })
    .catch((error) => {
      throw new Error(error);
    });
}
