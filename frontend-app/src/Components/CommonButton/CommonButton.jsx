import styles from './CommonButton.module.css'
export default function CommonButton({text, isOrange, handler}){
    return(
        <button
            className={
            isOrange?styles.commonButtonOrange:styles.commonButtonBlue
        }
            onClick={handler}>
            {text}
        </button>
    )
}