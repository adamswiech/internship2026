export interface Settlement {
    id: number;
    invoiceId: number;
    charges: Charge[];
    deductions: Deduction[];
    totalToPay: number;
}

export interface Charge {
    id: number;
    reason: string;
    amount: number;
    settlementId: number;
    settlement: Settlement;
}

export interface Deduction {
    id: number;
    reason: string;
    amount: number;
    settlementId: number;
    settlement: Settlement;
}