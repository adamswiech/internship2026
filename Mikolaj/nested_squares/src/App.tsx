import { useState } from "react";
import "./App.css";

function App() {
  const [inputValue, setInputValue] = useState<number>(0);
  return (
    <>
      <div className="app-container">
        <div className="picture">
          {Array.from({ length: 20 }, (_, i) => (
            <div
              key={i}
              className="square"
              style={{ transform: `rotate(${inputValue + (i+1) * inputValue}deg)`, width: 15*(i+1), height: 25*(i+1) }}
            ></div>
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
            max={100}
            defaultValue={0}
            onChange={(e) => setInputValue(Number(e.target.value))}
          />
        </div>
      </div>
    </>
  );
}

export default App;
