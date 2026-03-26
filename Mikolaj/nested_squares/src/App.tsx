import { useState } from "react";
import "./App.css";
import Square from "./components/Square";

function App() {
  const [larp, setLarp] = useState(0.1);
  const [counter, setCounter] = useState(5);

  const squarePoints = [
    { x: 0, y: 0 },
    { x: 200, y: 0 },
    { x: 200, y: 200 },
    { x: 0, y: 200 },
  ];

  return (
    <section>
      <Square counter={counter} points={squarePoints} l={larp} />

      <label htmlFor="">
        larp
        <input
          type="range"
          min="0"
          max="1"
          step="0.001"
          value={larp}
          onChange={(e) => setLarp(parseFloat(e.target.value))}
        />
        <p>larp = {larp}</p>
      </label>

      <label htmlFor="">
        depth
        <input
          type="range"
          min="1"
          max="100"
          step="1"
          value={counter}
          onChange={(e) => setCounter(parseInt(e.target.value))}
        />
        <p>depth = {counter}</p>
      </label>
    </section>
  );
}

export default App;
