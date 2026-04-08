import type { Party } from "../interfaces/Party.ts";
export const PartyCon = {

    apiPartyGetPartyById: async (): Promise<Party> => {
        try {
            const response = await fetch(`http://localhost:5186/api/Party/GetPartyById={id}`);

            if (!response.ok)
                throw new Error(`Network response was not ok: ${response.status}`);

            return await response.json() as Party;
        } catch (error) {
            console.error("Error calling apiPartyGetPartyById:", error);
            throw error;
        }
    },
};

