import type { Faktura } from "../interfaces/Faktura";

interface InvoiceTableRow {
  item: Faktura;
}

export default function InvoiceTableRow({ item }: InvoiceTableRow) {
  let wierszeLen = item.wiersze.length;
  const rows: any = [];

  const generateRows = () => {
    for (let i = 0; i < wierszeLen; i++) {
      rows.push(
        <tr className="tr">
          {i == 0 && <td rowSpan={i}>{item.id}</td>}
          <td>{item.podmiot1.nip}</td>
          <td>{item.podmiot2.nip}</td>
          <td>{item.podmiot1.kodKraju}</td>
          <td>{item.podmiot2.nazwa}</td>
          <td>{item.wiersze[i].p_7}</td>
          <td>{item.wiersze[i].p_9A.toLocaleString()} zł</td>
          <td>{item.wiersze[i].p_11.toLocaleString()} zł</td>
          <td>{item.wiersze[i].p_12}%</td>
        </tr>,
      );
    }

    return rows;
  };

  return (
    <table>
      <caption>{`Nr faktury: ${item.p_2}`}</caption>
      <tr>
        <th>L.p.</th>
        <th>NIP Podmiot 1</th>
        <th>NIP Podmiot 2</th>
        <th>Podmiot 1</th>
        <th>Podmiot 2</th>
        <th>Tytuł Pozycji</th>
        <th>Cena jednostkowa netto</th>
        <th>Wartość sprzedaży netto</th>
        <th>Stawka podatku VAT</th>
      </tr>
      {generateRows()}
    </table>
  );
}
