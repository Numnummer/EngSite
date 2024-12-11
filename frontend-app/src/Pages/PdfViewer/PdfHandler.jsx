import { useEffect, useState } from "react";
import CommonButton from "../../Components/CommonButton/CommonButton";
import {
  created,
  done,
  sentStudent,
  waitCheck,
} from "../../Constants/DocumentStatus";
import createDocument from "../../API/Works/CreateDocument";
import { worksPath } from "../../Constants/Paths";
import saveDocument from "../../API/Works/SaveDocument";
import sendDocument from "../../API/Works/SendDocument";
import sendDocumentForRevision from "../../API/Works/SendDocumentForRevision";

export default function ({
  documentStatus,
  navigate,
  documentBase64,
  documentName,
  role,
  id,
  status,
  myLogin,
  otherLogin,
}) {
  const [firstButton, setFirstButton] = useState();
  const [secondButton, setSecondButton] = useState();
  useEffect(() => {
    switch (documentStatus) {
      case created:
        if (role === "Teacher") {
          setFirstButton(
            <CommonButton
              text={"Save"}
              isOrange={false}
              handler={() => {
                saveDocument(documentBase64, documentName, id, status).then(
                  (res) => {
                    navigate(worksPath, {
                      state: {
                        myLogin: myLogin,
                        otherLogin: otherLogin,
                        role: role,
                      },
                    });
                  }
                );
              }}
            ></CommonButton>
          );
          setSecondButton(
            <CommonButton
              text={"Send"}
              isOrange={false}
              handler={() => {
                console.log({ documentBase64, documentName, id, status });
                sendDocument(documentBase64, documentName, id, status).then(
                  (res) => {
                    navigate(worksPath, {
                      state: {
                        myLogin: myLogin,
                        otherLogin: otherLogin,
                        role: role,
                      },
                    });
                  }
                );
              }}
            ></CommonButton>
          );
        } else {
          setFirstButton(null);
          setSecondButton(null);
        }
        break;

      case sentStudent:
        setFirstButton(null);
        if (role === "User") {
          setSecondButton(
            <CommonButton
              text={"Send"}
              isOrange={false}
              handler={() => {
                sendDocument(documentBase64, documentName, id, status).then(
                  (res) => {
                    navigate(worksPath, {
                      state: {
                        myLogin: myLogin,
                        otherLogin: otherLogin,
                        role: role,
                      },
                    });
                  }
                );
              }}
            ></CommonButton>
          );
        } else {
          setSecondButton(null);
        }
        break;

      case waitCheck:
        if (role === "Teacher") {
          setFirstButton(
            <CommonButton
              text={"Send checked"}
              isOrange={false}
              handler={() => {
                sendDocument(documentBase64, documentName, id, status).then(
                  (res) => {
                    navigate(worksPath, {
                      state: {
                        myLogin: myLogin,
                        otherLogin: otherLogin,
                        role: role,
                      },
                    });
                  }
                );
              }}
            ></CommonButton>
          );
          setSecondButton(
            <CommonButton
              text={"Send for revision"}
              isOrange={false}
              handler={() => {
                sendDocumentForRevision(
                  documentBase64,
                  documentName,
                  id,
                  status
                ).then((res) => {
                  navigate(worksPath, {
                    state: {
                      myLogin: myLogin,
                      otherLogin: otherLogin,
                      role: role,
                    },
                  });
                });
              }}
            ></CommonButton>
          );
        } else {
          setFirstButton(null);
          setSecondButton(null);
        }
        break;

      case done:
        setFirstButton(null);
        setSecondButton(null);
        break;

      default:
        if (role === "Teacher") {
          setFirstButton(
            <CommonButton
              text={"Save"}
              isOrange={false}
              handler={() => {
                saveDocument(documentBase64, documentName, id, status).then(
                  (res) => {
                    navigate(worksPath, {
                      state: {
                        myLogin: myLogin,
                        otherLogin: otherLogin,
                        role: role,
                      },
                    });
                  }
                );
              }}
            ></CommonButton>
          );
          setSecondButton(
            <CommonButton
              text={"Send"}
              isOrange={false}
              handler={() => {
                console.log({ documentBase64, documentName, id, status });
                sendDocument(documentBase64, documentName, id, status).then(
                  (res) => {
                    navigate(worksPath, {
                      state: {
                        myLogin: myLogin,
                        otherLogin: otherLogin,
                        role: role,
                      },
                    });
                  }
                );
              }}
            ></CommonButton>
          );
        } else {
          setFirstButton(null);
          setSecondButton(null);
        }
        break;
    }
  }, [documentStatus]);

  return (
    <>
      {firstButton}
      {secondButton}
    </>
  );
}
