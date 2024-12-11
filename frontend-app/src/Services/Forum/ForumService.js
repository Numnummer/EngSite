import { HubConnectionBuilder } from "@microsoft/signalr";
import { recieveMethod } from "../../Constants/SignalRMethods";
import forumStore from "./ForumStore";

export async function configureChatConnection() {
  const chatConnection = new HubConnectionBuilder()
    .withUrl("https://localhost:7074/forum")
    .build();
  chatConnection.on(recieveMethod, onRecieve);
  await chatConnection.start();
  forumStore.setConnection(chatConnection);
}

export function onRecieve(author, message, date) {
  forumStore.addMessage({ author: author, message: message, dateTime: date });
  console.log({ author: author, message: message, dateTime: date });
}
