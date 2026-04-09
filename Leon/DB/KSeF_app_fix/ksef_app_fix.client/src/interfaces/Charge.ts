import type { Settlement } from "./Settlement";

export interface Charge {
    id: number;
    reason: string | null;
    amount: number | null;
    settlementId: number | null;
    settlement: Settlement;
}
