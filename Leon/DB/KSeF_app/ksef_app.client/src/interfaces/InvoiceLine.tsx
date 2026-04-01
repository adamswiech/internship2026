export interface InvoiceLine {
    id: number;
    invoiceId: number;
    name: string;
    pricePerPiceNetto: number;
    quantity: number;
    unit: string;
    taxRate: number;
    priceTotalNetto: string;
    taxValue: number;
}