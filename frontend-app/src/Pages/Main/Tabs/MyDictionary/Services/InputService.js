export function saveInput(
  value,
  words,
  allWords,
  index,
  isTranslation,
  setWords,
  setAllWords,
  setCurrentValue
) {
  setCurrentValue(value);

  let changedWords = words;
  let changedAll = allWords;
  let foundedIndex = changedAll.findIndex(
    (item) => item[0] == words[index][0] && item[1] == words[index][1]
  );

  if (isTranslation) {
    changedWords[index] = [words[index][0], value];
    changedAll[foundedIndex] = [allWords[foundedIndex][0], value];
  } else {
    changedWords[index] = [value, words[index][1]];
    changedAll[foundedIndex] = [value, allWords[foundedIndex][1]];
  }
  setWords(changedWords);
  setAllWords(changedAll);
}
