import { FaWiersz } from "./FaWiersz";
import { Podmiot } from "./Podmiot";

export interface Faktura{
    sprzedawca: Podmiot,
    nabywca: Podmiot,
    kodWaluty: string,
    DataWyslania: Date,
    NrFaktury: string,
    DataOd: Date,
    DataDo: Date,
    KwatoaNetto: number,
    KwotaPodatku: number,
    KwotaPodatkuPLN: number,
    KwotaNaloznosci: number,
    faWiersze: FaWiersz []
}
