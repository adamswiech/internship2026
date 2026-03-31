import { useEffect, useState } from 'react'
import './App.css'

type Faktura = {
  id: number
  podmiot1Id: number
  podmiot2Id: number
  kodWaluty: string
  p_1: string
  p_2: string
  p_6Od: string | null
  p_6Do: string | null
  p_13_1: number
  p_14_1: number
  p_14W: number
  p_15: number
  wierszeCount: number
}

function App() {
  const [faktury, setFaktury] = useState<Faktura[]>([])
  const [loading, setLoading] = useState(true)
  const [error, setError] = useState<string | null>(null)

  useEffect(() => {
    const loadFaktury = async () => {
      try {
        const response = await fetch('/api/faktury')
        if (!response.ok) {
          throw new Error(`HTTP ${response.status}`)
        }

        const data = (await response.json()) as Faktura[]
        setFaktury(data)
      } catch (err) {
        setError(err instanceof Error ? err.message : 'Nieznany b³¹d')
      } finally {
        setLoading(false)
      }
    }

    loadFaktury()
  }, [])

  return (
    <div className="app-container">

      <main className="main-content">
        <section className="card weather-section">
          <div className="section-header">
            <h2 className="section-title">Lista faktur</h2>
          </div>
          {loading && <p>£adowanie danych...</p>}
          {error && <p>{error}</p>}
          {!loading && !error && faktury.length === 0 && <p>Brak faktur do wyœwietlenia.</p>}
          {!loading && !error && faktury.length > 0 && (
            <ul>
              {faktury.map((faktura) => (
                <li key={faktura.id}>
                  <strong>Faktura #{faktura.id}</strong> | Waluta: {faktura.kodWaluty} | Podmioty: {faktura.podmiot1Id}/{faktura.podmiot2Id} | Wiersze: {faktura.wierszeCount}
                </li>
              ))}
            </ul>
          )}
        </section>
      </main>
    </div>
  )
}

export default App
