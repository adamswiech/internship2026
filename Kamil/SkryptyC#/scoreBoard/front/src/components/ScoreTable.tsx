import React, { useState, useEffect } from 'react';
import Table from './Table';
import Inserter from './Inserter';
const ScoreTable = () => {
  const [scores, setScores] = useState([]);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    fetchScores();
  }, []);

  const fetchScores = async () => {
    try {
      const response = await fetch('https://localhost:7030/api/Score');
      
      if (!response.ok) {
        throw new Error(`HTTP error! Status: ${response.status}`);
      }

      const data = await response.json();
      setScores(data);
    } catch (err) {
      console.error('Failed to fetch scores:', err);
    } finally {
      setLoading(false);
    }
  };

  const handleRefresh = () => {
    fetchScores();
  };


  if (loading) return <p>Loading scores...</p>;

  return (
    <div className='view'>
      <Inserter onSubmit={fetchScores}/>
      <Table body={scores}/>
    </div>
  );
};
export default ScoreTable