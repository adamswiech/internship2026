import { useEffect, useRef, useState } from "react";
import Entity from "./components/entity/Entity";

function App() {
  const entityRef = useRef(null);
  const [tmp, setTmp] = useState(false);
  const delay = 100;

  useEffect(() => {
    if (entityRef.current.style.marginTop == "") {
      entityRef.current.style.left = `${15}px`; //start position x
      entityRef.current.style.top = `${15}px`; //start position y
    }

    const stopInterval = () => {
      console.log(`x = ${entityRef.current.x}`);
      console.log(`y = ${entityRef.current.y}`);
      clearInterval(interval);
    };

    const interval = setInterval(() => {
      let distanceX = 5;
      let distanceY = 5;

      entityRef.current.x = parseInt(entityRef.current.style.left);
      entityRef.current.y = parseInt(entityRef.current.style.top);

      const entityVector = [entityRef.current.x, entityRef.current.y];

      entityRef.current.i = 0;

      
      while (entityRef.current.i != 10) {
        if (parseInt(entityRef.current.x) < 10) {
          //left wall

          distanceX = -distanceX;
          
        } else if (parseInt(entityRef.current.x) > 980) {
          distanceX = -distanceX;
          //right wall
        } else if (parseInt(entityRef.current.y) > 480) {
          // console.log("ighfuiodhguhdrui")
          distanceY = -30;
          break;
          
          //bottom wall
        } else if (parseInt(entityRef.current.y) <= 10) {
          //top wall
          distanceY = -distanceY;
        }

        console.log(`entityRef.current.style.marginLeft = ${entityRef.current.style.marginLeft}, entityRef.current.style.marginTop = ${entityRef.current.style.marginTop}`)

        entityRef.current.style.left = `${entityRef.current.x + distanceX}px`;
        entityRef.current.style.top = `${entityRef.current.y + distanceY}px`;

        entityRef.current.i++;
      }
    }, delay)
  }, []);

  return (
    <>
      <div className="app-container">
        <h2>rock-scissors-paper simulation</h2>
        <div className="game-board">
          <Entity role="🪨" entityRef={entityRef} />
        </div>
      </div>
    </>
  );
}

export default App;
