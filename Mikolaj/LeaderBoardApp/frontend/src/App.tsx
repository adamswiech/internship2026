import Api from "../scripts/api";
import { useState, useEffect } from "react";
import type { Player } from "./interfaces/Player";

function App() {
  const [playerId, setPlayerId] = useState("");
  const [score, setScore] = useState(0);
  const [gameMode, setGameMode] = useState("");

  const [scoresArr, setScoresArr] = useState<Player[]>([]);
  const [reload, setReload] = useState(false);

  useEffect(() => {
    const getAllScores = async () => {
      const response = await Api.getAllScores();

      if (response.length != 0) {
        setScoresArr(response);
      }
    };

    getAllScores();
  }, [reload]);

  const handleUploadData = async () => {
    const response = await Api.uploadScore(playerId, score, gameMode);

    console.log(response);
  };

  return (
    <div className="app-container">
      <h1>Game leaderboard</h1>
      <div className="app-box">
        <div className="upload-score-box">
          <input
            type="text"
            placeholder="Player id"
            onChange={(e) => setPlayerId(e.target.value)}
            value={playerId}
          />
          <input
            type="number"
            placeholder="Score"
            onChange={(e) => setScore(parseInt(e.target.value))}
            value={score}
          />
          <input
            type="text"
            placeholder="Game mode"
            onChange={(e) => setGameMode(e.target.value)}
            value={gameMode}
          />

          <button
            onClick={() => {
              if (gameMode == "" || score == 0 || playerId == "") {
                console.log("Set data to inputs before submit");
                return;
              } else {
                handleUploadData();
                setGameMode("");
                setScore(0);
                setPlayerId("");

                setReload(!reload);
              }
            }}
          >
            Submit data
          </button>
        </div>

        <table>
          <thead>
            <tr>
              {/* <th>Id</th> */}
              <th>PlayerId</th>
              <th>Score</th>
              <th>GameMode</th>
              <th>Status</th>
            </tr>
          </thead>
          <tbody>
            {scoresArr.length > 0 ? (
              scoresArr.map((item) => (
                <tr key={item.id}>
                  {/* <td>{item.id}</td> */}
                  <td>{item.playerId}</td>
                  <td>{item.score}</td>
                  <td>{item.gameMode}</td>
                  <td>{item.status}</td>
                </tr>
              ))
            ) : (
              <tr>
                <td colSpan={4}>No data to display</td>
              </tr>
            )}
          </tbody>
        </table>
      </div>
    </div>
  );
}

export default App;
