import type { Carrier } from "./Carrier";
import type { Address } from "./Address";

export interface TransportInfo {
    id: number;
    transportType: number;
    carrier: Carrier;
    transportOrderNumber?: string | null;
    cargoDescription: number;
    packagingUnit?: string | null;
    startDate: string;
    endDate: string;
    shipFromId?: number | null;
    shipFrom: Address;
    shipViaID?: number | null;
    shipVia: Address;
    shipToID?: number | null;
    shipTo: Address;
    termsId: number;
}
