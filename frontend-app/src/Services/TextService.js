export default function selectTextsByBeginning(texts,beginning){
    return texts.filter((text) => text.startsWith(beginning));
}