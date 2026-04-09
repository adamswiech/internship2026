

export interface TaxSummary {
    id: number;
    invoiceId: number;
    taxRate: string | null;
    netto: number;
    taxAmount: number;
    brutto: number;
    plnAmount: number;
}
