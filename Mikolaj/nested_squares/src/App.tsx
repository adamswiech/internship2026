import { useState } from "react";
import Square from "./components/Square";
import "./App.css";

function App() {
  const [inputValue, setInputValue] = useState<number>(0);

  return (
    <>
      <div className="app-container">
        <div className="picture">
          {Array.from({ length: 2 }, (_, i) => (
            <Square key={i} inputValue={inputValue} ileMniejszy={i*50}></Square>
          ))}
        </div>

        <div>
          <label htmlFor="">
            <p>Rotate:</p>
            <input
              type="number"
              max={100}
              value={inputValue}
              onChange={(e) => {
                setInputValue(Number(e.target.value));
              }}
            />
          </label>
          <input
            type="range"
            name=""
            id=""
            min={0}
            max={360}
            value={inputValue}
            defaultValue={0}
            onChange={(e) => setInputValue(Number(e.target.value))}
          />
        </div>
      </div>
    </>
  );
}

export default App;
