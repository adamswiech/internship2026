import type { Faktura } from "../interfaces/Invoice";

interface InvoiceTableRow {
  item: Faktura;
}

export default function InvoiceTableRow({ item }: InvoiceTableRow) {
  return (
    <table>
      <caption>{`Nr faktury ${item.p_2}`}</caption>
      <tr>
        <th>Id</th>
        <th>NIP Podmiot 1</th>
        <th>NIP Podmiot 2</th>
        <th>Podmiot 1</th>
        <th>Podmiot 2</th>
        <th>Adres Podmiot 1</th>
        <th>Adres Podmiot 2</th>
        <th>Pozycja 1</th>
        <th>Pozycja 2</th>
      </tr>
      <tr className="tr">
        <td>{item.id}</td>
        <td>{item.podmiot1.nip}</td>
        <td>{item.podmiot2.nip}</td>
        <td>{item.podmiot1.nazwa}</td>
        <td>{item.podmiot2.nazwa}</td>
        <td>{item.podmiot1.adresL1}</td>
        <td>{item.podmiot2.adresL1}</td>
        <td>{item.wiersze[0].p_7}</td>
        <td>{item.wiersze[1].p_7}</td>
      </tr>
    </table>
  );
}
