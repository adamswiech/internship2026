import { useState } from 'react';
import './App.css';

function App() {
  const [steps, setSteps] = useState(10);
  const initialWidth = 399;
  const initialHeight = 400;

  const Box = ({
    step,
    width,
    height,
    angle =2,
  }: {
    step: number;
    width: number;
    height: number;
    angle?: number;
  }) => {
    if (step <= 0) return null;

    const nextSize = getInnerBoxSize(width, height, angle);

    return (
      <div
        className="box"
        style={{
          width: width,
          height: height,
          transform: step === steps ? 'rotate(0deg)' : `rotate(${angle}deg)`,
        }}
      >
        <div
          style={{
            position: 'absolute',
            top: '50%',
            left: '50%',
            transform: `translate(-50%, -50%)`,
          }}
        >
          <Box step={step - 1} width={nextSize.width} height={nextSize.height} angle={angle} />
        </div>
      </div>
    );
  };

  return (
    <div className="app-container">
      <div className="pic">
        <Box step={steps} width={initialWidth} height={initialHeight} />
      </div>

      <input
        type="range"
        min="1"
        max="100"
        value={steps}
        onChange={(e) => setSteps(Number(e.target.value))}
      />
    </div>
  );
}

function getInnerBoxSize(W: number, H: number, angle: number) {
  const rad = (angle * Math.PI) / 180;

  const sin = Math.sin(rad);
  const cos = Math.cos(rad);

  const width = (W * cos - H * sin) / (cos ** 2 - sin ** 2);
  const height = (H * cos - W * sin) / (cos ** 2 - sin ** 2);

  return {
    width: Math.abs(width),
    height: Math.abs(height),
  };
}

export default App;