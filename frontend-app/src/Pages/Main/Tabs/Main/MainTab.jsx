import FourBlockButtons from "../../../../Components/FourBlockButtons/FourBlockButtons.jsx";

export default function MainTab({setLocation}){
    function wordsHandler(){
        setLocation('Words')
    }
    function grammarHandler(){
        setLocation('Grammar')
    }
    function understandingHandler(){
        setLocation('Understanding')
    }
    function speakingHandler(){
        setLocation('Words')
    }

    const labels=['Words','Grammar','Understanding','Speaking']
    const handlers=[wordsHandler,grammarHandler,understandingHandler,speakingHandler]
    return(
        <>
            <FourBlockButtons labels={labels} handlers={handlers}></FourBlockButtons>
        </>
    )
}

