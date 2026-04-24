import { use, useState } from "react";
import "./App.css";
import Box from "./component/Box";

function App() {
  const [l, setL] = useState(0.5); 
  const [counter, setCounter] = useState(10); 

  const squarePoints = [
    { x: 0, y: 0 },
    { x: 200, y: 0 },
    { x: 200, y: 200 },
    { x: 0, y: 200 }
  ];
    const rectanglePoints = [
    { x: 25, y: 0 },
    { x: 175, y: 0 },
    { x: 175, y: 200 },
    { x: 25, y: 200 }
  ];

  return (
    <section>
      <p>depth = {counter}</p>
      <p>l = {l}</p>
      <Box counter={counter} points={rectanglePoints} l={l} />
      <label htmlFor=""> 
        larp
        <input type="range" min="0" max="1" step="0.001" value={l} onChange={(e) => setL(parseFloat(e.target.value))} />
      </label>
      <label htmlFor=""> 
        depth
        <input type="range" min="1" max="100" step="1" value={counter} onChange={(e) => setCounter(parseInt(e.target.value))} />
      </label>
    </section>
  );
}

export default App;