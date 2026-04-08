import { useEffect, useState } from "react";
import Table from "../Components/Table";
import { FakturaApi } from "../API/FakturaApi";
import { FakturaDTO } from "../Models/API/FakturaDTO";

const FakturaListView = ()=>{
  const [data, setData] = useState<FakturaDTO[]>([]);
  const [loading, setLoading] = useState(true);
  FakturaApi.GetFaktura()
    .then(e => setData(e));


  useEffect(() => {
  }, []);

  if (loading) return <p>Loading...</p>;
  return (
    <div>
      {data.map((f)=><Table body={f}/>)}
    </div>
  );
}
export default FakturaListView;