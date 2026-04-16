import { useRef, useState } from "react";

interface Entity {
  role: string;
}

export default function Entity({ role }: Entity) {
  const entityRef = useRef(null);
  const delay = 1000;
  const [distance, setDistance] = useState({
    x: Math.random() * (2 - 1) + 1,
    y: Math.random() * (2 - 1) + 1,
  });

  const entityMoveInterval = setInterval(() => {
    entityRef.current.style.left = `${entityRef.current.getClientRects()[0].x + distance.x}px`;
    entityRef.current.style.top = `${entityRef.current.getClientRects()[0].y + distance.y}px`;

    console.log(entityRef.current.style.left);
    console.log(entityRef.current.style.top);

    if (parseInt(entityRef.current.style.left) < 75) {



      clearInterval(entityMoveInterval);
      console.log("test1");


    } else if (parseInt(entityRef.current.style.top) < 55) {
      clearInterval(entityMoveInterval);
      console.log("test1");
    } else if (parseInt(entityRef.current.style.left) > 1459) {
      clearInterval(entityMoveInterval);
      console.log("test1");
    } else if (parseInt(entityRef.current.style.top) > 717) {
      clearInterval(entityMoveInterval);
      console.log("test1");
    }
  }, delay);

  return (
    <div className="entity" id="entity" ref={entityRef}>
      {role}
    </div>
  );
}
