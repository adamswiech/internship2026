import type { Address } from "./Address";
import type { BankAccount } from "./BankAccount";
import type { ContactInfo } from "./ContactInfo";
import type { Invoice } from "./Invoice";
import type { InvoiceLine } from "./InvoiceLine";
import type { Party } from "./Party";
import type { PaymentInfo } from "./PaymentInfo";
import type { PartialPayment } from "./PartialPayment";
import type { Settlement } from "./Settlement";
import type { Charge } from "./Charge";
import type { Deduction } from "./Deduction";
import type { Terms } from "./Terms";
import type { Contract } from "./Contract";
import type { OrderInfo } from "./OrderInfo";
import type { TransportInfo } from "./TransportInfo";
import type { Carrier } from "./Carrier";

export interface MyResponse {
    address: Address;
    bankAccount: BankAccount;
    contactInfo: ContactInfo;
    invoice: Invoice;
    invoiceLine: InvoiceLine;
    party: Party;
    paymentInfo: PaymentInfo;
    partialPayment: PartialPayment;
    settlement: Settlement;
    charge: Charge;
    deduction: Deduction;
    terms: Terms;
    contract: Contract;
    orderInfo: OrderInfo;
    transportInfo: TransportInfo;
    carrier: Carrier;
}
