import { translationClient } from "../../Constants/BackendServerClients.js";
import { authKey } from "../../Constants/LocalStorageKeys.js";
import {
  enRuTranslationClient,
  ruEnTranslationClient,
} from "../../Constants/TranslationClients.js";

export default async function translateEnglishSentenceAsync(sentence) {
  //let response = await enRuTranslationClient.get(sentence);
  let response = await getEnglishTranslation(sentence);
  console.log(response);
  return response.data[0][0][0];
}
function getEnglishTranslation(sentence) {
  const token = localStorage.getItem(authKey);
  return translationClient
    .get("/enRu", {
      headers: {
        Authorization: `Bearer ${token}`,
      },
      params: {
        sentence: sentence,
      },
    })
    .then((response) => {
      return response;
    })
    .catch((err) => {
      throw new Error(err);
    });
}

export async function translateRussianSentenceAsync(sentence) {
  //let response = await ruEnTranslationClient.get(sentence);
  let response = await getRussianTranslation(sentence);
  return response.data[0][0][0];
}

function getRussianTranslation(sentence) {
  const token = localStorage.getItem(authKey);
  return translationClient
    .get("/ruEn", {
      headers: {
        Authorization: `Bearer ${token}`,
      },
      params: {
        sentence: sentence,
      },
    })
    .then((response) => {
      return response;
    })
    .catch((err) => {
      throw new Error(err);
    });
}
