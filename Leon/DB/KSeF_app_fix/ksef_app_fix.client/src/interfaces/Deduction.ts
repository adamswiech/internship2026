import type { Settlement } from "./Settlement";

export interface Deduction {
    id: number;
    reason?: string | null;
    amount: number;
    settlementId: number;
    settlement: Settlement;
}
