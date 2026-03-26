interface Point {
  x: number;
  y: number;
}

function createNewPoints(points: Point[], l: number): Point[] {
  let newPoints: Point[] = [];
  for (let i = 0; i < 4; i++) {
    let A = points[i];
    let B = points[(i + 1) % 4];
    let X = A.x - B.x;
    let Y = A.y - B.y;
    let R = Math.sqrt(X * X + Y * Y);

    let r = R * l;
    let rad = Math.atan2(Y, X);
    let y = r * Math.sin(rad);
    let x = r * Math.cos(rad);

    newPoints.push({ x: x + B.x, y: y + B.y });
  }

  return newPoints;
}

function Box(props: { counter: number; l: number; points?: Point[] }) {
  const l = props.l;
  const punkty: Point[] = [
    { x: 0, y: 0 },
    { x: 50, y: 0 },
    { x: 50, y: 100 },
    { x: 0, y: 100 },
  ];
  const currentPoints = props.points || punkty;
  const nowePunkty = createNewPoints(currentPoints, l);

  if (props.counter <= 0) return <></>;

  const pointsString = currentPoints.map((p) => `${p.x},${p.y}`).join(" ");

  return (
    <svg width="200" height="200" viewBox="0 0 200 200">
      <polygon
        points={pointsString}
        fill="none"
        stroke="black"
        strokeWidth="1"
      />
      <Box counter={props.counter - 1} points={nowePunkty} l={l} />
    </svg>
  );
}

export default Box;
