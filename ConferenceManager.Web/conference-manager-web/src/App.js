import React, { useEffect, useState } from 'react';
import axios from 'axios';

function App() {
  const [conferences, setConferences] = useState([]);

  useEffect(() => {
    // Заміни URL на той, де працює твій API (наприклад, порт 5000 або 5123)
    axios.get('http://localhost:5000/api/v1/Conferences')
      .then(response => {
        setConferences(response.Data || response.data);
      })
      .catch(error => console.error('Помилка при завантаженні:', error));
  }, []);

  return (
    <div style={{ padding: '20px', fontFamily: 'Arial' }}>
      <h1>Conference Manager (React Client)</h1>
      <h2>Список конференцій з БД:</h2>
      <ul>
        {conferences.map(conf => (
          <li key={conf.Id || conf.id}>
            <strong>{conf.Name || conf.name}</strong> — {conf.Location || conf.location}
          </li>
        ))}
      </ul>
    </div>
  );
}

export default App;