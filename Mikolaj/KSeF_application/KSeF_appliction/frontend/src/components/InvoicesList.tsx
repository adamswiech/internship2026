import { useEffect, useState } from "react";
import type { Invoice } from "../interfaces/Invoice";

export default function InvoicesList() {
  const [invoicesArray, setInvoicesArray] = useState<Invoice[]>([]);

  useEffect(() => {
    const getInvoices = async () => {
      try {
        const response = await fetch(
          "https://server-ksef_appliction.dev.localhost:7459/api/Faktura/GetFaktury",
        );
        if (!response.ok) throw new Error(`Response status ${response.status}`);

        const result = await response.json();

        setInvoicesArray(result);
      } catch (error) {
        console.log(`Error: ${error}`);
      }
    };

    getInvoices();
  }, []);

  return (
    <div className="invoices-list-container">
      <h2>All invoices</h2>

      <div className="invoices-list">
        {invoicesArray.map((item: Invoice) => (
          <>
            <table>
                <tr>
                    <th>Id</th>
                </tr>
              <tr>
                <td>{item.id} </td>
                <td>{item.podmiot1.nazwa}</td>
                <td>{item.podmiot2.nazwa}</td>
              </tr>
              
              {/* {item.id}
              {item.id} 
              {item.id} */}
            </table>
          </>
        ))}
      </div>
    </div>
  );
}
