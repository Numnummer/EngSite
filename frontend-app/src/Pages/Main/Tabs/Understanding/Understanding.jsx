import FourBlockButtons from "../../../../Components/FourBlockButtons/FourBlockButtons.jsx";

export default function Understanding({setLocation}){
    const labels=['Text','Audio','Video']
    const handlers=[text,audio,video]
    return(
        <>
            <FourBlockButtons labels={labels} handlers={handlers}></FourBlockButtons>
        </>
    )

    function text(){
        setLocation('Texts')
    }
    function audio(){

    }
    function video(){

    }
}
