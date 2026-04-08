import type { PartialPayment } from "./PartialPayment";

export interface PaymentInfo {
    id: number;
    invoiceId: number;
    isPartial: boolean;
    partialPayments: PartialPayment[] | null;
    paymentDueDate: Date;
    paymentTermsDescription: string | null;
    paymentMethod: string | null;
}
