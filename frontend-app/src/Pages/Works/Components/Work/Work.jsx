import { useState } from "react";
import styles from "./Work.module.css";
import deleteDocument from "../../../../API/Works/DeleteDocument";
import getDocument from "../../../../API/Works/GetDocument";

export default function ({ work, handler, setNavigatorSwitcher }) {
  const [optionStyle, setOptionStyle] = useState({ display: "none" });
  return (
    <div
      onMouseOver={() => {
        setOptionStyle(null);
      }}
      onMouseOut={() => {
        setOptionStyle({ display: "none" });
      }}
    >
      <div
        className={styles.workItem}
        onClick={() => {
          console.log(work.id);
          handler(work.id, work.name, work.status);
        }}
      >
        <div className={styles.row}>
          <label className={styles.name}>{work.name}</label>
          <label className={styles.status}>{work.status}</label>
        </div>
      </div>
      <div style={optionStyle} className={styles.options}>
        <button
          onClick={() => {
            deleteDocument(work.id, work.name)
              .then((res) => {
                setNavigatorSwitcher((prev) => !prev);
              })
              .catch((error) => {
                console.log(error);
              });
          }}
        >
          X
        </button>
        <button
          onClick={() => {
            getDocument(work.id, work.name)
              .then((doc) => {
                const byteArray = new Uint8Array(doc);
                const blob = new Blob([byteArray], { type: "application/pdf" });

                const url = URL.createObjectURL(blob);

                const link = document.createElement("a");
                link.href = url;
                link.download = work.name;

                document.body.appendChild(link);

                link.click();

                document.body.removeChild(link);
                URL.revokeObjectURL(url);
              })
              .catch((error) => {
                console.log(error);
              });
          }}
        >
          S
        </button>
      </div>
    </div>
  );
}
