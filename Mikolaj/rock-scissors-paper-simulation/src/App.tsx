import { useEffect, useRef, useState } from "react";
import Entity from "./components/entity/Entity";

function App() {
  const entityRef = useRef(null);
  const delay = 30;

  useEffect(() => {
    if (entityRef.current.style.marginTop == "") {
      entityRef.current.style.marginTop = "480px";
      entityRef.current.style.marginLeft = "980px";
    }

    const velocity = Math.floor(Math.random() * (5 - 1) + 1);

    console.log(velocity);

    const stopInterval = () => {
      clearInterval(interval);
    };

    entityRef.current.up = true;
    entityRef.current.down = false;

    const interval = setInterval(() => {
      entityRef.current.x = parseInt(entityRef.current.style.marginLeft);
      entityRef.current.y = parseInt(entityRef.current.style.marginTop);

      const entityVector = [entityRef.current.x, entityRef.current.y];

      if (entityRef.current.up) {
        entityRef.current.style.marginTop = `${entityVector[1] - velocity}px`;
        entityRef.current.style.marginLeft = `${entityVector[0] - velocity}px`;
      } else {
        entityRef.current.style.marginTop = `${entityVector[1] + velocity}px`;
        entityRef.current.style.marginLeft = `${entityVector[0] + velocity}px`;
      }

      // console.log(
      //   `entityRef.current.style.marginTop = ${entityRef.current.style.marginTop}, entityRef.current.style.marginLeft = ${entityRef.current.style.marginLeft}`,
      // );

      // console.log(`x = ${entityRef.current.x}`);
      // console.log(`y = ${entityRef.current.y}`);

      if (parseInt(entityRef.current.y) <= 0) {
        entityRef.current.up = false;
        entityRef.current.down = true;
        //based on vector value try to decide if you have to + or - velocity from value of margin
      } else if (parseInt(entityRef.current.y) >= 480) {
        entityRef.current.up = true;
        entityRef.current.down = false;
      }
    }, delay);
    
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
