import { makeAutoObservable } from "mobx";
import {
  signalRConnectionKey,
  signalrMessagesKey,
} from "../../Constants/LocalStorageKeys";

class ForumStore {
  connection = JSON.parse(localStorage.getItem(signalRConnectionKey));
  messages = JSON.parse(localStorage.getItem(signalrMessagesKey));
  constructor() {
    makeAutoObservable(this);
    this.connection = JSON.parse(localStorage.getItem(signalRConnectionKey));
  }

  setConnection(connection) {
    this.connection = connection;
    localStorage.setItem(signalRConnectionKey, JSON.stringify(this.connection));
  }

  addMessage(message) {
    this.messages = [...this.messages, message];
    localStorage.setItem(signalrMessagesKey, JSON.stringify(this.messages));
  }
  setMessages(messages) {
    this.messages = messages;
    localStorage.setItem(signalrMessagesKey, JSON.stringify(this.messages));
  }

  disconnect() {
    this.connection.stop();
  }
}

const forumStore = new ForumStore();

export default forumStore;
