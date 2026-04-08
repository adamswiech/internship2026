// import type { Faktura } from "../src/interfaces/Faktura";

// export default class Api {
//   public static async getFaktura(): Promise<Faktura[]> {
//     const response = await fetch(
//       "https://server-ksef_appliction.dev.localhost:7459/api/Faktura/GetFaktury",
//     );
//     const jsonResponse: any[] = await response.json();

//     return jsonResponse.map(
//       (item: any): Faktura => ({
//         ...item,
//         p_1: new Date(item.p_1),
//         p_6_Od: new Date(item.p_6_Od),
//         p_6_Do: new Date(item.p_6_Do),
//         wiersze: item.wiersze,
//       }),
//     );
//   }
// }
