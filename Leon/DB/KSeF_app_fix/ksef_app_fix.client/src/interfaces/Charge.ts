import type { Settlement } from "./Settlement";

export interface Charge {
    id: number;
    reason: string | null;
    amount: number;
    settlementId: number;
    settlement: Settlement;
}
