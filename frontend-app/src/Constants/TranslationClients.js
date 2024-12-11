import axios from "axios";

export const enRuTranslationClient=axios.create({
    baseURL:"https://translate.googleapis.com/translate_a/single?client=gtx&sl=en&tl=ru&dt=t&q="
})

export const ruEnTranslationClient=axios.create({
    baseURL:"https://translate.googleapis.com/translate_a/single?client=gtx&sl=ru&tl=en&dt=t&q="
})