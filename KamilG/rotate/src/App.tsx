import { useMemo, useState } from 'react'
import './App.css'

const h = 400;
const w = 200;



var l=[];
const kw = [[20,30],[20,170],[80,170],[80,30]];

function lerp (x: number,y: number,t: number) { return x+(y-x)*t; }
function nextkw (t: number, kw: number[][]): number[][] {
  return [
    [lerp(kw[0][0],kw[1][0],t),lerp(kw[0][1],kw[1][1],t)],
    [lerp(kw[1][0],kw[2][0],t),lerp(kw[1][1],kw[2][1],t)],
    [lerp(kw[2][0],kw[3][0],t),lerp(kw[2][1],kw[3][1],t)],
    [lerp(kw[3][0],kw[0][0],t),lerp(kw[3][1],kw[0][1],t)],
  ]
}
const e = nextkw(0.2,kw);
console.log(e);


function App() {
  const [t, setT] = useState(0.01);
  const [count, setCount] = useState(20);

const shapes = useMemo(() => {
  const out: number[][][] = [kw];
  for (let i = 1; i < count; i++) {
    out.push(nextkw(t, out[i - 1]));
  }
  return out;
}, [t, count]);

  return (
    <>
      <input type="range" min="0" max="1" step="0.01" value={t} onChange={(e) => setT(parseFloat(e.target.value))}/>
        <svg width={1600} height={800} viewBox={`0 0 ${w} ${h}`} >
          {/* <rect x={rectX} y={rectY} width={rectW} height={rectH} fill="none" stroke="black" /> */}
          {shapes.map((p, i) => (
          <polygon
            key={i}
            points={p.map(([x, y]) => `${x},${y}`).join(' ')}
            fill="none"
            stroke="black"
            strokeWidth={1}
            opacity={0.25 + 0.75 * (i / Math.max(1, shapes.length - 1))}
          />
          ))}
        </svg>
    </>
  )
}

export default App
