import styles from './Search.module.css'
import image from '/public/images/search-icon.png'
import {useState} from "react";
export default function Search({onSearch}){
    const [input,setInput]=useState('')
    return(
        <div className={styles.searchBox}>
            <input className={styles.searchInput} onChange={(event)=>{
                setInput(event.target.value)
            }}/>
            <img src={image} className={styles.image} onClick={()=>onSearch(input)}/>
        </div>
    )
}