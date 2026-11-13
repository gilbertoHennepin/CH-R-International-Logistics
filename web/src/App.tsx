import { useState, type FormEvent } from 'react'
import './App.css'

type RouteResponse = { destination: string; list: string[] }

const API_URL = import.meta.env.VITE_API_URL

function App() {
  const [code, setCode] = useState('')
  const [route, setRoute] = useState<string[] | null>(null)
  const [error, setError] = useState<string | null>(null)
  const [loading, setLoading] = useState(false)

  async function handleSubmit(e: FormEvent<HTMLFormElement>) {
    e.preventDefault()            // stop the browser from reloading the page
    setLoading(true)
    setError(null)
    setRoute(null)
    try {
      const res = await fetch(`${API_URL}/${code}`)
      if (res.status === 404) throw new Error(`${code} is not a supported North American country code.`)
      if (!res.ok) throw new Error('Something went wrong. Please try again.')
      const data: RouteResponse = await res.json()
      setRoute(data.list)
    } catch (err) {
      setError(err instanceof Error ? err.message : 'Could not reach the server.')
    } finally {
      setLoading(false)
    }
  }

  return (
    <main className="card">
      <h1>Customs Route Finder</h1>
      <p>Enter a 3-letter North American country code to see every country a shipment passes through from the USA.</p>

      <form onSubmit={handleSubmit}>
        <label htmlFor="code">Country code</label>
        <input
          id="code"
          value={code}
          onChange={(e) => setCode(e.target.value.toUpperCase())}
          maxLength={3}
          pattern="[A-Za-z]{3}"
          placeholder="USA"
          autoComplete="off"
          required
        />
        <button type="submit" disabled={loading}>{loading ? 'Finding…' : 'Find route'}</button>
      </form>

      <div aria-live="polite">
        {error && <p className="error">{error}</p>}
        {route && (
          <ol>
            {route.map((country) => <li key={country}>{country}</li>)}
          </ol>
        )}
      </div>
    </main>
  )
}

export default App
