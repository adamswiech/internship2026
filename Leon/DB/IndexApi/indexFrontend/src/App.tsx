import { useState, useEffect } from 'react'
import './App.css'
import type { Person } from './Interfaces/people';
import { getPeople } from './api/peopleApi';


export default function App() {
  const [data, setData] = useState<Person[]>([]);
  const [totalPages, setTotalPages] = useState(0);
  const [page, setPage] = useState(1);
  const [firstName, setFirstName] = useState("");
  const [lastName, setLastName] = useState("");
  const [loading, setLoading] = useState(false);
  const pageSize = 20;


  const fetchData = async () => {
    setLoading(true);
    try {
      const res = await getPeople({
        page,
        pageSize,
        firstName: firstName || undefined,
        lastName: lastName || undefined,
      });
      setData(res.items);
      setTotalPages(res.totalPages);
      console.log(res.totalPages);
    } finally {
      setLoading(false);
    }
  };
  useEffect(() => {
    fetchData();
  }, [page]);

  const handleSearch = () => {
    setPage(1);
    fetchData();
  };


  return (
    <div className='bod'>
      <h2>People Table</h2>
      <div>
        <input
          placeholder="First name"
          value={firstName}
          onChange={(e) => setFirstName(e.target.value)}
        />
        <input
          placeholder="Last name"
          value={lastName}
          onChange={(e) => setLastName(e.target.value)}
        />
        <button onClick={handleSearch}>
          Search
        </button>
      </div>
      {loading ? <p>Loading...</p> : null}
      <table>
        <thead>
          <tr>
            <th>ID</th>
            <th>First Name</th>
            <th>Middle Name</th>
            <th>Last Name</th>
            <th>Age</th>
            <th>Heigh</th>
            <th>eight</th>
            <th>City</th>
            <th>Country</th>
            <th>Fav number</th>
          </tr>
        </thead>

        <tbody>
          {data.map((p) => (
            <tr key={p.id}>
              <td>{p.id}</td>
              <td>{p.first_name}</td>
              <td>{p.middle_name}</td>
              <td>{p.last_name}</td>
              <td>{p.age}</td>
              <td>{p.height_cm}</td>
              <td>{p.weight_kg}</td>
              <td>{p.city}</td>
              <td>{p.country}</td>
              <td>{p.favorite_number}</td>
            </tr>
          ))}
        </tbody>
      </table>
      <div>
        <button disabled={page === 1} onClick={() => setPage(page - 1)}>
          Prev
        </button>

        <span>
          Page {page} / {totalPages}
        </span>

        <button
          disabled={page === totalPages}
          onClick={() => setPage(page + 1)}
        >
          Next
        </button>
      </div>
    </div>
  );
}
