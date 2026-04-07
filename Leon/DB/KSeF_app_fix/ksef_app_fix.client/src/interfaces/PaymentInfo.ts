import type { PartialPayment } from "./PartialPayment";

export interface PaymentInfo {
    id: number;
    invoiceId: number;
    isPartial: boolean;
    partialPayments: PartialPayment[];
    paymentDueDate: string;
    paymentTermsDescription: string;
    paymentMethod: string;
}
