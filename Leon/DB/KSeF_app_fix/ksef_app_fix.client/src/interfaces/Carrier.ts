import type { Address } from "./Address";

export interface Carrier {
    id: number;
    countryCode: string | null;
    taxId: string | null;
    name: string | null;
    addressId: number | null;
    address: Address;
}
