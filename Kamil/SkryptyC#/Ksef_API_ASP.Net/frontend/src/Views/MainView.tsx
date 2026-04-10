import { useState } from "react";
import FakturaListView from "./FakturaListView";
import SentFakturaView from "./SentFakturaView";

const MainView = ()=>{
  const [view,setView] = useState(<FakturaListView/>);
  
  return (
    <div>
      <nav>
        <button onClick={()=> setView(<FakturaListView/>)}>Faktury</button>
        <button onClick={()=> setView(<SentFakturaView/>)}>Sent Faktura</button>
      </nav>
        {view}
    </div>
  );
}
export default MainView;