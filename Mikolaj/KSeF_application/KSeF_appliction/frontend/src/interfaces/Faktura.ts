export interface Faktura {
id: number;
podmiot1Id: number;
podmiot2Id: number;
podmiot1: Podmiot;
podmiot2: Podmiot;
kod_waluty: string;
p_1: Date;
p_2: string;
p_6_Od: Date;
p_6_Do: Date;
p_13_1: number;
p_14_1: number;
p_14_W: number;
p_15: number;
wiersze: FaWiersz[];
}
import type { Podmiot } from "./Podmiot";
import type { FaWiersz } from "./FaWiersz";
