import type { Carrier } from "./Carrier";
import type { Address } from "./Address";

export interface TransportInfo {
    id: number;
    transportType: number;
    carrier: Carrier;
    transportOrderNumber: string;
    cargoDescription: number;
    packagingUnit: string;
    startDate: string;
    endDate: string;
    shipFromId: number;
    shipFrom: Address;
    shipViaID: number;
    shipVia: Address;
    shipToID: number;
    shipTo: Address;
    termsId: number;
}
