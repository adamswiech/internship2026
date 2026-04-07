import type { Charge } from "./Charge";
import type { Deduction } from "./Deduction";

export interface Settlement {
    id: number;
    invoiceId: number;
    charges: Charge[];
    deductions: Deduction[];
    totalToPay: number;
}
