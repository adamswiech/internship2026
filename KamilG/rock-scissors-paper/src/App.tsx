import { useEffect, useState } from 'react'
import './App.css'

function App() {
  const A = 120;
  const rad = A*(Math.PI/180)
  // const rad = Math.PI/2
  console.log(rad)
  const [point, setPoint] = useState([{ x: Math.cos(rad), y: Math.sin(rad), rad}])
  let t = point[0].x
  let d = point[0].y;
  console.log("st")
  console.log(point)
  for (let i=0; i<5; i++){
    console.log(i)
    console.log(point)
    t = t*2
    d = d*2 
    console.log(t)
    console.log(d)
    // debugger
  }
  console.log(point)
  useEffect(()=>{
      document.getElementById('p1')?.style.setProperty('margin-left', t + 'px')
      document.getElementById('p1')?.style.setProperty('margin-top', d + 'px')
      
  },
  [point])
  return (
    <>
      <div className='te' id='p1'>
        
      </div>
      {/* <div className='me'></div>
      <div className="pe"></div> */}
    </>
  )
}

export default App
