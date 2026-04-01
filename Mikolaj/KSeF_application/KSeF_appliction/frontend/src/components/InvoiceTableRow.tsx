import type { Faktura } from "../interfaces/Invoice";

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
          <td>{item.podmiot1.nazwa}</td>
          <td>{item.podmiot2.nazwa}</td>
          <td>{item.wiersze[i].p_7}</td>
          <td>{item.p_13_1.toLocaleString()} zł</td>
          <td>{(item.p_13_1 + item.p_14_W).toLocaleString()} zł</td>
          <td>{item.p_14_W.toLocaleString()} zł</td>
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
        <th>Kwota netto PLN</th>
        <th>Kwota brutton PLN</th>
        <th>Kwota podatku PLN</th>
      </tr>
      {generateRows()}
    </table>
  );
}
