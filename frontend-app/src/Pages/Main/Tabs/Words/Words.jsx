import FourBlockButtons from "../../../../Components/FourBlockButtons/FourBlockButtons.jsx";
import { wordsPracticeType } from "../../../../Constants/LocalStorageKeys.js";
import sendWordsPracticeOption from "/src/API/Words/SendWordsPracticeOption.js";

export default function Words({ setLocation }) {
  function myDictionary() {
    setLocation("MyDictionary");
  }
  function dailyWord() {
    setLocation("DailyWord");
  }
  async function practiceMyDictionary() {
    localStorage.setItem(wordsPracticeType, "UserDictionary");
    setLocation("WordsPractice");
  }
  async function practiceRandom() {
    localStorage.setItem(wordsPracticeType, "Random");
    setLocation("WordsPractice");
  }

  const labels = [
    "My Dictionary",
    "Daily Word",
    "Practice My Dictionary",
    "Practice Random",
  ];
  const handlers = [
    myDictionary,
    dailyWord,
    practiceMyDictionary,
    practiceRandom,
  ];
  return (
    <>
      <FourBlockButtons labels={labels} handlers={handlers}></FourBlockButtons>
    </>
  );
}
