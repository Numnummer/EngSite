import translateSentenceAsync, {translateRussianSentenceAsync} from "../API/Words/TranslateSentence.js";
import translateEnglishSentenceAsync from "../API/Words/TranslateSentence.js";

export default function selectWordsByBeginning(words,beginning){
    return words.filter(([word, translation]) => word.startsWith(beginning));
}
export function removeEmptyItems(words){
    return words.filter(([word, translation])=>word!=='' || translation!=='')
}
export function removeDublicates(words){
    return words.filter((item, index, self) => {
        const firstIndex = self.findIndex((prevItem) => prevItem.word === item.word);
        return firstIndex === index;
    });
}

export async function translateUntranslatedAsync(words){
    return await Promise.all(
        words.map(async ([word, translation]) => {
            if (translation === '') {
                translation = await translateEnglishSentenceAsync(word)
            }
            else if(word === ''){
                word=await translateRussianSentenceAsync(translation)
            }
            return [word, translation];
        })
    );
}

export function mapWordsTranslation(wordsForMap,words){
    const getTranslation=(wordToFind)=>{
        return words.find(([word,translation])=>word===wordToFind)[1]
    }
    const getWord=(translationToFind)=>{
        return words.find(([word,translation])=>translation===translationToFind)[0]
    }
    return wordsForMap.map(([word, translation]) => {
        if (translation === '') {
            translation = getTranslation(word)
        }
        if (word === ''){
            word = getWord(translation)
        }
        return [word, translation];
    });
}