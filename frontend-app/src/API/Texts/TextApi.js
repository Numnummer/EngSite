import { textsClient } from "../../Constants/BackendServerClients";
import { authKey } from "../../Constants/LocalStorageKeys";

export async function GetAllTextsForUserAsync() {
  const token = localStorage.getItem(authKey);
  return textsClient
    .get("/getAllTexts", {
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

export async function fetchRandomTextAsync(level) {
  const token = localStorage.getItem(authKey);
  return textsClient
    .get(`/getRandomText/${level}`, {
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

export function getTextByName(name) {
  const token = localStorage.getItem(authKey);
  return textsClient
    .get(`/getText/${name}`, {
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
