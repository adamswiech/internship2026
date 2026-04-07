

export interface InvoiceLine {
    id: number;
    invoiceId: number;
    name?: string | null;
    pricePerPiceNetto: number;
    quantity: number;
    unit?: string | null;
    taxRate: number;
    priceTotalNetto?: string | null;
    taxValue: number;
}
