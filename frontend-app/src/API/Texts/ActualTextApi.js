import { textsClient } from "../../Constants/BackendServerClients";
import { authKey } from "../../Constants/LocalStorageKeys";

export async function getCurrentTextNameAsync() {}
export async function getTextAsync(name) {
  return "text asj lscioas qw";
}
export async function addTextByNameAsync(name, level) {
  const data = { textName: name, textLevel: level };
  const token = localStorage.getItem(authKey);

  await textsClient
    .post("/addText", data, {
      headers: {
        Authorization: `Bearer ${token}`,
      },
    })
    .then((response) => {
      console.log(response);
    })
    .catch((error) => {
      console.log(error);
    });
}
