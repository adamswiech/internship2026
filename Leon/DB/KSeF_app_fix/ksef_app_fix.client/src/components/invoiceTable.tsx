import type { Invoice } from "../interfaces/Invoice";

interface Props {
    items: Invoice[];
}

export default function InvoiceTable({ items }: Props) {
    return (
        <div>
            <h2>Invoices</h2>
            <table style={{ borderCollapse: "collapse", width: "100%" }}>
                <thead>
                    <tr>
                        <th style={{ border: "1px solid black", padding: "8px" }}>ID</th>
                        <th style={{ border: "1px solid black", padding: "8px" }}>Invoice Number</th>
                        <th style={{ border: "1px solid black", padding: "8px" }}>Issue Date</th>
                        <th style={{ border: "1px solid black", padding: "8px" }}>Seller</th>
                        <th style={{ border: "1px solid black", padding: "8px" }}>Buyer</th>
                    </tr>
                </thead>
                <tbody>
                    {items.map((inv) => (
                        <tr key={inv.id}>
                            <td style={{ border: "1px solid black", padding: "8px" }}>{inv.id}</td>
                            <td style={{ border: "1px solid black", padding: "8px" }}>{inv.invoiceNumber}</td>
                            <td style={{ border: "1px solid black", padding: "8px" }}>
                                {new Date(inv.issueDate).toLocaleDateString()}
                            </td>
                            <td style={{ border: "1px solid black", padding: "8px" }}>
                                {inv.sellerId ?? "N/A"}
                            </td>
                            <td style={{ border: "1px solid black", padding: "8px" }}>
                                {inv.buyerId ?? "N/A"}
                            </td>
                        </tr>
                    ))}
                </tbody>
            </table>
        </div>
    );
}