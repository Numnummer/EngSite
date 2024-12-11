import sendWordsAsync from "../MyDictionary/SendWords";

export default async function sendNewWord(word, translation) {
  await sendWordsAsync([[word, translation]]);
}
