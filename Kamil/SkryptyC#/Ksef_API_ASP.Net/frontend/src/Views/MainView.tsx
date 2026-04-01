import React, { useEffect, useState } from "react";
import { Faktura } from "../Models/Faktura";
import Table from "../Components/Table";
import { Podmiot } from "../Models/Podmiot";
import { FaWiersz } from "../Models/FaWiersz";
import { convertCompilerOptionsFromJson } from "typescript";

const MainView = ()=>{
  const [data, setData] = useState<Faktura[]>([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);

   const mapPodmiot = (raw: Podmiot): Podmiot => ({
            Id: raw.Id,
            Nip: raw.Nip,
            Nazwa: raw.Nazwa,
            KodKraju: raw.KodKraju,
            AdresL1: raw.AdresL1
        })

        const mapFaWiersz = (raw: FaWiersz): FaWiersz => ({
            NrWiersza: raw.NrWiersza,
            KursWaluty: raw.KursWaluty,
            P_7: raw.P_7,
            P_8A: raw.P_8A,
            P_8B: raw.P_8B,
            P_9A: raw.P_9A,
            P_11: raw.P_11,
            P_12: raw.P_12
        })

        
        const mapFaktura = (raw: Faktura): Faktura => ({
            Id: raw.Id,
            Podmiot1: mapPodmiot(raw.Podmiot1),
            Podmiot2: mapPodmiot(raw.Podmiot2),
            KodWaluty: raw.KodWaluty,
            P_1: new Date(raw.P_1),
            P_2: raw.P_2,
            P_6_Od: new Date(raw.P_6_Od),
            P_6_Do: new Date(raw.P_6_Do),
            P_13_1: raw.P_13_1,
            P_14_1: raw.P_14_1,
            P_14_W: raw.P_14_W,
            P_15: raw.P_15,
            FaWiersze: Array.isArray(raw.FaWiersze) ? raw.FaWiersze.map(mapFaWiersz) : []
        })

  useEffect(() => {
    const fetchFaktura = async () => {
      try {
          const response  = await fetch("http://localhost:5058/Faktura/GetFaktura");
          
          if (!response.ok) {
          throw new Error("Network response was not ok");
        }

       
        const rawResult = await response.json() as Faktura[];
        const result: Faktura[] = rawResult.map((e)=>mapFaktura(e));
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