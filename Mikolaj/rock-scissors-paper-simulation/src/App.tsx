import { useEffect, useRef, useState } from "react";
import Entity from "./components/entity/Entity";

function App() {
  const entityRef = useRef(null);
  const [tmp, setTmp] = useState(false);
  const delay = 100;

  useEffect(() => {
    if (entityRef.current.style.marginTop == "") {
      entityRef.current.style.marginTop = "0px";
      entityRef.current.style.marginLeft = "0px";
    }

    // const velocity = Math.floor(Math.random() * (5 - 1) + 1);
    const velocity = 5;

    console.log(velocity);

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

      if (entityRef.current.up) {
        entityRef.current.style.marginTop = `${entityVector[1] - velocity}px`;
        // entityRef.current.style.marginLeft = `${entityVector[0] - velocity}px`;
      } else {
        entityRef.current.style.marginTop = `${entityVector[1] + velocity}px`;
        // entityRef.current.style.marginLeft = `${entityVector[0] + velocity}px`;
      }

      if (entityRef.current.left) {
        // entityRef.current.style.marginTop = `${entityVector[1] + velocity}px`;
        entityRef.current.style.marginLeft = `${entityVector[0] + velocity}px`;
      } 

      if (entityRef.current.right) {
                entityRef.current.style.marginLeft = `${entityVector[0] - velocity}px`;
      }
      // console.log(
      //   `entityRef.current.style.marginTop = ${entityRef.current.style.marginTop}, entityRef.current.style.marginLeft = ${entityRef.current.style.marginLeft}`,
      // );

        entityRef.current.style.left = `${entityRef.current.x + distanceX}px`;
        entityRef.current.style.top = `${entityRef.current.y + distanceY}px`;

        entityRef.current.i++;
      }

      if (parseInt(entityRef.current.x) <= 0) {
        entityRef.current.left = true;
        entityRef.current.right = false;
      }else if (parseInt(entityRef.current.x) >= 980) {
        entityRef.current.left = false;
        entityRef.current.right = true;
      }

      for (let i = 0; i < 100; i++) {
        console.log("entityRef.current.x = " + entityRef.current.x);
        console.log("entityRef.current.y = " + entityRef.current.y);
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
