import type { Party } from "./Party";
import type { InvoiceLine } from "./InvoiceLine";
import type { TaxSummary } from "./TaxSummary";
import type { PaymentInfo } from "./PaymentInfo";
import type { Settlement } from "./Settlement";
import type { BankAccount } from "./BankAccount";
import type { Terms } from "./Terms";

export interface Invoice {
    id: number;
    invoiceNumber: string;
    ksefNumber: string;
    issueDate: string;
    deliveryDate: string;
    issuePlace: string;
    currencyCode: string;
    currencyRate: number;
    sellerId: number;
    buyerId: number;
    seller: Party;
    buyer: Party;
    otherParties: Party[];
    lines: InvoiceLine[];
    taxSummaries: TaxSummary[];
    payment: PaymentInfo;
    settlement: Settlement;
    factorBankAccountId: number;
    factorBankAccount: BankAccount;
    sellerBankAccountId: number;
    sellerBankAccount: BankAccount;
    transactionTerms: Terms;
    footerNote: string;
}
