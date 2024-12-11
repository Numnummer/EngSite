import styles from './TextRecord.module.css'
export default function TextRecord({textName, handler}){
    return (
        <div className={styles.record} onClick={()=>handler(textName)}>
            <label className={styles.text}>{textName}</label>
        </div>
    )
}