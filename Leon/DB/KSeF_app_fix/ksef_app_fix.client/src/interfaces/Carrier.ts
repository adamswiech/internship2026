import type { Address } from "./Address";

export interface Carrier {
    id: number;
    countryCode: string;
    taxId: string;
    name: string;
    addressId: number;
    address: Address;
}
