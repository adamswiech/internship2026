import type { PartialPayment } from "./PartialPayment";

export interface PaymentInfo {
    id: number;
    invoiceId: number;
    isPartial: boolean;
    partialPayments: PartialPayment[] | null;
    paymentDueDate: Date | null;
    paymentTermsDescription: string | null;
    paymentMethod: string | null;
}
