import { InvoiceCon } from "../api/Invoice";
import { useState } from "react";

interface UploadRefresh {
  onUploadRefresh: () => void;
}

export default function UploadInvoice({ onUploadRefresh }: UploadRefresh) {
  const [file, setFile] = useState<File | null>(null);
  const [mess, setMess] = useState<string>();

  const upload = async () => {
    if (!file) {
      setMess("No file uploaded");
      return;
    }

    const formData = new FormData();
    formData.append("file", file);

    try {
      const result = await InvoiceCon.apiInvoiceUpload(formData);
      onUploadRefresh(); 
      setMess(`Upload successful${result}`);
    } catch (error) {
      console.error(error);
      setMess("Upload failed");
    }
  };

  return (
    <>
        <div className="UploadInvoice">
        <h2>Upload Invoice</h2>
        <input
            type="file"
            name="XmlInput"
            id="XmlInput"
            accept=".xml"
            onChange={(e) => {
            if (e.target.files?.[0]) setFile(e.target.files[0]);
            }}
        />
        <button className="UploadBtn" onClick={upload}>
            Upload Invoice
        </button>
        </div>
        <div className="UploadInvoice">
            <h2>{mess}</h2>
        </div>
    </>
  );
}