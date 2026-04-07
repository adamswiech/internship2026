import type { Settlement } from "./Settlement";

export interface Deduction {
    id: number;
    reason: string;
    amount: number;
    settlementId: number;
    settlement: Settlement;
}
