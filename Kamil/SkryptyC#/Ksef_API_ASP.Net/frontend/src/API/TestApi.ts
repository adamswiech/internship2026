import {FakturaDTO } from '../Models/API/FakturaDTO'
import {PodmiotDTO } from '../Models/API/PodmiotDTO'

export const TestApi = {

    Test: async () : Promise<undefined> => {
        try {
            const response  = await fetch(`https://${process.env.REACT_APP_HOSTAPI}:${process.env.REACT_APP_PORTAPI}/Test/Test?`);

            if (!response.ok) 
                throw new Error(`Network response was not ok: ${response.status}`);

            return await response.json() as undefined;
        }catch (error) {
            console.error('Error creating faktura:', error);
            throw error; 
        }

    }, 
    GetTest: async () : Promise<FakturaDTO[]> => {
        try {
            const response  = await fetch(`https://${process.env.REACT_APP_HOSTAPI}:${process.env.REACT_APP_PORTAPI}/Test/GetTest?`);

            if (!response.ok) 
                throw new Error(`Network response was not ok: ${response.status}`);

            return await response.json() as FakturaDTO[];
        }catch (error) {
            console.error('Error creating faktura:', error);
            throw error; 
        }

    }, 
    GetPodmiot: async () : Promise<PodmiotDTO> => {
        try {
            const response  = await fetch(`https://${process.env.REACT_APP_HOSTAPI}:${process.env.REACT_APP_PORTAPI}/Test/GetPodmiot?`);

            if (!response.ok) 
                throw new Error(`Network response was not ok: ${response.status}`);

            return await response.json() as PodmiotDTO;
        }catch (error) {
            console.error('Error creating faktura:', error);
            throw error; 
        }

    }, 
}