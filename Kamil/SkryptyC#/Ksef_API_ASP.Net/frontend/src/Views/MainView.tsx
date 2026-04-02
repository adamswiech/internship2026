import React, { useEffect, useState } from "react";
import { Faktura } from "../Models/API/Faktura";
import Table from "../Components/Table";
import { Podmiot } from "../Models/API/Podmiot";
import { FaWiersz } from "../Models/API/FaWiersz";
import { convertCompilerOptionsFromJson } from "typescript";

const MainView = ()=>{
  const [data, setData] = useState<Faktura[]>([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);


  useEffect(() => {
    const fetchFaktura = async () => {
      try {
          const response  = await fetch("http://localhost:5058/Faktura/GetFaktura");
          
          if (!response.ok) {
          throw new Error("Network response was not ok");
        }

       
        const rawResult = await response.json() as Faktura[];
        const result: Faktura[] = rawResult;
        setData(result);
      } finally {
        setLoading(false);
      }
    };
    fetchFaktura();
  }, []);

  if (loading) return <p>Loading...</p>;
  if (error) return <p>Error: {error}</p>;
  return (
    <div>
      <h2>Faktura Data</h2>
      {data.map((f)=><Table body={f}/>)}
    </div>
  );
}
export default MainView;