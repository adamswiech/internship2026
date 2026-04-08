import { useEffect, useState } from 'react';
import { InvoiceCon } from './api/Invoice';
import type { Invoice } from './interfaces/Invoice';
import './App.css';

function App() {
    const [invoices, setInvoices] = useState<Invoice[]>([]);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState<string | null>(null);

    useEffect(() => {
        const fetchInvoices = async () => {
            try {
                const data = await InvoiceCon.apiInvoiceGetAllInvoices();
                setInvoices(data);
            } catch (err: any) {
                setError(err.message || "Error fetching invoices");
            } finally {
                setLoading(false);
            }
        };

        fetchInvoices();
    }, []);

    return (
        <div>
            {loading && <p>Loading...</p>}
            {error && <p style={{ color: 'red' }}>{error}</p>}

            {!loading && !error && (
                <ul>
                    {invoices.map((invoice, index) => (
                        <li key={index}>
                            {JSON.stringify(invoice)}
                        </li>
                    ))}
                </ul>
            )}
        </div>
    );
}

export default App;
// TODO CORS - Same Origin, func auto args, frontend;