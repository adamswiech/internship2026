import type { Party } from "./Party";
import type { InvoiceLine } from "./InvoiceLine";
import type { TaxSummary } from "./TaxSummary";
import type { PaymentInfo } from "./PaymentInfo";
import type { Settlement } from "./Settlement";
import type { BankAccount } from "./BankAccount";
import type { Terms } from "./Terms";

export interface Invoice {
    id: number;
    invoiceNumber: string | null;
    ksefNumber: string | null;
    issueDate: Date;
    deliveryDate: Date;
    issuePlace: string | null;
    currencyCode: string | null;
    currencyRate: number;
    sellerId: number;
    buyerId: number;
    seller: Party;
    buyer: Party;
    otherParties: Party[] | null;
    lines: InvoiceLine[] | null;
    taxSummaries: TaxSummary[] | null;
    payment: PaymentInfo;
    settlement: Settlement;
    factorBankAccountId: number | null;
    factorBankAccount: BankAccount;
    sellerBankAccountId: number | null;
    sellerBankAccount: BankAccount;
    transactionTerms: Terms;
    footerNote: string | null;
}
