import { Podmiot } from "./Podmiot"; 
import { FaWiersz } from "./FaWiersz"; 

export interface Faktura{
	id: number,
	podmiot1: Podmiot,
	podmiot2: Podmiot,
	kodWaluty: string,
	p_1: Date,
	p_2: string,
	p_6_Od: Date,
	p_6_Do: Date,
	p_13_1: number,
	p_14_1: number,
	p_14_W: number,
	p_15: number,
	faWiersze: FaWiersz[],
}