import BlockButton from "../BlockButton/BlockButton.jsx";
import styles from './FourBlockButtons.module.css'
export default function FourBlockButtons({labels,handlers}){
    return (
        <div className={styles.block}>
            <div className={styles.row}>
                <BlockButton label={labels[0]} handler={handlers[0]}></BlockButton>
                <BlockButton label={labels[1]} handler={handlers[1]}></BlockButton>
            </div>
            <div className={styles.row}>
                <BlockButton label={labels[2]} handler={handlers[2]}></BlockButton>
                <BlockButton label={labels[3]} handler={handlers[3]}></BlockButton>
            </div>
        </div>
    )
}