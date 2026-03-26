// import { useState } from 'react';
// import './App.css';

// function App() {
//   const [steps, setSteps] = useState(10);
//   const initialWidth = 400;
//   const initialHeight = 400;

//   const Box = ({
//     step,
//     width,
//     height,
//     angle =5,
//   }: {
//     step: number;
//     width: number;
//     height: number;
//     angle?: number;
//   }) => {
//     if (step <= 0) return null;

//     const nextSize = getInnerBoxSize(width, height, angle);

//     return (
//       <div
//         className="box"
//         style={{
//           width: width,
//           height: height,
//           transform: step === steps ? 'rotate(0deg)' : `rotate(${angle}deg)`,
//         }}
//       >
//         <div
//           style={{
//             position: 'absolute',
//             top: '50%',
//             left: '50%',
//             transform: `translate(-50%, -50%)`,
//           }}
//         >
//           <Box step={step - 1} width={nextSize.width} height={nextSize.height} angle={angle} />
//         </div>
//       </div>
//     );
//   };

//   return (
//     <div className="app-container">
//       <div className="pic">
//         <Box step={steps} width={initialWidth} height={initialHeight} />
//       </div>

//       <input
//         type="range"
//         min="1"
//         max="100"
//         value={steps}
//         onChange={(e) => setSteps(Number(e.target.value))}
//       />
//     </div>
//   );
// }

// function getInnerBoxSize(W: number, H: number, angle: number) {
//   const rad = (angle * Math.PI) / 180;

//   const sin = Math.sin(rad);
//   const cos = Math.cos(rad);

//   const width = (W * cos - H * sin) / (cos ** 2 - sin ** 2);
//   const height = (H * cos - W * sin) / (cos ** 2 - sin ** 2);

//   return {
//     width: Math.abs(width),
//     height: Math.abs(height),
//   };
// }

// export default App;

























 import { useState } from "react";

 type Point = { x: number; y: number };

 function App() {

  const [count, setCount] = useState(10);
  const [ratio, setRatio] = useState(10);


  const initialW = 350;
  const initialH = 400;
  const initialPoints: Point[] = [{x: 0, y:0},{x: 0, y:initialH},{x: initialW, y:initialH},{x: initialW, y:0}]


   var poly = genPol(initialPoints, count, ratio); 

  

   return (
     <>
     <div style={{padding: 20}}>
       <svg height={initialH} width={initialW} style={{border: "1px solid black"}}>
        {poly.map((pol, i)=>(
          <polygon key={i} points={pol.map((p) => `${p.x},${p.y}`).join(" ")} stroke="black" fill="none" strokeWidth={1}></polygon>
        ))}
       </svg>
    
       <input 
       type="range"
        min="1"
        max="100"
        value={count}
        onChange={(e) => setCount(Number(e.target.value))}
       />
       <input 
       type="range"
        min="1"
        max="50"
        value={ratio}
        onChange={(e) => setRatio(Number(e.target.value))}
       />
     </div>
     </>
   );
   function genPol(points: Point[], num: number, rat: number): Point[][] {
    const result: Point[][] = [points];

    let curr = points;

    for (let i = 0; i < num; i++) {
      curr = newPoly(curr, rat);
      result.push(curr);
  }
  return result;
}
  function newPoly(points: Point[], rat: number): Point[]{
    var pol: Point[] = [];
    var t = rat /100;

  for (let i = 0; i < 4; i++) {
    var a = points[i];
    var b = points[(i + 1) % 4];

    pol.push({
      x: lerp(a.x, b.x, t),
      y: lerp(a.y,b.y,t),
    });
  }
  
  return pol;
  }

  function lerp(a: number, b: number, t: number){
    var lerp = a * (1 - t) + b * t;
    return lerp;
  }

 }


export default App;