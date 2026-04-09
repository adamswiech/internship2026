import { useEffect, useState } from "react";
import { FakturaApi } from "../API/FakturaApi";
import { FakturaDTO } from "../Models/API/FakturaDTO";
import Table from "../Components/Table";

const SentFakturaView = ()=>{
    const [faktura, setFaktura] = useState<FakturaDTO | undefined>(undefined);
    const [xmlString, setXmlString] = useState<string>('');

    const handleFileChange = (event: React.ChangeEvent<HTMLInputElement>) => {
        const file = event.target.files?.[0];
        if (!file) return;

        if (!file.name.endsWith('.xml') && file.type !== 'text/xml') {
            alert('Please select an XML file');
            return;
        }

        const reader = new FileReader();

        reader.onload = (e) => {
            const content = e.target?.result as string;
            setXmlString(content);
            console.log('XML as string:', content);
        };

        reader.onerror = () => {
            console.error('Error reading file');
        };
    };
    const sentXml = ()  => {
        if(xmlString.length > 0)
            FakturaApi;
    }
    return (
        <div className="sentXmlView">
            <h2>upload invoice View</h2>
            <input type="file" name="ksefUpload" id="ksefUpload" onChange={handleFileChange} />
            <button className="sentInvoiceButton" onClick={sentXml}>Sent Invoice</button>
            {faktura?<Table body={faktura}/>: <></>}
        </div>
  );
}
export default SentFakturaView;