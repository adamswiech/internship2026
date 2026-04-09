import { useState } from "react";
import Api from "../../scripts/api";

export default function AddInvoices() {
  const [fileXML, setFileXML] = useState<File | null>(null);
  const [status, setStatus] = useState<string>("");

  const handleFileChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    const selected = e.target.files?.[0];

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
      const result = await Api.AddXML(formData);
      setStatus(result.id);
    } catch (err: any) {
      setStatus(`Network error: ${err.message}`);
    }
  };

  return (
    <div className="new-invoices-container">
      <div className="invoices-list-container-element">
        <h2>Dodaj nową fakturę</h2>
        <input type="file" accept=".xml" onChange={handleFileChange} />
        <button onClick={addInvoice}>Dodaj fakturę</button>
      </div>

      {status && <p>{status}</p>}
    </div>
  );
}
