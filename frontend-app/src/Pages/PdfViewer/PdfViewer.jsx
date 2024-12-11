import React, { useState, useEffect } from "react";
import { Page, Document, pdfjs } from "react-pdf";
import { useLocation, useNavigate } from "react-router-dom";
import styles from "./PdfViewer.module.css";
import CommonButton from "../../Components/CommonButton/CommonButton";
import { worksPath } from "../../Constants/Paths";
import PdfHandler from "./PdfHandler";
pdfjs.GlobalWorkerOptions.workerSrc = `//cdnjs.cloudflare.com/ajax/libs/pdf.js/${pdfjs.version}/pdf.worker.js`;

const PDFViewerPage = () => {
  const navigate = useNavigate();
  const [numPages, setNumPages] = useState(null);
  const [file, setFile] = useState(null);
  const { state } = useLocation();
  const {
    pdfData,
    documentBase64,
    documentStatus,
    otherLogin,
    documentName,
    id,
    role,
    myLogin,
  } = state || {};

  function onDocumentLoadSuccess({ numPages }) {
    setNumPages(numPages);
  }

  return (
    <>
      <div className={styles.document}>
        <Document file={pdfData} onLoadSuccess={onDocumentLoadSuccess}>
          {Array.from(new Array(numPages), (el, index) => (
            <Page key={`page_${index + 1}`} pageNumber={index + 1} />
          ))}
        </Document>
      </div>

      <div className={styles.buttonContainer}>
        <CommonButton
          text={"Back"}
          isOrange={true}
          handler={() => {
            navigate(worksPath, {
              state: {
                myLogin: myLogin,
                otherLogin: otherLogin,
                role: role,
              },
            });
          }}
        ></CommonButton>
        <PdfHandler
          documentStatus={documentStatus}
          navigate={navigate}
          documentBase64={documentBase64}
          role={role}
          documentName={documentName}
          id={id}
          status={documentStatus}
          otherLogin={otherLogin}
          myLogin={myLogin}
        ></PdfHandler>
      </div>
    </>
  );
};

export default PDFViewerPage;
