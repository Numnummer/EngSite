/* eslint-disable no-var, strict */
/* globals pdfjsLib */

"use strict";

if (typeof PDFJSDev !== "undefined" && PDFJSDev.test("PRODUCTION")) {
  __non_webpack_require__ = require;
}

var pdfjsWorker;
if (typeof __non_webpack_require__ === "function") {
  pdfjsWorker = __non_webpack_require__("./pdf.worker.entry.js");
} else {
  pdfjsWorker = require("./pdf.worker.entry.js");
}

pdfjsLib.GlobalWorkerOptions.workerPort = pdfjsWorker;
