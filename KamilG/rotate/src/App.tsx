import { useMemo, useState } from 'react'
import './App.css'

type Point = {
  x: number
  y: number
}

type Quad = [Point, Point, Point, Point]

function lerp(a: Point, b: Point, t: number): Point {
  return {
    x: a.x + (b.x - a.x) * t,
    y: a.y + (b.y - a.y) * t,
  }
}

function nextQuad([a, b, c, d]: Quad, t: number): Quad {
  return [
    lerp(a, b, t),
    lerp(b, c, t),
    lerp(c, d, t),
    lerp(d, a, t),
  ]
}

function generateSpiral(width: number, height: number, steps: number, t: number): Quad[] {
  const result: Quad[] = []
  let current: Quad = [
    { x: 0, y: 0 },
    { x: width, y: 0 },
    { x: width, y: height },
    { x: 0, y: height },
  ]

  result.push(current)

  for (let i = 0; i < steps; i += 1) {
    current = nextQuad(current, t)
    result.push(current)
  }

  return result
}

function App() {
  const [ratio, setRatio] = useState(0.12)
  const [steps, setSteps] = useState(64)

  const width = 360
  const height = 520

  const quads = useMemo(() => generateSpiral(width, height, steps, ratio), [height, ratio, steps, width])

  return (
    <main className="rotation-page">
      <div className="stage" aria-label="Spirala z prostokata">
        <svg viewBox={`0 0 ${width} ${height}`} className="spiral-svg" role="img" aria-hidden="true">
          {quads.map((quad, index) => {
            const points = [...quad, quad[0]].map((point) => `${point.x},${point.y}`).join(' ')
            const alpha = 0.9 - (index / quads.length) * 0.6

            return (
              <polyline
                key={index}
                points={points}
                fill="none"
                stroke="rgba(17, 17, 17, 1)"
                strokeOpacity={alpha}
                strokeWidth="1.2"
              />
            )
          })}
        </svg>
      </div>

      <label className="control">
        t (przesuniecie na bokach): {ratio.toFixed(2)}
        <input
          type="range"
          min="0.02"
          max="0.35"
          step="0.01"
          value={ratio}
          onChange={(event) => setRatio(Number(event.currentTarget.value))}
        />
      </label>

      <label className="control">
        liczba warstw: {steps}
        <input
          type="range"
          min="12"
          max="120"
          step="1"
          value={steps}
          onChange={(event) => setSteps(Number(event.currentTarget.value))}
        />
      </label>
    </main>
  )
}

export default App
