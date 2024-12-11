import Work from "../Work/Work";
import styles from "./WorksContainer.module.css";
import { useEffect, useRef, useState } from "react";

function WorksContainer({ works, onWorkClick, setNavigatorSwitcher }) {
  const messageContainer = useRef();
  const scrollToBottom = () => {
    if (messageContainer) {
      messageContainer.current.scrollTop =
        messageContainer.current.scrollHeight;
    }
  };
  useEffect(() => {
    scrollToBottom();
  }, []);

  return (
    <div className={styles.container} ref={messageContainer}>
      {works &&
        works.map((work, index) => (
          <Work
            setNavigatorSwitcher={setNavigatorSwitcher}
            work={work}
            key={index}
            handler={onWorkClick}
          ></Work>
        ))}
    </div>
  );
}

export default WorksContainer;
