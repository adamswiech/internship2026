import type { Address } from "./Address";

export interface Terms {
    id: number;
    invoiceId: number;
    contract: Contract;
    order: OrderInfo;
    deliveryTerms: string;
    transport: TransportInfo;
}

export interface Contract {
    id: number;
    contractDate: string;
    contractNumber: string;
    termsId: number;
}

export interface OrderInfo {
    id: number;
    orderDate: string;
    orderNumber: string;
    termsId: number;
}

export interface TransportInfo {
    id: number;
    transportType: number;
    carrier: Carrier;
    transportOrderNumber: string;
    cargoDescription: number;
    packagingUnit: string;
    startDate: string;
    endDate: string;
    shipFromId: number | null;
    shipFrom: Address;
    shipViaID: number | null;
    shipVia: Address;
    shipToID: number | null;
    shipTo: Address;
    termsId: number;
}

export interface Carrier {
    id: number;
    countryCode: string;
    taxId: string;
    name: string;
    addressId: number | null;
    address: Address;
}