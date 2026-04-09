export default class Api {
public static async AddXML(file:any): Promise<Faktura[]> {
        const response = await fetch(
            "https://localhost:7459/api/Faktura/AddXML", 
            {
                method: "POST",
                body: file
            }
        );
        const jsonResponse: any[] = await response.json();

        if (!response.ok) {
            throw new Error("HTTP error! status:" + response.status);
        }

        return [];        
      
      
}
public static async GetFaktury(): Promise<Faktura[]> {
        const response = await fetch("https://localhost:7459/api/Faktura/GetFaktury");
        const jsonResponse: any[] = await response.json();

        if (!response.ok) {
            throw new Error("HTTP error! status:" + response.status);
        }   
            
        return jsonResponse;        
      
      
}
public static async GetFaktura(): Promise<Faktura[]> {
        const response = await fetch("https://localhost:7459/api/Faktura/GetFaktura");
        const jsonResponse: any[] = await response.json();

        if (!response.ok) {
            throw new Error("HTTP error! status:" + response.status);
        }   
            
        return jsonResponse;        
      
      
}
}
import type {Faktura} from "../src/interfaces/Faktura";
