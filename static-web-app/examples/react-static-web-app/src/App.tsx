import { useState } from "react";

import "./App.css";

function App() {
  const [results, setResults] = useState("[]");

  //develpment
  //const apiUrl: string = "http://localhost:5000/api";
  //production app service not linked to static web app
  //const apiUrl: string = 'https://lcs16-swa-as.azurewebsites.net/api';
  //production app service linked to static web app
  const apiUrl: string = "/api";

  const getApi = async () => {
    const endpointUrl = `${apiUrl}/weatherforecast`;
    console.log(endpointUrl);
    const response = await fetch(endpointUrl);
    const data = await response.json();
    setResults(JSON.stringify(data, null, 4));
  };

  return (
    <div className="content" role="main">
      <svg width="100" height="100" xmlns="http://www.w3.org/2000/svg" viewBox="-11.5 -10.23174 23 20.46348">
        <title>React Logo</title>
        <circle cx="0" cy="0" r="2.05" fill="#61dafb" />
        <g stroke="#61dafb" strokeWidth="1" fill="none">
          <ellipse rx="11" ry="4.2" />
          <ellipse rx="11" ry="4.2" transform="rotate(60)" />
          <ellipse rx="11" ry="4.2" transform="rotate(120)" />
        </g>
      </svg>
      <h1>react-static-web-app</h1>
      <p>Test Application</p>
      <br />
      <button onClick={getApi} className="btn btn-primary">
        Get Api
      </button>
      <br />
      <pre>{results}</pre>
    </div>
  );
}

export default App;
