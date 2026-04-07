import type { PaymentInfo } from "./PaymentInfo";

export interface PartialPayment {
    id: number;
    date: string;
    amount: number;
    method?: string | null;
    paymentInfo: PaymentInfo;
    paymentInfoId: number;
}
