import { useState } from "react";
import FakturaListView from "./FakturaListView";

import SendFakturaView from './SendFakturaView';

const MainView = ()=>{
  const [view,setView] = useState(<FakturaListView/>);
  
  return (
    <div>
      <nav>
        <button onClick={()=> setView(<FakturaListView/>)}>Faktury</button>
        <button onClick={()=> setView(<SendFakturaView/>)}>send Faktura</button>
      </nav>
        {view}
    </div>
  );
}
export default MainView;