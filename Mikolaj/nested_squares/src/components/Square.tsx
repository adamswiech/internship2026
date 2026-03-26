import { useState } from "react";

interface Square {
    inputValue: number,
    ileMniejszy: number
};

export default function Square({inputValue, ileMniejszy}:Square) {
    const [verticies, setVerticies] = useState({A: 0, B: 0, C: 0});
    // console.log(ileMniejszy);
    
    
    

  return (
      <svg className="square" width={450 - Number(ileMniejszy)} height={550 - Number(ileMniejszy)} xmlns="http://www.w3.org/2000/svg">





        <line x1={0} y1={0} x2={450 - ileMniejszy} y2={0} stroke="black" strokeWidth={1} onClick={() => {console.log(ileMniejszy)}}/> {/*TOP*/}


        <line x1={0}  y1={550 - ileMniejszy} x2={450 - ileMniejszy} y2={550 - ileMniejszy} stroke="black" strokeWidth="1"/> {/*BOTTOM */}


        <line x1="0"  y1="0"  x2="0"  y2={550 - ileMniejszy} stroke="black" strokeWidth="1"/> {/*LEFT*/}


        <line x1={450 - ileMniejszy} y1="0"  x2={450 - ileMniejszy} y2={550 - ileMniejszy} stroke="black" strokeWidth="1"/> {/*RIGHT*/}






        
      </svg>
  );
}
