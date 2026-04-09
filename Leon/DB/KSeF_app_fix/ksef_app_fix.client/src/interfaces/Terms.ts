import type { Contract } from "./Contract";
import type { OrderInfo } from "./OrderInfo";
import type { TransportInfo } from "./TransportInfo";

export interface Terms {
    id: number;
    invoiceId: number;
    contract: Contract;
    order: OrderInfo;
    deliveryTerms: string | null;
    transport: TransportInfo;
}
