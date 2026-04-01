import { FaWiersz } from "./FaWiersz";
import { Podmiot } from "./Podmiot";

export interface Faktura {
    Id: number,
    Podmiot1: Podmiot,
    Podmiot2: Podmiot,
    KodWaluty: string,
    P_1: Date,
    P_2: string,
    P_6_Od: Date,
    P_6_Do: Date,
    P_13_1: number,
    P_14_1: number,
    P_14_W: number,
    P_15: number,
    FaWiersze: FaWiersz[]
}