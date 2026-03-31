export interface Podmiot {
  id: number;
  nip: string;
  nazwa: string;
  adresL1: string;
  kodKraju: string;
}

export interface FaWiersz {
  id: number;
  fakturaId: number;
  nr_wiersza: number;
  p_7: string;
  p_8A: number;
  p_8B: number;
  p_9A: number;
  p_11: number;
  p_12: number;
  kurs_waluty: number;
}

export interface Faktura {
  id: number;
  podmiot1Id: number;
  podmiot2Id: number;
  podmiot1: Podmiot;
  podmiot2: Podmiot;
  kod_waluty: string;
  p_1: string;           
  p_2: string;          
  p_6_Od: string;       
  p_6_Do: string;        
  p_13_1: number;       
  p_14_1: number;       
  p_14_W: number;        
  p_15: number;          
  wiersze: FaWiersz[];
}