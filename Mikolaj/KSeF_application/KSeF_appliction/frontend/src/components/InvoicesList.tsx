import { useEffect, useState } from "react";
import type { Invoice } from "../interfaces/Invoice";
import InvoiceTableRow from "./InvoiceTableRow";

export default function InvoicesList() {
  const [invoicesArray, setInvoicesArray] = useState<Invoice[]>([]);
  const [fileXML, setFileXML] = useState<any>(null); //change from any to another
  const [status, setStatus] = useState<string>("");

  const [fetchStatus, setFetchStatus] = useState<boolean>(false);

  const handleFileChange = (e: any) => {
    //change form any XDDD
    const selected = e.target.files[0];

    if (selected && selected.name.endsWith(".xml")) {
      setFileXML(selected);
      setStatus("");
    } else {
      setFileXML(null);
      setStatus("Please select a valid .xml file.");
    }
  };

  const addInvoice = async () => {
    if (!fileXML) {
      setStatus("No file selected.");
      return;
    }

    const formData = new FormData();
    formData.append("file", fileXML);

    try {
      setStatus("Uploading...");

      const response = await fetch(
        "https://server-ksef_appliction.dev.localhost:7459/api/Faktura/AddXML",
        {
          method: "POST",
          body: formData,
          // Do NOT set Content-Type header — browser sets it automatically with boundary
        },
      );

      if (!response.ok) {
        const error = await response.text();
        setStatus(`Error: ${error}`);
        return;
      }

      const result = await response.json();
      setStatus(`Faktura created successfully! ID: ${result.id}`);
    } catch (err: any) {
      setStatus(`Network error: ${err.message}`);
    }
  };

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

      setFetchStatus(true);
    };

    getInvoices();
  }, [status]);

  return (
    <div className="invoices-list-container">
      <div className="invoices-list-container-element">
        <h2>Dodaj nową fakturę</h2>
        <input type="file" accept=".xml" onChange={handleFileChange} />
        <button onClick={addInvoice}>Dodaj fakturę</button>
      </div>

      {status && <p>{status}</p>}

      <div className="invoices-list-container-element">
        <h2>Wszystkie faktury</h2>
        <table>
          <tr>
            <th>Id</th>
            <th>NIP Podmiot 1</th>
            <th>NIP Podmiot 2</th>
            <th>Podmiot 1</th>
            <th>Podmiot 2</th>
            <th>Adres Podmiot 1</th>
            <th>Adres Podmiot 2</th>
          </tr>

          {fetchStatus
            ? invoicesArray.map((item: Invoice) => (
                <InvoiceTableRow key={item.id} item={item} />
              ))
            : "Loading..."}
        </table>
      </div>
    </div>
  );
}
