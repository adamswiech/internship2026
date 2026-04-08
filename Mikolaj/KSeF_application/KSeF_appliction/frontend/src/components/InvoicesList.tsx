import { useEffect, useState } from "react";
import type { Faktura } from "../interfaces/Faktura";
import InvoiceTableRow from "./InvoiceTableRow";
import Api from "../../scripts/api";

export default function InvoicesList() {
  const [invoicesArray, setInvoicesArray] = useState<Faktura[]>([]);
  const [fileXML, setFileXML] = useState<File | null>(null);
  const [status, setStatus] = useState<string>("");
  const [fetchStatus, setFetchStatus] = useState<boolean>(false);

  const handleFileChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    const selected = e.target.files?.[0];

    // console.log(Api.getFaktura());

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
      const invoices = (await Api.GetFaktury()) as Faktura[];
      setInvoicesArray(invoices);

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
