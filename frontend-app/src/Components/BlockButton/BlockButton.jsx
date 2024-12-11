import styles from './BlockButton.module.css'
export default function BlockButton({label, handler}){
    return (
        <div className={styles.block} onClick={()=>{handler()}}>
            <label className={styles.text}>{label}</label>
        </div>
    )
}