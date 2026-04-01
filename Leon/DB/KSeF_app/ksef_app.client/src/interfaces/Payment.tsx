export interface PaymentInfo {
    id: number;
    invoiceId: number;
    isPartial: boolean;
    partialPayments: PartialPayment[];
    paymentDueDate: string;
    paymentTermsDescription: string;
    paymentMethod: string;
}

export interface PartialPayment {
    id: number;
    date: string;
    amount: number;
    method: string;
    paymentInfo: PaymentInfo;
    paymentInfoId: number;
}