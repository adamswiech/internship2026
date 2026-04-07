import type { Settlement } from "./Settlement";

export interface Charge {
    id: number;
    reason: string;
    amount: number;
    settlementId: number;
    settlement: Settlement;
}
