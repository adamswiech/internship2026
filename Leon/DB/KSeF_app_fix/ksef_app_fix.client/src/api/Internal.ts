import type { MyResponse } from "../interfaces/MyResponse.ts";
export const InternalCon = {

    sInternalSchema: async (): Promise<MyResponse> => {
        try {
            const response = await fetch(`http://localhost:5186/s/Internal/schema`);

            if (!response.ok)
                throw new Error(`Network response was not ok: ${response.status}`);

            return await response.json() as MyResponse;
        } catch (error) {
            console.error("Error calling sInternalSchema:", error);
            throw error;
        }
    },
};

