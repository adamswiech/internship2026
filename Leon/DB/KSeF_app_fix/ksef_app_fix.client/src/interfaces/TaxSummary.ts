

export interface TaxSummary {
    id: number;
    invoiceId: number;
    taxRate: string;
    netto: number;
    taxAmount: number;
    brutto: number;
    plnAmount: number;
}
