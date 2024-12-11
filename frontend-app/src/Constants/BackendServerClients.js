import axios from "axios";

const serverOrigin = "https://localhost:7074";
export const userClient = axios.create({
  baseURL: `${serverOrigin}`,
});

export const wordsClient = axios.create({
  baseURL: `${serverOrigin}/words`,
});

export const textsClient = axios.create({
  baseURL: `${serverOrigin}/text`,
});

export const statsClient = axios.create({
  baseURL: `${serverOrigin}/stats`,
});

export const forumClient = axios.create({
  baseURL: `${serverOrigin}/forumControl`,
});

export const studentTeacherClient = axios.create({
  baseURL: `${serverOrigin}/studentTeacher`,
});

export const worksClient = axios.create({
  baseURL: `${serverOrigin}/works`,
});

export const translationClient = axios.create({
  baseURL: `${serverOrigin}/translate`,
});
