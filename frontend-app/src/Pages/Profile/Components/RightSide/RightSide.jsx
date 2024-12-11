import { useEffect } from "react";
import styles from "./RightSide.module.css";
export default function RightSide({ userData, userStats }) {
  useEffect(() => {
    console.log(userStats);
  }, [userStats]);
  return (
    <div className={styles.verticalBox}>
      <div className={styles.infoBox}>
        <label className={styles.info}>
          Email: {userData && userData.email}
        </label>
      </div>
      <hr className={styles.line} />
      <div className={styles.infoBox}>
        <label className={styles.info}>State</label>
        <label className={styles.info}>
          Right answers in words practice: {userStats && userStats.wordsLearned}
        </label>
      </div>
    </div>
  );
}
