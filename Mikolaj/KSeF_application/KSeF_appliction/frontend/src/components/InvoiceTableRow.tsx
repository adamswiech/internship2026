import type { Invoice } from "../interfaces/Invoice";

interface InvoiceTableRow {
  item: Invoice;
}

export default function InvoiceTableRow({item}:InvoiceTableRow) {
  return (
    <tr className="tr">
      <td>{item.id}</td>
      <td>{item.podmiot1.nip}</td>
      <td>{item.podmiot2.nip}</td>
      <td>{item.podmiot1.nazwa}</td>
      <td>{item.podmiot2.nazwa}</td>
      <td>{item.podmiot1.adresL1}</td>
      <td>{item.podmiot2.adresL1}</td>
    </tr>
  );
}
