import { wordsClient } from "../../Constants/BackendServerClients.js";
import { authKey } from "../../Constants/LocalStorageKeys.js";
import { randomWordClient } from "../../Constants/RandomWordClient.js";
import translateEnglishSentenceAsync from "../Words/TranslateSentence.js";

export default async function getDailyWord() {
  const token = localStorage.getItem(authKey);
  let result = await wordsClient.get("/getRandomWord", {
    headers: {
      Authorization: `Bearer ${token}`,
    },
  });

  let word = result.data;
  let translation = await translateEnglishSentenceAsync(word);
  return [word, translation];
}
