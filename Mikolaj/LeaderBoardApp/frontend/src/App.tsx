import Api from "../scripts/api";
import { useState } from "react";

function App() {
  const [playerId, setPlayerId] = useState("");
  const [score, setScore] = useState(0);
  const [gameMode, setGameMode] = useState("");

  const handleUploadData = async () => {
    const response =  await Api.scores(playerId, score, gameMode);

    console.log(response);
  };

  return <>
    <input type="text" placeholder="Player id" onChange={(e) => setPlayerId(e.target.value)}/>
    <input type="number" placeholder="Score" onChange={(e) => setScore(parseInt(e.target.value))}/>
    <input type="text" placeholder="Game mode" onChange={(e) => setGameMode(e.target.value)}/>

    <button onClick={handleUploadData}>Submit data</button>
  </>;
}

export default App;
