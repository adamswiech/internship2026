import type { Address } from "./Address";
import type { ContactInfo } from "./ContactInfo";

export interface Party {
    id: number;
    role: string | null;
    eori: string;
    nip: string;
    name: string;
    mainAddressId: number | null;
    mainAddress: Address;
    correspondenceAddressID: number | null;
    correspondenceAddress: Address;
    contactInfoId: number | null;
    contact: ContactInfo;
    customerNumber: string | null;
}