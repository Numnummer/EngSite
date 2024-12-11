import axios from "axios";

export const randomWordClient = axios.create({
    baseURL: 'https://random-word.ryanrk.com/api/en/word/random'
})