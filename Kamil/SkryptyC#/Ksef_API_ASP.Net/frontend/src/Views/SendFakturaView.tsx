import { useEffect, useState } from "react";
import { FakturaApi } from "../API/FakturaApi";
import { FakturaDTO } from "../Models/API/FakturaDTO";
import Table from "../Components/Table";

const sendFakturaView = ()=>{
    const [faktura, setFaktura] = useState<FakturaDTO | undefined>(undefined);
    const [xmlString, setXmlString] = useState<string>('');
    // console.log(xmlString);
    const handleFileChange = (event: React.ChangeEvent<HTMLInputElement>) => {
    
        const file = event.target.files?.[0];
        if (!file) return;

        if (!file.name.endsWith('.xml') && file.type !== 'text/xml') {
            alert('Please select an XML file');
            return;
        }

        const xmlFileToString = async (file: File): Promise<string> => {
            return new Promise((resolve, reject) => {
                const reader = new FileReader();
                reader.onload = (e: ProgressEvent<FileReader>) => resolve(e.target!.result as string);
                reader.onerror = () => reject(new Error("Failed to read file"));
                reader.readAsText(file, "UTF-8");
            });
        };
        xmlFileToString(file).then(xml => setXmlString(xml));
    };
    const sendXml = ()  => {
        if(xmlString.length > 0)
            FakturaApi.InsertFakturaFromXml(xmlString);
    }
    return (
        <div className="sendXmlView">
            <h2>upload invoice View</h2>
            <input type="file" name="ksefUpload" id="ksefUpload" onChange={handleFileChange} />
            <button className="sendInvoiceButton" onClick={sendXml}>send Invoice</button>
            {faktura?<Table body={faktura}/>: <></>}
        </div>
  );
}
export default sendFakturaView;