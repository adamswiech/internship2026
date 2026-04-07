import { PodmiotDTO } from "./PodmiotDTO"; 
import { FaWierszDTO } from "./FaWierszDTO"; 

export interface FakturaDTO{
	sprzedawca: PodmiotDTO,
	nabywca: PodmiotDTO,
	kodWaluty: string,
	dataWyslania: Date,
	nrFaktury: string,
	dataOd: Date,
	dataDo: Date,
	kwatoaNetto: number,
	kwotaPodatku: number,
	kwotaPodatkuPLN: number,
	kwotaNaloznosci: number,
	faWiersze: FaWierszDTO[],
}