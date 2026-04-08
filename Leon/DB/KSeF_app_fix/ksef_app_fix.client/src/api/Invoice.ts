import type { Invoice } from "../interfaces/Invoice.ts";
export const InvoiceCon = {

    apiInvoiceUpload: async (): Promise<number> => {
        try {
            const response = await fetch(`http://localhost:5186/api/Invoice/upload`);

            if (!response.ok)
                throw new Error(`Network response was not ok: ${response.status}`);

            return await response.json() as number;
        } catch (error) {
            console.error("Error calling apiInvoiceUpload:", error);
            throw error;
        }
    },

    apiInvoiceGetInvoiceById: async (): Promise<Invoice> => {
        try {
            const response = await fetch(`http://localhost:5186/api/Invoice/GetInvoiceById={id}`);

            if (!response.ok)
                throw new Error(`Network response was not ok: ${response.status}`);

            return await response.json() as Invoice;
        } catch (error) {
            console.error("Error calling apiInvoiceGetInvoiceById:", error);
            throw error;
        }
    },

    apiInvoiceGetAllInvoices: async (): Promise<Invoice[]> => {
        try {
            const response = await fetch(`http://localhost:5186/api/Invoice/GetAllInvoices`);

            if (!response.ok)
                throw new Error(`Network response was not ok: ${response.status}`);

            return await response.json() as Invoice[];
        } catch (error) {
            console.error("Error calling apiInvoiceGetAllInvoices:", error);
            throw error;
        }
    },
};

