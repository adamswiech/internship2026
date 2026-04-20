import type { Top10 } from '../interfaces/interfaces';

interface Top10LProps {
  top10: Top10[];
}

function Top10L({ top10 }: Top10LProps) {
  if (!top10 || top10.length === 0) {
    return <p>No leaderboard data available.</p>;
  }

  return (
    <div className='leaderboard-table'>
      <table>
        <thead>
          <tr>
            <th>Rank</th>
            <th>Username</th>
            <th>Score</th>
            <th>Time</th>
            <th>Game Mode</th>
            <th>Suspected</th>
          </tr>
        </thead>
        <tbody>
          {top10.map((entry) => (
            <tr key={entry.id}>
              <td>{entry.rank ?? 'N/A'}</td>
              <td>{entry.score?.username ?? '—'}</td>
              <td>{entry.score?.score ?? '—'}</td>
              <td>{entry.score?.time ?? '—'}</td>
              <td>{entry.score?.gameMode ?? '—'}</td>
              <td>{entry.score?.isSuspicious ? 'Yes' : 'No'}</td>
            </tr>
          ))}
        </tbody>
      </table>
    </div>
  )
}

export default Top10L
