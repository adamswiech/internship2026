import { useState } from "react";

const Inserter = (props : {onSubmit : () => Promise<void>})=>{
    const [score,setScore] = useState(200);
    const [name,setName] = useState("user");
    
    const submitScore = async () => {
        try {
            const data = {
                Points: score,
                PlayerName: name,
                
            }
            const response = await fetch('https://localhost:7030/api/Score/submitScore',{
                method: 'POST',                    
                headers: {
                    'Content-Type': 'application/json',  
                },
                body: JSON.stringify(data),
            });
        
            if (!response.ok) {
                throw new Error(`HTTP error! Status: ${response.status}`);
            }

        } catch (err) {
            console.error('Failed to fetch scores:', err);
        } finally {
            props.onSubmit();
        }
    };


    const handleSubmit = () =>{
        submitScore();
    }
    return(
        <div className="inserter">

            <label>
                Name: 
                <input type="text" value={name} onChange={(e)=>setName(e.target.value)}/>
            </label>
            <label>
                Points: 
                <input type="number" value={score} onChange={(e)=>setScore(e.target.value as any)}/>
            </label>
            <button onClick={handleSubmit}>Submit Score</button>
        </div>
    );
}

export default Inserter;