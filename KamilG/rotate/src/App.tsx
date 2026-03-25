import { useState } from 'react'
import './App.css'

const MAX_TRACE = 20

function App() {
  const [angle, setAngle] = useState<number>(0)
  const [trace, setTrace] = useState<number[]>([0])

  const handleAngleChange = (value: number) => {
    setAngle(value)

    setTrace((previous) => {
      const next = [...previous, value]

      if (next.length > MAX_TRACE) {
        next.shift()
      }

      return next
    })
  }

  return (
    <main className="rotation-page">
      <div className="stage">
        <div className="rectangle-stack" aria-hidden="true">
          {trace.map((traceAngle, index) => {
            const progress = (index + 1) / trace.length
            const opacity = 0.08 + progress * 0.21

            return (
              <div
                key={`${traceAngle}-${index}`}
                className="rectangle-layer"
                style={{
                  transform: `translate(-50%, -50%) rotate(${traceAngle}deg)`,
                  opacity,
                }}
              />
            )
          })}
        </div>
      </div>

      <input
        type="range"
        min="0"
        max="360"
        step="1"
        value={angle}
        onChange={(event) => handleAngleChange(Number(event.currentTarget.value))}
      />
    </main>
  )
}

export default App
