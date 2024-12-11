import styles from "./MessageContainer.module.css";
import Message from "../Message/Message.jsx";
import GetDateTimeNow from "../../../../Services/TimeService.js";
import forumStore from "../../../../Services/Forum/ForumStore.js";
import { useEffect, useRef, useState } from "react";
import { observer } from "mobx-react";
import { ru } from "date-fns/locale";
import { format } from "date-fns";
function MessageContainer({ userData }) {
  const messageContainer = useRef();
  const scrollToBottom = () => {
    if (messageContainer) {
      messageContainer.current.scrollTop =
        messageContainer.current.scrollHeight;
    }
  };
  useEffect(() => {
    scrollToBottom();
  }, [forumStore.messages]);

  return (
    <div className={styles.container} ref={messageContainer}>
      {forumStore.messages &&
        forumStore.messages.map((message, index) => (
          <Message
            key={index}
            author={message.author}
            message={message.message}
            date={format(message.dateTime, "d MMMM HH:mm", { locale: ru })}
            isSelf={userData && message.author === userData.fullName}
          ></Message>
        ))}
    </div>
  );
}

export default observer(MessageContainer);
