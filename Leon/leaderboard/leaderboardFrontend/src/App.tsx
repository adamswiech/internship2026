import { useState, useEffect } from 'react';
import { addScore, getTop10 } from './api/leaderboardApi';
import type { Top10 } from './interfaces/interfaces';
import './App.css'
import Top10L from './components/top10';

function App() {
const [username, setUsername] = useState('');
const [score, setScore] = useState(0);
const [time, setTime] = useState('');
const [gameMode, setGameMode] = useState('');
const [top10, setTop10] = useState<Top10[]>([]);
const [loading, setLoading] = useState(false);
useEffect(() => {
    fetchTop10();
}, []);

const fetchTop10 = async () => {
    setLoading(true);
    try {
    const data = await getTop10();
        setTop10(data);
    } catch (error) {
    console.error('Error fetching top 10:', error);
        setTop10([]);
    } finally {
        setLoading(false);
    }
};

const handleAddScore = async () => {
    try {
    const params = {
        username,
        score: Number(score),
        time,
        gameMode,
    };
    const result = await addScore(params);
        console.log('Add score result:', result);
    await fetchTop10();
    } catch (error) {
        console.error('Error adding score:', error);
    }
};

return (
    <div className='app-container'>
    <input className='usernameI' value={username} onChange={(e) => setUsername(e.target.value)} placeholder="Username"/>
    <input className='scoreI' type="number" value={score} onChange={(e) => setScore(Number(e.target.value))} placeholder="Score"/>
    <input className='timeI' value={time} onChange={(e) => setTime(e.target.value)} placeholder="Time (e.g., 10:10:10)"/>
    <input className='gamemodeI' value={gameMode} onChange={(e) => setGameMode(e.target.value)} placeholder="Game Mode"/>
    <button onClick={handleAddScore}>Add Score</button>
    {loading ? <p>Loading leaderboard...</p> : <Top10L top10={top10} />}
    </div>
)
}

export default App
