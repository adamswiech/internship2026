import type { Address } from "./Address";
import type { ContactInfo } from "./ContactInfo";

export interface Party {
    id: number;
    role: string;
    eori: string;
    nip: string;
    name: string;
    mainAddressId: number;
    mainAddress: Address;
    correspondenceAddressID: number;
    correspondenceAddress: Address;
    contactInfoId: number;
    contact: ContactInfo;
    customerNumber: string;
}
