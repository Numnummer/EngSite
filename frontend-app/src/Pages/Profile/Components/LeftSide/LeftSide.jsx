import styles from "./LeftSide.module.css";
import defaultImage from "../../../../../public/images/default-user.png";
import { useEffect, useState } from "react";
import postUserPhoto from "../../../../API/User/PostUserPhoto";
import { arrayBufferToBase64 } from "../../../../Helpers/ConvertArrayBufferToBase64";

export default function LeftSide({ userData }) {
  const [imageData, setImageData] = useState(null);
  const [imageLoadSwitcher, setImageLoadSwitcher] = useState(false);
  const onImageChange = (event) => {
    if (event.target.files && event.target.files[0]) {
      const file = event.target.files[0];
      if (file) {
        const reader = new FileReader();
        reader.onloadend = () => {
          const byteArray = new Uint8Array(reader.result);
          setImageData(arrayBufferToBase64(byteArray));
          setImageLoadSwitcher(!imageLoadSwitcher);
        };
        reader.readAsArrayBuffer(file);
      }
    }
  };

  useEffect(() => {
    if (userData) {
      setImageData(userData.photo);
    }
  }, [userData]);
  useEffect(() => {
    const postImage = () => {
      const data = imageData;
      postUserPhoto({ photoBase64: data })
        .then((result) => {
          console.log(result);
        })
        .catch((error) => {
          console.log(error);
        });
    };
    if (imageData != null) {
      console.log(imageData);
      postImage();
    }
  }, [imageLoadSwitcher]);
  return (
    <div className={styles.verticalBox}>
      <input
        type="file"
        accept="image/*"
        onChange={onImageChange}
        className="filetype"
      />
      {imageData == null ? (
        <img className={styles.image} src={defaultImage} />
      ) : (
        <img
          className={styles.image}
          src={`data:image/jpeg;base64,${imageData}`}
          alt={"preview"}
        />
      )}
      <label className={styles.fullName}>
        {userData ? userData.fullName : "No data"}
      </label>
    </div>
  );
}
