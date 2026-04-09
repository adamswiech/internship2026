import {FakturaDTO } from '../Models/API/FakturaDTO'
import {Podmiot } from '../Models/API/Podmiot'

export const FakturaApi = {

    GetFaktura: async () : Promise<FakturaDTO[]> => {
        try {
            const response  = await fetch(`https://${process.env.REACT_APP_HOSTAPI}:${process.env.REACT_APP_PORTAPI}/Faktura/GetFaktura?`);

            if (!response.ok) 
                throw new Error(`Network response was not ok: ${response.status}`);

            return await response.json() as FakturaDTO[];
        }catch (error) {
            console.error('Error creating faktura:', error);
            throw error; 
        }

    }, 
    InsertFakturaFromXml: async (data: string): Promise<FakturaDTO> => {
        try {
        const response = await fetch(`https://${process.env.REACT_APP_HOSTAPI}:${process.env.REACT_APP_PORTAPI}/Faktura/InsertFakturaFromXml`, {
            method: 'POST',
            headers: {
            'Content-Type': 'application/json',
            },
            body: JSON.stringify(data),
        });

        if (!response.ok) 
            throw new Error(`Network response was not ok: ${response.status}`);

        return await response.json() as FakturaDTO;

        }catch (error) {
            console.error('Error creating faktura:', error);
            throw error; 
        }
    },
    GetPodmiot: async () : Promise<Podmiot> => {
        try {
            const response  = await fetch(`https://${process.env.REACT_APP_HOSTAPI}:${process.env.REACT_APP_PORTAPI}/Faktura/GetPodmiot?`);

            if (!response.ok) 
                throw new Error(`Network response was not ok: ${response.status}`);

            return await response.json() as Podmiot;
        }catch (error) {
            console.error('Error creating faktura:', error);
            throw error; 
        }

    }, 
    GetIloscFaktur: async () : Promise<number> => {
        try {
            const response  = await fetch(`https://${process.env.REACT_APP_HOSTAPI}:${process.env.REACT_APP_PORTAPI}/Faktura/GetIloscFaktur?`);

            if (!response.ok) 
                throw new Error(`Network response was not ok: ${response.status}`);

            return await response.json() as number;
        }catch (error) {
            console.error('Error creating faktura:', error);
            throw error; 
        }

    }, 
    GetCzyIstniejeFaktura: async () : Promise<boolean> => {
        try {
            const response  = await fetch(`https://${process.env.REACT_APP_HOSTAPI}:${process.env.REACT_APP_PORTAPI}/Faktura/GetCzyIstniejeFaktura?`);

            if (!response.ok) 
                throw new Error(`Network response was not ok: ${response.status}`);

            return await response.json() as boolean;
        }catch (error) {
            console.error('Error creating faktura:', error);
            throw error; 
        }

    }, 
}