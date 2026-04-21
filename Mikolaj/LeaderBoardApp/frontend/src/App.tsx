import Api from "../scripts/api";
import { useState, useEffect } from "react";
import type { Player } from "./interfaces/Player";

function App() {
  const [playerId, setPlayerId] = useState("");
  const [score, setScore] = useState(0);
  const [gameMode, setGameMode] = useState("solo");

  const [scoresArr, setScoresArr] = useState<Player[]>([]);
  const [reload, setReload] = useState("");

  const [status, setStatus] = useState("");

  useEffect(() => {
    const getAllScores = async () => {
      const response = await Api.getAllScores();

      if (response.length != 0) {
        setScoresArr(response);
      }
    };

    getAllScores();
    setReload("");
  }, [reload]);

  const handleUploadData = async () => {
    try {
      const response = await Api.uploadScore(playerId, score, gameMode);

      if (response === "Job enqueued.") {
        setReload("reload");
        setStatus("");
      }
    } catch (e: any) {
      setStatus(e.message);
    }
  };

  return (
    <div className="app-container">
      <h1>Game leaderboard top 10</h1>
      <div className="app-box">
        <div className="upload-score-box">
          <div className="input-container">
            <p>Player ID</p>
            <input
              type="text"
              placeholder="Player id"
              onChange={(e) => setPlayerId(e.target.value)}
              value={playerId}
            />
          </div>
          <div className="input-container">
            <p>Score</p>
            <input
              type="number"
              placeholder="Score"
              onChange={(e) => setScore(parseInt(e.target.value))}
              value={score}
            />
          </div>

          <div className="input-container">
            <p>Game Mode</p>
            <select
              value={gameMode}
              onChange={(e) => setGameMode(e.target.value)}
            >
              <option value="solo">solo</option>
              <option value="survival">survival</option>
              <option value="multiplayer">multiplayer</option>
            </select>
          </div>

          <button
            onClick={() => {
              if (gameMode == "" || playerId == "") {
                console.log("Set data to inputs before submit");
                return;
              } else {
                handleUploadData();
                setGameMode("");
                setScore(0);
                setPlayerId("");
              }
            }}
          >
            Submit data
          </button>

          <p>{status}</p>
        </div>

        <div className="table-container">
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
    </div>
  );
}

export default App;
