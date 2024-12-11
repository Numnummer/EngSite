import styles from "./SendMessageBar.module.css";
import image from "/public/images/send-message-image.png";
import SendMessageAsync from "../../Services/MessageService.js";
import { useRef } from "react";
export default function SendMessageBar({ userData }) {
  let message = "";
  const messageRef = useRef();
  return (
    <div className={styles.messageBar}>
      <input
        ref={messageRef}
        className={styles.messageInput}
        type={"text"}
        onChange={(event) => (message = event.target.value)}
      />
      <button
        onClick={() => {
          SendMessageAsync(userData.fullName, message);
          if (messageRef) messageRef.current.value = "";
        }}
      >
        <img className={styles.image} src={image} />
      </button>
    </div>
  );
}
