import type { PaymentInfo } from "./PaymentInfo";

export interface PartialPayment {
    id: number;
    date: string;
    amount: number;
    method: string;
    paymentInfo: PaymentInfo;
    paymentInfoId: number;
}
