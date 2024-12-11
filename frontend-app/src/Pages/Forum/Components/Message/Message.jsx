import styles from './Message.module.css'
export default function Message({isSelf, message, author, date}){
    return(
        <div className={isSelf? styles.selfMessageBox:styles.messageBox}>
            <label className={styles.messageText}>{message}</label>
            <hr className={styles.line}/>
            <div className={styles.metaData}>
                <label className={!isSelf && styles.author}>{!isSelf && author}</label>
                <label className={styles.date}>{date}</label>
            </div>
        </div>
    )
}