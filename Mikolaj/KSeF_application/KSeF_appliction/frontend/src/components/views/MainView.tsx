import InvoicesList from "../InvoicesList";
import AddInvoices from "../AddInvoices";
import { useState } from "react";

export default function MainView() {
  const [activeView, setActiveView] = useState<number>(0);

  const handleChangeView = (id: number) => {
    setActiveView(id);
  };

  return (
    <div>
      <nav>
        <button onClick={() => handleChangeView(0)}>Lista faktur</button>
        <button onClick={() => handleChangeView(1)}>Dodaj faktury</button>
      </nav>

      <div className={activeView === 0 ? "block" : "hidden"}>
        <InvoicesList />
      </div>

      <div className={activeView === 1 ? "block" : "hidden"}>
        <AddInvoices />
      </div>
    </div>
  );
}
