import { useEffect, useState } from "react";
import type { Faktura } from "../interfaces/Faktura";
import InvoiceTableRow from "./InvoiceTableRow";
import Api from "../../scripts/api";

export default function InvoicesList() {
  const [invoicesArray, setInvoicesArray] = useState<Faktura[]>([]);

  const [fetchStatus, setFetchStatus] = useState<boolean>(false);

  

  

  useEffect(() => {
    const getInvoices = async () => {
      const invoices = await Api.GetFaktury();
      setInvoicesArray(invoices);

      setFetchStatus(true);
    };

    getInvoices();
  }, [status]);

  return (
    <div className="invoices-list-container">
      

      <div className="invoices-list-container-element">
        <h2>Wszystkie faktury</h2>

        {fetchStatus ? (
          invoicesArray.map((item: Faktura) => (
            <InvoiceTableRow key={item.id} item={item} />
          ))
        ) : (
          <h3>Loading...</h3>
        )}
      </div>
    </div>
  );
}
